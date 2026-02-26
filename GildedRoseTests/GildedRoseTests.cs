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
            // Item before sell by - quality should decrease by 1 per day
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

    [Fact]
    public void BackstagePasses_RulesRespected()
    {
        // Arrange
        var items = new List<Item>
        {
            // Item with > 7 days expiry: quality should increase by 1
            new() { Name = Constants.BackstagePass, SellIn = 8, Quality = 20 },

            // Items with 3 - 7 days expiry: quality should increase by 3
            new() { Name = Constants.BackstagePass, SellIn = 7, Quality = 20 },
            new() { Name = Constants.BackstagePass, SellIn = 5, Quality = 20 },
            new() { Name = Constants.BackstagePass, SellIn = 3, Quality = 20 },

            // Item with 0 - 2 days expiry: quality should increase by 4
            new() { Name = Constants.BackstagePass, SellIn = 2, Quality = 20 },

            // Item past sell by - quality should drop to 0
            new() { Name = Constants.BackstagePass, SellIn = 0, Quality = 25 },

            // Quality should be capped at 40
            new() { Name = Constants.BackstagePass, SellIn = 1, Quality = 38 },
        };

        // Act
        RunApp(items, days: 1);

        // Assert
        Assert.Equal(21, items[0].Quality);
        Assert.Equal(7, items[0].SellIn);

        Assert.Equal(23, items[1].Quality);
        Assert.Equal(6, items[1].SellIn);

        Assert.Equal(23, items[2].Quality);
        Assert.Equal(4, items[2].SellIn);

        Assert.Equal(23, items[3].Quality);
        Assert.Equal(2, items[3].SellIn);

        Assert.Equal(24, items[4].Quality);
        Assert.Equal(1, items[4].SellIn);

        Assert.Equal(0, items[5].Quality);
        Assert.Equal(-1, items[5].SellIn);

        Assert.Equal(40, items[6].Quality);
        Assert.Equal(0, items[6].SellIn);
    }

    [Fact]
    public void Conjured_RulesRespected()
    {
        // Arrange
        var items = new List<Item>
        {
            // Item before sell by quality decrease by 2 per day
            new() { Name = $"{Constants.Conjured} Mana Cake", SellIn = 5, Quality = 10 },

            // Item past sell by quality should decrease by 4 per day
            new() { Name = $"{Constants.Conjured} Mana Drink", SellIn = 0, Quality = 10 },

            // Min quality should be 0
            new() { Name = $"{Constants.Conjured} Mana Chocolate", SellIn = 0, Quality = 2 },
        };

        // Act
        RunApp(items, days: 1);

        // Assert
        Assert.Equal(8, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);

        Assert.Equal(6, items[1].Quality);
        Assert.Equal(-1, items[1].SellIn);

        Assert.Equal(0, items[2].Quality);
        Assert.Equal(-1, items[2].SellIn);
    }

    [Fact]
    public void MixedItems_RulesRespected()
    {
        // Arrange - one of each type, values chosen so day 1 and day 2 hit edge cases
        var items = new List<Item>
        {
            // Normal item crosses sell by on day 2
            new() { Name = "Item 1", SellIn = 1, Quality = 5 },

            // Aged Brie: crosses sell by, then hits cap at 40
            new() { Name = Constants.AgedBrie, SellIn = 1, Quality = 39 },

            // Normal item reaches quality 0 on day 2
            new() { Name = "Item 2", SellIn = 2, Quality = 2 },

            // Sulfuras: unchanged across both phases
            new() { Name = Constants.Sulfuras, SellIn = 0, Quality = 80 },

            // Backstage pass in 0–2 band day 1, then past concert day 2 (drops to 0)
            new() { Name = Constants.BackstagePass, SellIn = 1, Quality = 35 },

            // Conjured: past sell-by day 2, hits limit of 0
            new() { Name = $"{Constants.Conjured} Mana Cake", SellIn = 1, Quality = 6 },
        };

        // Act 1 – first day
        RunApp(items, days: 1);

        // Assert after day 1
        Assert.Equal(0, items[0].SellIn);
        Assert.Equal(4, items[0].Quality);

        Assert.Equal(0, items[1].SellIn);
        Assert.Equal(40, items[1].Quality);

        Assert.Equal(1, items[2].SellIn);
        Assert.Equal(1, items[2].Quality);

        Assert.Equal(0, items[3].SellIn);
        Assert.Equal(80, items[3].Quality);

        Assert.Equal(0, items[4].SellIn);
        Assert.Equal(39, items[4].Quality);

        Assert.Equal(0, items[5].SellIn);
        Assert.Equal(4, items[5].Quality);

        // Act 2 – second day
        RunApp(items, days: 1);

        // Assert after day 2
        Assert.Equal(2, items[0].Quality);
        Assert.Equal(-1, items[0].SellIn);

        Assert.Equal(40, items[1].Quality);
        Assert.Equal(-1, items[1].SellIn);

        Assert.Equal(0, items[2].Quality);
        Assert.Equal(0, items[2].SellIn);

        Assert.Equal(80, items[3].Quality);
        Assert.Equal(0, items[3].SellIn);

        Assert.Equal(0, items[4].Quality);
        Assert.Equal(-1, items[4].SellIn);

        Assert.Equal(0, items[5].Quality);
        Assert.Equal(-1, items[5].SellIn);
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