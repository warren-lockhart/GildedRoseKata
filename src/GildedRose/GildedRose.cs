using System.Collections.Generic;
using GildedRoseKata.Enums;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            this.Items = items;
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
            var itemType = ItemManager.GetItemType(item.Name);

            if (itemType == ItemType.Legendary)
            {
                return;
            }

            ItemManager.UpdateSellIn(item);

            switch (itemType)
            {
                case ItemType.BackstagePass:
                    ItemManager.UpdateBackstagePass(item);
                    break;
                case ItemType.Improving:
                    ItemManager.UpdateImprovingItem(item);
                    break;
                case ItemType.Degrading:
                    ItemManager.UpdateDegradingItem(item);
                    break;
                default:
                    ItemManager.UpdateDegradingItem(item);
                    break;
            }
        }
    }
}
