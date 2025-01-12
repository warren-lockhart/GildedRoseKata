using System;
using GildedRoseKata.Constants;
using GildedRoseKata.Enums;

namespace GildedRoseKata
{
    public static class ItemManager
    {
        public static ItemType GetItemType(string name)
        {
            if (ItemCategories.LegendaryItems.Contains(name))
            {
                return ItemType.Legendary;
            }

            if (ItemCategories.BackstagePasses.Contains(name))
            {
                return ItemType.BackstagePass;
            }

            if (ItemCategories.ImprovingItems.Contains(name))
            {
                return ItemType.Improving;
            }

            if (ItemCategories.DegradingItems.Contains(name))
            {
                return ItemType.Degrading;
            }

            return ItemType.Degrading;
        }

        public static void UpdateSellIn(Item item)
        {
            item.SellIn--;
        }

        public static void UpdateBackstagePass(Item item)
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

        public static void UpdateImprovingItem(Item item)
        {
            if (item.SellIn < 0)
            {
                IncreaseQuality(item, 2);
                return;
            }

            IncreaseQuality(item, 1);
        }

        public static void UpdateDegradingItem(Item item)
        {
            if (item.SellIn < 0)
            {
                DecreaseQuality(item, 2);
                return;
            }

            DecreaseQuality(item, 1);
        }

        private static void IncreaseQuality(Item item, int amount)
        {
            if (item.Quality < 50)
            {
                item.Quality = Math.Min(item.Quality + amount, 50);
            }
        }

        private static void DecreaseQuality(Item item, int amount)
        {
            if (item.Quality > 0)
            {
                item.Quality = Math.Max(0, item.Quality - amount);
            }
        }
    }
}
