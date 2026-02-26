namespace GildedRoseKata.Strategies;

public interface IItemUpdater
{
    bool CanHandle(Item item);
    void Update(Item item);
}