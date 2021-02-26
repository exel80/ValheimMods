# Base Health Modifier

Modify the player base health. Normally base health (white bar in food bar) is 25, this mod allows you modify it.
With this mod, you can make your game a lot harder or easier. However you like it =)

## Manual Installation
1. Install the [BepInExPack Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download latest ``BaseHealthModifier.dll`` by clicking ``Manual Download > Extract ZIP file > You only need dll from it``
3. Move the DLL to plugins folder into ``<GameDirectory>\Bepinex\plugins``

## Config
Config file ``dev.exel80.basehealthmodifier`` is inside the config folder ``<GameDirectory>\Bepinex\config``.
File will be automatically generated after the first time launching with the plugin.

```ini
## Settings file was created by plugin BaseHealthModifier v1.0.0
## Plugin GUID: dev.exel80.basehealthmodifier

[Player]

## Player base health when haven't eat anything
# Setting type: Int32
# Default value: 1
BaseHealth = 1
```