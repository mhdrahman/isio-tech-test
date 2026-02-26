namespace GildedRoseKata.Strategies;

public class SulfurasUpdater : IItemUpdater
{
    public static readonly SulfurasUpdater Instance = new();
    
    public bool CanHandle(Item item) => item.Name == "Sulfuras, Hand of Ragnaros";

    public void Update(Item item)
    {
        // Do nothing to match original implementation
    }
}