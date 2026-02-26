using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class ConjuredUpdater : IItemUpdater
{
    public static readonly ConjuredUpdater Instance = new();
    
    public bool CanHandle(Item item) => item.Name.StartsWith(Constants.Conjured);

    public void Update(Item item)
    {
        item.DecreaseQualityBy(2);
        item.SellIn--;

        if (item.PastSellByDate())
        {
            item.DecreaseQualityBy(2);
        }
    }
}