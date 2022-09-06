using Blazor_App.Shared.Extensions;
using Blazor_App.Shared.Host;
using Blazor_App.Shared.Models;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_App.Shared.Daily
{
    public class BibleItemsHostServer
    {
        public static BibleItem GetLocalItem()
        {
            var data = new BibleItem()
            {
                Type = "Today",
                BookCommas = "Hebrews,12,14",
                Book = "Heb 12:14",
                Words = "Follow peace with all men, and holiness, without which no man shall see the Lord:",
                Inspiration = null,
                Prayer = null,
            };
            return data;
        }
        public static string Language { get; set; } = "English";
       
        public static Dictionary<string, List<BibleItem>> _items = new Dictionary<string, List<BibleItem>>();
        public async static Task<List<BibleItem>> GetFromServerBibleItemsAsync(string language = null, bool refresh = false)
        {
            if (language == null)
                language = Language;
            if(refresh == false)
            {
                if (_items.ContainsKey(language))
                {
                    return _items[language];
                }
            }
            var url = $"https://raw.githubusercontent.com/christian-nations/christian-nations.github.io/main/Lib/Shared/Daily/Host/{language}/data.json";
            var json = await RepoHelper.DownloadStringAsync2(url);
            if (json.IsValidString())
            {
                var prayerItemData = JsonConvert.DeserializeObject<BibleItemData>(json);
                if(prayerItemData.Items!=null && prayerItemData.Items.Count > 0)
                {
                    
                    _items[language] = prayerItemData.Items;
                    return prayerItemData.Items;
                }
                return null;
            }
            return null;
        }
        public static void PushItemsToServer(PInfo cookieInfo, string message, string serialized)
        {
            if (serialized.IsValidString() == false)
                return;
            CookUpServices.UpdateContentAsync(cookieInfo, message + "-" + DateTime.Now.ToString(), serialized);
        }
    }
}
