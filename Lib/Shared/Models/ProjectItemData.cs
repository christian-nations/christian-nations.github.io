using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_App.Shared.Models
{
    public class ProjectItemData
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; } = SiteInfo.Language.ToString();
        public List<ProjectItem> Items { get; set; }
    }
}
