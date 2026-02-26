using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class AgedBrieUpdater : IItemUpdater
{
    public static readonly AgedBrieUpdater Instance = new();
    
    public bool CanHandle(Item item) => item.Name == "Aged Brie";

    public void Update(Item item)
    {
        item.IncreaseQualityBy(1);
        item.SellIn--;
        
        // Match original implementation 
        if (item.PastSellByDate())
        {
            item.IncreaseQualityBy(1);
        }
    }
}