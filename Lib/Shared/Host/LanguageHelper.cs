using Blazor_App.Shared.Extensions;
using Blazor_App.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_App.Shared.Host
{
    public class LanguageHelper
    {
        
        static List<LanguageItem> list = null;
        public static event EventHandler<List<LanguageItem>> LanguageTotalUpdated = delegate { };
        public static async void SetTotal(int count, int take = 7)
        {
            var languages = await GetLanguagesAsync();
            var item = languages.Where(p => p.Language == SiteInfo.Language).FirstOrDefault();
            item.Total = count;
            var items = languages.OrderByDescending(p => p.Total).Take(7).ToList();
            LanguageTotalUpdated?.Invoke(EventArgs.Empty, items);
        }
        public static async Task<List<LanguageItem>> GetLanguagesAsync()
        {
            if (list != null && list.Count > 0)
                return list;
            if (SiteInfo.IsDebug)
            {
                var stream = RepoHelper.GetResourceStreamAsync(typeof(LanguageHelper), "Blazor_App.Shared.Host.Languages.json");
                using (StreamReader reader = new StreamReader(stream))
                {
                    var text = reader.ReadToEnd();
                    if (text.IsValidString())
                    {
                        list = JsonConvert.DeserializeObject<List<LanguageItem>>(text);
                    }
                }
            }
            else
            {
               var text = await RepoHelper.DownloadstringAsync("https://raw.githubusercontent.com/christian-nations/christian-nations.github.io/main/Lib/Shared/Host/Languages.json");
                if (text.IsValidString())
                {
                    list = JsonConvert.DeserializeObject<List<LanguageItem>>(text);
                }
            }
            if(list!=null && list.Count > 0)
            {
                list = list.OrderBy(p => p.Language).ToList();
            }
            return list;
        }
        
    }
    public class LanguageItem
    {
        public string Language { get; set; }
        public string Link { get; set; }
        public int Total { get; set; }
    }
}
