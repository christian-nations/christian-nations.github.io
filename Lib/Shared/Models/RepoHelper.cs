using Blazor_App.Shared.Enums;
using Blazor_App.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_App.Shared.Models
{
    public class RepoHelper
    {
        public static string ToUrlSlug(string str)
        {

            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        
       
    
        static Dictionary<string, string> DictionaryJson = new Dictionary<string, string>();
     
        public static Category GetCategory(string category)
        {
            Category _category = Category.PRAISE;
            foreach (var item in Enum.GetNames(typeof(Category)))
            {
                if (category.ToLower() == item.ToLower())
                {
                    _category = (Category)Enum.Parse(typeof(Category), item);
                    break;
                }
            }
            return _category;
        }
        //public static LanguageType ConvertLanguage(string framework)
        //{
        //    var _framework = SiteInfo.Language;
        //    foreach (var item in Enum.GetNames(typeof(LanguageType)))
        //    {
        //        if (framework.ToLower() == item.ToLower())
        //        {
        //            _framework = (LanguageType)Enum.Parse(typeof(LanguageType), item);
        //            break;
        //        }
        //    }
        //    return _framework;
        //}
        public static Stream GetResourceStreamAsync(Type type, string pathDot)
        {
            var assembly = type.GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(pathDot);
            return stream;
        }
       
        public static bool IsValidRepo(ProjectItem repo)
        {
            if (repo == null)
                return false;
           
            if (repo.Title.IsValidString() == false)
                return false;
           
            if (repo.Title.IsValidString()
                && repo.Notes.IsValidString()
                && repo.Title.IsValidString()
                && repo.Categories?.Count() > 0
                )
            {
                
                return true;
            }
            return false;
        }
        public static async Task<string> DownloadstringAsync(string txtFileUrl)
        {
            string jsonString = null;
            try
            {
                using (var httpClient = new HttpClient())
                {

                    var stream = await httpClient.GetStreamAsync(txtFileUrl);
                    StreamReader reader = new StreamReader(stream);
                    jsonString = reader.ReadToEnd();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return jsonString;
        }
    }
}
