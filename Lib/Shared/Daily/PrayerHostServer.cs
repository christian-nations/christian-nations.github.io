using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_App.Shared.Daily
{
    public class BibleItemsHostServer
    {
        public static PrayerItemData GetLocalItemData()
        {
            PrayerItemData prayerItemData = new PrayerItemData();
            prayerItemData.Title = "Bible Items";
            List<BibleItem> items = new List<BibleItem>();
            items.Add(new BibleItem()
            {
                Type = "Today",
                Osis = "Hebrews",
                Chapter = 12,
                IsPrayerEnabled = false,
                Verses = "14",
                Words = "Follow peace with all men, and holiness, without which no man shall see the Lord:",
                Inspiration = null,
                Prayer = null,
            });
            items.Add(new BibleItem()
            {
                Type = "Night Prayer",
                Osis = "John",
                Chapter = 6,
                Verses = "29",
                Words = "Jesus answered and said unto them, This is the work of God, that ye believe on him whom he hath sent.",
                Inspiration = "Believe in the work of God! Believe that He sent His son, Jesus, to die so that our sins would be forgiven.",
                Prayer = "Father in heaven, creator of all things, I believe in your work. Forgive my doubts, strengthen my faith, now and forever. In Jesus' name I pray, Amen.",
                IsPrayerEnabled = true,
                
            });
            prayerItemData.Items = items;
            return prayerItemData;
        }
        
    }
}
