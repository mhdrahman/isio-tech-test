using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class BackstagePassUpdater : IItemUpdater
{
    public static readonly BackstagePassUpdater Instance = new();
    
    public bool CanHandle(Item item) => item.Name == Constants.BackstagePass;

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