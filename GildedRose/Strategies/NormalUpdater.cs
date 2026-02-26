using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class NormalUpdater : IItemUpdater
{
    public static readonly NormalUpdater Instance = new();
    
    // Default updater can handle all items.
    public bool CanHandle(Item item) => true;

    public void Update(Item item)
    {
        item.DecreaseQualityBy(1);
        item.SellIn--;

        if (item.PastSellByDate())
        {
            item.DecreaseQualityBy(1);
        }
    }
}