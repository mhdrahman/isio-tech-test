using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using GildedRoseKata.Helpers;

namespace GildedRoseTests;

public class GildedRoseTests
{
    [Fact]
    public void NormalItem_RulesRespected()
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

    [Fact]
    public void AgedBrie_RulesRespected()
    {
        // Arrange
        var items = new List<Item>
        {
            // Item before sell by - quality should increase by 1 per day
            new() { Name = Constants.AgedBrie, SellIn = 5, Quality = 10 },
            
            // Item past sell by - quality should increase by 2 per day
            new() { Name = Constants.AgedBrie, SellIn = 0, Quality = 10 },
            
            // Quality should be capped at 40
            new() { Name = Constants.AgedBrie, SellIn = 0, Quality = 39 },
        };
        
        // Act
        RunApp(items, days: 1);
        
        // Assert
        Assert.Equal(11, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
        
        Assert.Equal(12, items[1].Quality);
        Assert.Equal(-1, items[1].SellIn);
        
        Assert.Equal(40, items[2].Quality);
        Assert.Equal(-1, items[2].SellIn);
    }

    [Fact]
    public void Sulfuras_RulesRespected()
    {
        // Arrange
        var items = new List<Item>
        {
            // Item before sell by with negative quality - nothing should change
            new() { Name = Constants.Sulfuras, SellIn = 5, Quality = -10 },
            
            // Item after sell by - nothing should change
            new() { Name = Constants.Sulfuras, SellIn = 0, Quality = 50 },
        };
        
        // Act
        RunApp(items, days: 3);
        
        // Assert
        Assert.Equal(-10, items[0].Quality);
        Assert.Equal(5, items[0].SellIn);
        
        Assert.Equal(50, items[1].Quality);
        Assert.Equal(0, items[1].SellIn);
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