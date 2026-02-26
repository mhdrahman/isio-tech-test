using System;

namespace GildedRoseKata.Helpers;

public static class ItemExtensions
{
    public static void IncreaseQualityBy(this Item item, int amount)
        => item.Quality = Math.Min(Constants.MaxQuality, item.Quality + amount);
    
    public static void DecreaseQualityBy(this Item item, int amount)
        => item.Quality = Math.Max(Constants.MinQuality, item.Quality - amount);

    public static bool PastSellByDate(this Item item)
        => item.SellIn < 0;
}