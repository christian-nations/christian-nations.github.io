
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_App.Shared.Daily
{
    [Table("BibleItem")]
    public class BibleItem
    {
        public BibleItem()
        {
            if (UniqueId == null)
                this.UniqueId = Guid.NewGuid().ToString();
        }
        public string UniqueId { get; set; }

        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Osis { get; set; }
        [JsonIgnore]
        public string Human { get; set; }
        public int Chapter { get; set; }
        public string Verses { get; set; }
        public string Words { get; set; }
        public string Prayer { get; set; }
        public string Inspiration { get; set; }
        public string Language { get; set; } = "English";
        public int Likes { get; set; }
        public int Shares { get; set; }
        public bool IsPrayerEnabled { get; set; }
    }
}
