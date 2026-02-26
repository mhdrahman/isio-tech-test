namespace GildedRoseKata.Strategies;

public class SulfurasUpdater : IItemUpdater
{
    public bool CanHandle(Item item) => item.Name ==  "Sulfuras, Hand of Ragnaros";

    public void Update(Item item) => item.SellIn--;
}