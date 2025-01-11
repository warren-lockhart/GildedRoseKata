using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void UpdateQuality_ItemWithName_NameIsPreserved()
        {
            // Arrange
            var name = "testName";
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(name, Items[0].Name);
        }
    }
}
