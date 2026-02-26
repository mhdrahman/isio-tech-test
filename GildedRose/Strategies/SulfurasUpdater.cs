using GildedRoseKata.Helpers;

namespace GildedRoseKata.Strategies;

public class SulfurasUpdater : IItemUpdater
{
    public static readonly SulfurasUpdater Instance = new();
    
    public bool CanHandle(Item item) => item.Name == Constants.Sulfuras;

    public void Update(Item item)
    {
        // Do nothing to match original implementation
    }
}