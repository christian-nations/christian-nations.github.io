using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_App.Shared.Daily
{
    public class PrayerItemData
    {
        public string Title { get; set; }
        public bool IsPrayerEnabled { get; set; }
        public List<BibleItem> Items { get; set; }
    }
}
