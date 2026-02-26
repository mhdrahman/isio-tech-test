using System;

namespace GildedRoseKata.Helpers;

public static class ItemExtensions
{
    public static void IncreaseQuality(this Item item, int amount)
        => item.Quality = Math.Min(Constants.MaxQuality, item.Quality + amount);
    
    public static void DecreaseQuality(this Item item, int amount)
        => item.Quality = Math.Max(Constants.MinQuality, item.Quality - amount);
}