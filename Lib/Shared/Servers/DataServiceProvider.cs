
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Blazor_App.Shared.Models;
using Blazor_App.Shared.Enums;
using Blazor_App.Shared.Extensions;

namespace Blazor_App.Shared.Servers
{

    public class DataServiceProvider
    {
        public static int TotalCategories = 0;
        static bool hostedJson = true;
        public static Dictionary<string, List<ProjectItem>> _currentItems = new Dictionary<string, List<ProjectItem>>();
        public static bool NeedUpdate = false;
        public static Random Random = new Random();
        public static bool ItemHasLoaded = false;
        public static event EventHandler<List<ProjectItem>> ItemsLoaded;
        
        public static List<ProjectItem> CheckItems()
        {
            if (TotalCategories == 0)
                TotalCategories = Enum.GetNames(typeof(Category)).Length;
            if (SiteInfo.IsDebug)
            {
                hostedJson = false;
            }
            List<ProjectItem> _items = null;
            if (_currentItems.ContainsKey(SiteInfo.Language))
            {
                _items = _currentItems[SiteInfo.Language];
            }
            return _items;
        }
        public static async Task<ProjectItem> GetProjectItemAsync(string slug)
        {
            var items = await GetItemsAsync();
            return items.Where(p => p.Id == slug).FirstOrDefault();
        }
        public static async Task<List<ProjectItem>> GetItemsAsync(bool refresh = false)
        {
            List<ProjectItem> _items = null;
            if (refresh == false)
            {
                _items = CheckItems();
                if (_items != null)
                {
                    return _items;
                }
            }
            if (processing)
                return _items;
            processing = true;
            _items = await GetFromServerAsync();
            if (_items != null && _items.Count > 0)
            {
                _items = _items.Shuffle().ToList();
                _currentItems[SiteInfo.Language] = _items;
                ItemHasLoaded = true;
                ItemsLoaded?.Invoke(SiteInfo.Language, _items);
            }
            processing = false;
            return _items;
        }
        static bool processing = false;
        public async static void LoadDataServerAsync(bool refresh = false)
        {
            List<ProjectItem> _items = null;
            if (refresh == false)
            {
                _items = CheckItems();
                if (_items != null)
                {
                    ItemHasLoaded = true;
                    if (_items.Count > 0)
                    {
                        ItemHasLoaded = true;
                        _items = _items.Shuffle().ToList();
                        _currentItems[SiteInfo.Language] = _items;
                        ItemsLoaded?.Invoke(SiteInfo.Language, _items);
                        return;
                    }
                }
            }
            if (processing)
                return;
            processing = true;
            
            await Task.Run(async () =>
            {
                if (hostedJson)
                {
                    var url = GetHostUrl(SiteInfo.Language);
                    var txt = await CookUpServices.DownloadstringAsync(url);
                    if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt))
                        return;
                    var projectItemData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectItemData>(txt);
                    _items = projectItemData.Items;
                }
                else
                {
                    _items = GetProjectItemData().Items;
                }
                if (_items != null && _items.Count > 0)
                {
                    ItemHasLoaded = true;
                    _currentItems[SiteInfo.Language] = _items;
                    ItemsLoaded?.Invoke(SiteInfo.Language, _items);
                }
            });
            processing = false;
        }
        private static async Task<List<ProjectItem>> GetFromServerAsync()
        {
           
            List<ProjectItem> _items = null;           
            if (hostedJson)
            {
                var url = GetHostUrl(SiteInfo.Language);
                var txt = await CookUpServices.DownloadstringAsync(url);
                if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt))
                    return _items;
                var projectItemData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectItemData>(txt);

                projectItemData.Items = projectItemData.Items.Shuffle().ToList();
                _items = projectItemData.Items;
            }
            else
            {
                _items = GetProjectItemData().Items;
            }
            return _items;
        }
        static bool cloudItemsSet = false;
        public static async Task<List<ProjectItem>> GetFromCloudServerAsync()
        {

            
            List<ProjectItem> _items = null;
            if (cloudItemsSet)
            {
                _items = await GetItemsAsync();
                if(_items != null && _items.Count > 0)
                {
                    return _items;
                }
            }
            var url = GetHostUrl(SiteInfo.Language);
            var txt = await CookUpServices.DownloadstringAsync(url);
            if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt))
            {
                _items = await GetItemsAsync();
            }
            else
            {
                var projectItemData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectItemData>(txt);
                _items = projectItemData.Items;
                if (_items != null && _items.Count > 0)
                {
                    _currentItems[SiteInfo.Language] = _items;
                    ItemsLoaded?.Invoke(_items, _items);
                    cloudItemsSet = true;
                }
            }
            return _items;
        }
        public static ProjectItemData GetProjectItemData()
        {
            ProjectItemData projectItemData = new ProjectItemData();
            
            var stream = RepoHelper.GetResourceStreamAsync(typeof(ProjectItemData), "Blazor_App.Shared.Host." + SiteInfo.Language.ToString() + ".data.json");
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();
                if (text.IsValidString())
                {
                    projectItemData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectItemData>(text);
                }
            }
            return projectItemData;
        }
        public static string GetSamplePreContent()
        {
            var stream = RepoHelper.GetResourceStreamAsync(typeof(ProjectItemData), "Blazor_App.Shared.Host.sample.txt");
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }
        public static string[] GetSamplePreContentArray()
        {
            var stream = RepoHelper.GetResourceStreamAsync(typeof(ProjectItemData), "Blazor_App.Shared.Host.sample.txt");
            var lines = new string[200];
            using (var reader = new StreamReader(stream))
                for (var i = 0; i < 200 && !reader.EndOfStream; i++)
                    lines[i] = reader.ReadLine();
            return lines;
        }
        
        public static string GetHostUrl(string language, bool raw = true)
        {
            if (raw)
            {
                return $"https://raw.githubusercontent.com/christian-nations/christian-nations.github.io/main/Lib/Shared/Host/{language}/data.json";
            }
            else
            {
                return $"https://github.com/christian-nations/christian-nations.github.io/blob/main/Lib/Shared/Host/{language}/data.json";
            }
        }
        public static void UpdateToServer(string language, string serialize, string message)
        {
            var path = GetHostUrl(language, false);
            CookieManager.PushCookies(language, serialize, message, path);
        }
        public static void LoadDeveloperTools()
        {
           
        }
    }
}
