using Blazor_App.Shared.Models;
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
        public static string GetLanguages()
        {
            var stream = RepoHelper.GetResourceStreamAsync(typeof(ProjectItemData), "Blazor_App.Shared.Host.Languages.json");
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }
    }
    public class LanguageItem
    {
        public string Language { get; set; }
        public string Link { get; set; }
    }
}
