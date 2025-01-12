using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GildedRoseKata;
using Xunit;

namespace GildedRoseTests
{
    [ExcludeFromCodeCoverage]
    public class GildedRoseTests
    {
        [Fact]
        public void UpdateQuality_ItemWithName_NameIsPreserved()
        {
            // Arrange
            var name = "testName";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(name, Items[0].Name);
        }

        [Theory]
        [InlineData("+5 Dexterity Vest")]
        [InlineData("Elixir of the Mongoose")]
        public void UpdateQuality_DegradingItemBeforeSellBy_QualityDegradesByOne(string itemName)
        {
            // Arrange
            List<Item> Items = new List<Item> { new Item { Name = itemName, SellIn = 1, Quality = 4 }
            };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(3, Items[0].Quality);
        }

        // Once the sell by date has passed, Quality degrades twice as fast
        [Theory]
        [InlineData("+5 Dexterity Vest")]
        [InlineData("Elixir of the Mongoose")]
        public void UpdateQuality_SellByDatePassed_QualityDegradesTwiceAsFast(string itemName)
        {
            // Arrange
            List<Item> Items = new List<Item> { new Item { Name = itemName, SellIn = 0, Quality = 4 }
            };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(2, Items[0].Quality);
        }

        // The Quality of an item is never negative
        [Theory]
        [InlineData("+5 Dexterity Vest")]
        [InlineData("Elixir of the Mongoose")]
        public void UpdateQuality_DegradingItem_QualityNeverNegative(string itemName)
        {
            List<Item> Items = new List<Item> { new Item { Name = itemName, SellIn = 4, Quality = 0 }};
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, Items[0].Quality);
        }

        // "Aged Brie" actually increases in Quality the older it gets
        [Fact]
        public void UpdateQuality_IncreasingQualityItem_QualityIncreases()
        {
            // Arrange
            var name = "Aged Brie";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 4, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(1, Items[0].Quality);
        }

        // The Quality of an improving, non-backstage-pass item increases by 2 when sell by has passed
        // Not explicitly documented in requirement, but implied
        [Fact]
        public void UpdateQuality_IncreasingQualityItemPastSellIn_QualityIncreasesByTwo()
        {
            // Arrange
            var itemName = "Aged Brie";
            List<Item> Items = new List<Item> { new Item { Name = itemName, SellIn = -1, Quality = 2 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(4, Items[0].Quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches
        // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
        [Fact]
        public void UpdateQuality_BackstagePassLessThanTenDaysBeforeSellIn_QualityIncreasesByTwo()
        {
            // Arrange
            var name = "Backstage passes to a TAFKAL80ETC concert";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 10, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(2, Items[0].Quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches
        // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
        [Fact]
        public void UpdateQuality_BackstagePassLessThanFiveDaysBeforeSellIn_QualityIncreasesByThree()
        {
            // Arrange
            var name = "Backstage passes to a TAFKAL80ETC concert";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 5, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(3, Items[0].Quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches
        // Quality drops to 0 after the concert
        [Fact]
        public void UpdateQuality_BackstagePassAfterSellIn_QualityDropsToZero()
        {
            // Arrange
            var name = "Backstage passes to a TAFKAL80ETC concert";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, Items[0].Quality);
        }

        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        [Fact]
        public void UpdateQuality_LegendaryItem_NeverHasToBeSold()
        {
            // Arrange
            var name = "Sulfuras, Hand of Ragnaros";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 1, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(1, Items[0].SellIn);
        }

        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        [Fact]
        public void UpdateQuality_LegendaryItem_NeverDecreasesInQuality()
        {
            // Arrange
            var name = "Sulfuras, Hand of Ragnaros";
            List<Item> Items = new List<Item> { new Item { Name = name, SellIn = 1, Quality = 4 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(4, Items[0].Quality);
        }

        // The Quality of an item is never more than 50
        [Theory]
        [InlineData("Aged Brie")]
        [InlineData("Backstage passes to a TAFKAL80ETC concert")]
        public void UpdateQuality_IncreasingQualityItem_QualityIsNeverMoreThanFifty(string itemName)
        {
            // Arrange
            List<Item> Items = new List<Item> { new Item { Name = itemName, SellIn = 4, Quality = 50 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(50, Items[0].Quality);
        }
    }
}
