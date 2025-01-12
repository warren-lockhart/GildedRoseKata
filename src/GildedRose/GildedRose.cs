using System;
using System.Collections.Generic;
using GildedRoseKata.Enums;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private static List<string> BackstagePasses = ["Backstage passes to a TAFKAL80ETC concert"];
        private static List<string> ImprovingItems = ["Aged Brie"];
        private static List<string> DegradingItems = ["+5 Dexterity Vest", "Elixir of the Mongoose"];
        private static List<string> LegendaryItems = ["Sulfuras, Hand of Ragnaros"];

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
            var itemType = GetItemType(item.Name);

            if (itemType == ItemType.Legendary)
            {
                return;
            }

            UpdateSellIn(item);

            switch (itemType)
            {
                case ItemType.BackstagePass:
                    UpdateBackstagePass(item);
                    break;
                case ItemType.Improving:
                    UpdateImprovingItem(item);
                    break;
                case ItemType.Degrading:
                    UpdateDegradingItem(item);
                    break;
                default:
                    UpdateDegradingItem(item);
                    break;
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
            if (item.Quality > 0)
            {
                item.Quality = Math.Max(0, item.Quality - amount);
            }
        }

        private static void UpdateSellIn(Item item)
        {
            item.SellIn--;
        }

        private static ItemType GetItemType(string name)
        {
            if (LegendaryItems.Contains(name))
            {
                return ItemType.Legendary;
            }

            if (BackstagePasses.Contains(name))
            {
                return ItemType.BackstagePass;
            }

            if (ImprovingItems.Contains(name))
            {
                return ItemType.Improving;
            }

            if (DegradingItems.Contains(name))
            {
                return ItemType.Degrading;
            }

            return ItemType.Degrading;
        }
    }
}
