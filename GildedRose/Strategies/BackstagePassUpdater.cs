using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class BackstagePassUpdater : IItemUpdater
{
    public bool CanHandle(Item item) => item.Name == "Backstage passes to a TAFKAL80ETC concert";

    public void Update(Item item)
    {
        switch (item.SellIn)
        {
            case > 7:
                item.IncreaseQualityBy(1);
                break;
            case > 2:
                item.IncreaseQualityBy(3);
                break;
            default:
                item.IncreaseQualityBy(4);
                break;
        }
        
        item.SellIn--;
        if (item.PastSellByDate())
        {
            item.Quality = 0;
        }
    }
}