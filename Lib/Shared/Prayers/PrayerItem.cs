
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_App.Shared.Prayers
{
    public class PrayerItem
    {
        public string Type { get; set; }
        public string Osis { get; set; }
        public int Chapter { get; set; }
        public string Verses { get; set; }
        public string Words { get; set; }
        public string Prayer { get; set; }
        public string Inspiration { get; set; }
        public string Language { get; set; } = "English";
        public int Likes { get; set; }
        public int Shares { get; set; }
    }
}
