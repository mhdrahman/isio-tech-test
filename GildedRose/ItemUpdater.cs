using System.Collections.Generic;
using System.Linq;
using GildedRoseKata.Strategies;

namespace GildedRoseKata;

public static class ItemUpdater
{
    // We use static instances here to avoid creating new instances every time.
    private static readonly List<IItemUpdater> ItemUpdaters =
    [
        AgedBrieUpdater.Instance,
        SulfurasUpdater.Instance,
        BackstagePassUpdater.Instance,
        ConjuredUpdater.Instance,
        
        // Normal updater needs to go last in order to make it the default case
        NormalUpdater.Instance,
    ];

    public static void Update(Item item)
    {
        var updater = ItemUpdaters.FirstOrDefault(u => u.CanHandle(item)) ?? NormalUpdater.Instance;
        updater.Update(item);
    }
}