using Blazor_App.Shared.Extensions;
using Blazor_App.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_App.Shared.Host
{
    public class LanguageHelper
    {
        public static List<LanguageItem> GetLanguageItems()
        {
            List<LanguageItem> languageItems = new List<LanguageItem>();
            languageItems.Add(new LanguageItem()
            {
                Language = "English",
                Link = "",
            });
            languageItems.Add(new LanguageItem()
            {
                Language = "Cebuano",
                Link = "",
            });
            return languageItems;
        }
        static List<LanguageItem> list = null;
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
            return list;
        }
    }
    public class LanguageItem
    {
        public string Language { get; set; }
        public string Link { get; set; }
    }
}
