using System.Collections.Generic;
using System.Linq;
using GildedRoseKata.Updaters;

namespace GildedRoseKata;

public static class ItemUpdater
{
    private static readonly List<IItemUpdater> ItemUpdaters =
    [
        new AgedBrieUpdater(),
        new SulfurasUpdater(),
        new BackstagePassUpdater(),
        new ConjuredUpdater(),
        
        // Normal updater needs to go last in order to make it the default case
        new NormalUpdater(),
    ];

    public static void Update(Item item)
    {
        var updater = ItemUpdaters.FirstOrDefault(u => u.CanHandle(item)) ?? new NormalUpdater();
        updater.Update(item);
    }
}