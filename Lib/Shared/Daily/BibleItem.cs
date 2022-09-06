
using Blazor_App.Shared.Extensions;
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
       
        public string Prayer { get; set; }
        public string Inspiration { get; set; }
        public string Language { get; set; } = BibleItemsHostServer.Language;
        public int Likes { get; set; }
        public int Shares { get; set; }
        public string Book { get; set; }
        public bool IsReadOnly { get; set; } = true;
        public bool IsPublic { get; set; } = true;

        [JsonIgnore]
        public string Words { get; set; }

        [JsonIgnore]
        public string BookCommas { get; set; }
        public bool IsValid()
        {
            if (this.Prayer.IsValidString() == false)
                return false;
            if (this.Book.IsValidString() == false)
                return false;
            return true;
        }
    }
}
