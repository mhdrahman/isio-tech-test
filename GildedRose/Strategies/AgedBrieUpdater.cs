using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class AgedBrieUpdater : IItemUpdater
{
    public bool CanHandle(Item item) => item.Name == "Aged Brie";

    public void Update(Item item)
    {
        item.IncreaseQualityBy(1);
        item.SellIn--;
        
        // TODO: Might be missing some logic here...
    }
}