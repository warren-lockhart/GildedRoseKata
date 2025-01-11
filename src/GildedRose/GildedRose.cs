using System;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                UpdateItem(item);
            }
        }

        private static void UpdateItem(Item item)
        {
            UpdateSellIn(item);

            if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                DecreaseQuality(item);

                if (item.SellIn < 0)
                {
                    DecreaseQuality(item);
                }
            }
            else
            {   // Item is Aged Brie or Backstage pass.
                if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.SellIn < 0)
                    {
                        item.Quality = 0;
                    }
                    else if (item.SellIn < 5)
                    {
                        IncreaseQuality(item, 3);
                    }
                    else if (item.SellIn < 10)
                    {
                        IncreaseQuality(item, 2);
                    }
                    else
                    {
                        IncreaseQuality(item, 1);
                    }
                }
                else
                {
                    // Aged Brie
                    IncreaseQuality(item, 1);
                    if (item.SellIn < 0)
                    {
                        // An additional increase by 1 (total increase by 2). Not in the spec!
                        IncreaseQuality(item, 1);
                    }
                }
            }
        }

        private static void IncreaseQuality(Item item, int amount)
        {
            if (item.Quality < 50)
            {
                item.Quality = Math.Min(item.Quality + amount, 50);
            }
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.Quality--;
            }
        }

        private static void UpdateSellIn(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn--;
            }
        }
    }
}
