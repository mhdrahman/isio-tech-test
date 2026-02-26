using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class NormalUpdater : IItemUpdater
{
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