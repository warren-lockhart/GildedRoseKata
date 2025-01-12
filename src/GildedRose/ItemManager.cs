using System;
using GildedRoseKata.Constants;
using GildedRoseKata.Enums;

namespace GildedRoseKata
{
    public static class ItemManager
    {
        public static ItemType GetItemType(string name)
        {
            return name switch
            {
                _ when ItemCategories.Legendary.Contains(name) => ItemType.Legendary,
                _ when ItemCategories.BackstagePasses.Contains(name) => ItemType.BackstagePass,
                _ when ItemCategories.Improving.Contains(name) => ItemType.Improving,
                _ when ItemCategories.Conjured.Contains(name) => ItemType.Conjured,
                _ when ItemCategories.Degrading.Contains(name) => ItemType.Degrading,
                _ => ItemType.Degrading
            };
        }

        public static void UpdateSellIn(Item item)
        {
            item.SellIn--;
        }

        public static void UpdateBackstagePass(Item item)
        {
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }

            int qualityIncrease = item.SellIn switch
            {
                < 5 => 3,
                < 10 => 2,
                _ => 1
            };

            IncreaseQuality(item, qualityIncrease);
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

        public static void UpdateConjuredItem(Item item)
        {
            if (item.SellIn < 0)
            {
                DecreaseQuality(item, 4);
                return;
            }

            DecreaseQuality(item, 2);
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
