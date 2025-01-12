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
                // TODO: A branch will be needed for the new item type.
                // TODO: Where do legendary items fit into the decision making?

                UpdateDegradingItem(item);
            }
            else
            {   // Item is Aged Brie or Backstage pass.
                if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    UpdateBackstagePass(item);
                }
                else
                {
                    UpdateImprovingItem(item);
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

        private static void UpdateDegradingItem(Item item)
        {
            if (item.SellIn < 0)
            {
                DecreaseQuality(item, 2);
                return;
            }

            DecreaseQuality(item, 1);
        }

        private static void UpdateImprovingItem(Item item)
        {
            if (item.SellIn < 0)
            {
                // TODO: This requirement is not captured in the specification.
                // Add a unit test for it
                IncreaseQuality(item, 2);
                return;
            }

            IncreaseQuality(item, 1);
        }

        private static void UpdateBackstagePass(Item item)
        {
            switch (item.SellIn)
            {
                case < 0:
                    item.Quality = 0;
                    break;
                case < 5:
                    IncreaseQuality(item, 3);
                    break;
                case < 10:
                    IncreaseQuality(item, 2);
                    break;
                default:
                    IncreaseQuality(item, 1);
                    break;
            }
        }

        private static void DecreaseQuality(Item item, int amount)
        {
            if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.Quality = Math.Max(0, item.Quality - amount);
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
