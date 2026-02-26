# Gilded Rose

## Adding/updating an item type

Item behaviour is defined in `./GildedRose/Strategies/`. To support a new item type:
- Create a new strategy in the above folder which implements IItemupdater
- Expose a public static readonly singleton instance e.g. `public static readonly FooUpdater Instance = new();`
- Implement:
  - `CanHandle(Item item)` this should return true only for this item based on some criteria, typically name
  - `Update(Item item)` this should apply the rules for the item like updating quality and sell in. **Ensure you use helpers provided in `ItemExtensions.cs` where possible**.
- Register the strategy in `./GildedRose/ItemUpdater.cs`
- Add or update the tests in `./GildedRoseTests/`

## Build the project

Use your normal build tools to build the projects in Debug mode.
For example, you can use the `dotnet` command line tool:

``` cmd
dotnet build GildedRose.sln -c Debug
```

## Run the Gilded Rose Command-Line program

For e.g. 10 days:

``` cmd
GildedRose/bin/Debug/net8.0/GildedRose 10
```

## Run all the unit tests

``` cmd
dotnet test
```