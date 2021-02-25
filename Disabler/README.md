﻿# Checker disabler for Valheim

A mod that allows modify "checkers" *(like the requirement of having fire nearby the bed)* with configurable values.
Craftinstation covers both Workbench and Forge.

## Configuration
```
## Settings file was created by plugin Disabler v1.0.1
## Plugin GUID: dev.exel80.disabler

[Bed]

## Disable requirement of fire nearby the bed
# Setting type: Boolean
# Default value: true
Fire = true

## Disable requirement of killing nearby enemies
# Setting type: Boolean
# Default value: false
Enemies = false

## Disable requirement of building a roof for the bed
# Setting type: Boolean
# Default value: false
Exposure = false

## Disable requirement of being dry before interacting with the bed
# Setting type: Boolean
# Default value: true
Wet = true

[CraftingStation]

## Disable requirement of roof when using the crafting station(s)
# Setting type: Boolean
# Default value: true
Roof = true
```