# RecipeModifier
Modify the resource cost to craft a specific type of items. You can modify ``double`` value from config files what ever you want.
Plugin will round the number back to ``int``, before passing it to the game.

You can use this to make the game harder or easier.

## Manual Installation
1. Install the [BepInExPack Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download latest ``RecipeModifier.dll`` by clicking ``Manual Download > Extract ZIP file > You only need dll from it``
3. Move the DLL to plugins folder into ``<GameDirectory>\Bepinex\plugins``

## Config
Config file ``dev.exel80.recipemodifier`` is inside the config folder ``<GameDirectory>\Bepinex\config`` and will be automatically generated after the first time launching the plugin.
Changing value under ``1.0`` will reduce the amount of resource you need to craft it, and vice versa. 

```ini
## Settings file was created by plugin RecipeModifier v1.0.0
## Plugin GUID: dev.exel80.recipemodifier

[RecipeMultiplier]

## None recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
None = 1.5

## Material recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Material = 1.5

## Consumable recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Consumable = 1.5

## OneHandedWeapon recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
OneHandedWeapon = 1.5

## Bow recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Bow = 1.5

## Shield recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Shield = 1.5

## Helmet recipe(s) resource multiplier
# Setting type: Double
# Default value: 2
Helmet = 2

## Chest recipe(s) resource multiplier
# Setting type: Double
# Default value: 2
Chest = 2

## Ammo recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Ammo = 1.5

## Customization recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Customization = 1.5

## Legs recipe(s) resource multiplier
# Setting type: Double
# Default value: 2
Legs = 2

## Hands recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Hands = 1.5

## Trophie recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Trophie = 1.5

## TwoHandedWeapon recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
TwoHandedWeapon = 1.5

## Torch recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Torch = 1.5

## Misc recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Misc = 1.5

## Shoulder recipe(s) resource multiplier
# Setting type: Double
# Default value: 2
Shoulder = 2

## Utility recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Utility = 1.5

## Tool recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Tool = 1.5

## Attach_Atgeir recipe(s) resource multiplier
# Setting type: Double
# Default value: 1.5
Attach_Atgeir = 1.5
```

## Changelog
1.0.0 - Initial release