using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_App.Shared.Prayers
{
    public class PrayerHostServer
    {
        public static PrayerItemData GetPrayerItemData()
        {
            PrayerItemData prayerItemData = new PrayerItemData();
            prayerItemData.Title = "Prayers";
            List<PrayerItem> items = new List<PrayerItem>();
            items.Add(new PrayerItem()
            {
                Type = "Night Prayer",
                Osis = "John",
                Chapter = 6,
                Verses = "29",
                Words = "Jesus answered and said unto them, This is the work of God, that ye believe on him whom he hath sent.",
                Inspiration = "Believe in the work of God! Believe that He sent His son, Jesus, to die so that our sins would be forgiven.",
                Prayer = "Father in heaven, creator of all things, I believe in your work. Forgive my doubts, strengthen my faith, now and forever. In Jesus' name I pray, Amen."
            });
            prayerItemData.Items = items;
            return prayerItemData;
        }
    }
}
