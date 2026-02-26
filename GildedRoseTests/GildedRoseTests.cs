using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTests
{
    [Fact]
    public void NormalItem_DecreasesQualityAndSellIn_LimitsAndRulesRespected()
    {
        // Arrange
        var items = new List<Item>
        {
            // Item before sell by
            new() { Name = "foo", SellIn = 5, Quality = 10 },
            
            // Item past sell by - should degrade at double speed
            new() { Name = "foo", SellIn = 0, Quality = 5 },
        };
        
        // Act
        RunApp(items, days: 5);
        
        // Assert
        Assert.Equal(5, items[0].Quality);
        Assert.Equal(0, items[0].SellIn);
        
        Assert.Equal(0, items[1].Quality);
        Assert.Equal(-5, items[1].SellIn);
    }

    private static void RunApp(List<Item> items, int days = 1)
    {
        var app = new GildedRose(items);
        for (var i = 0; i < days; i++)
        {
            app.UpdateQuality();
        }
    }
}