# VFXEditor
A VFX editing plugin for Dalamud  ([Wiki](https://github.com/0ceal0t/Dalamud-VFXEditor/wiki))

<img align="center" src="docs/preview.png" width="700px">

## Installation
1. This plugin requires [XIV Quick Launcher](https://github.com/goatcorp/FFXIVQuickLauncher) to run
2. Enable the plugins from the `/xlplugins` menu
3. Once installed, open with `/vfxedit`

## Usage
1. Select a "source" VFX (the effect you want to edit)
2. Select a "replace" VFX (the effect to temporarily overwrite, can be the same as the source)
3. Make any modifications you want, then press "Update"

Having problems? Check [here](https://github.com/0ceal0t/Dalamud-VFXEditor/wiki/Troubleshooting)

## Building
1. Build the solution
2. Place the files inside of `AppData\Roaming\XIVLauncher\devPlugins`, or wherever the `devPlugins` folder of your QuickLauncher installation is located
3. Run QuickLauncher

### Notes
* This plugin does not currently work with `LivePluginLoader`
* It might break Penumbra

## Features
* Load and preview in-game VFXs (weapons, actions, status effects)
* Edit and export
* Preview textures
* Add, remove, and edit particles, emitters, etc.
* Import and export models
* Export as `.avfx` or Textools Modpack

## Contributing
If a VFX is behaving unexpectedly, or you are getting log messages incidating that it is not being parsed properly, please open an [Issue](https://github.com/0ceal0t/Dalamud-VFXEditor/issues).

A lot of the data in `.avfx` files is not fully understood, and I'm regularly finding new fields, so any help is appreciated.

## TODO
- [ ] Clean up adding + deleting items (get rid of extra `Init()`). Be careful of `foreach`, like items in `UISplitView`
- [ ] Linking between objects (`emitterIdx = 1` links to `Emitters[1]`, if it exists)
- [ ] Properly fix literal int list
- [ ] Node view (waiting on [ImNode](https://github.com/mellinoe/ImGui.NET/pull/218))
- [ ] 3D model view (waiting on [ImGuizmo](https://github.com/mellinoe/ImGui.NET/pull/218))
- [ ] Save all particles at once, etc.
- [ ] Multiple vfxs (-_-)
- [ ] Test with Penumbra
- [ ] Flesh out Wiki
- [ ] Clean up "troubleshooting"
- [ ] File loaded indication
- [ ] Better key UI (curve editor?)
- [ ] Help text
- [ ] Penumbra webserver integration
- [ ] "Go To" button
- [ ] Get it to work with LPL (FASM is being annoying)
- [ ] TexTools import
- [ ] Penumbra import
- [ ] Cutscene VFX select
- [ ] Better error / log display
- [ ] Open from equipped weapon
- [ ] Better TMB parsing
- [ ] Texture replacement with Penumbra/Textools
- [ ] Update "try on" preview
- [ ] Auto-update npc csv file

---
- [x] ~~Better sidebar names to reflect target index~~
- [x] ~~Multiple model indexes (see `vfx/action/ab_virus1/eff/abi_virus1t1h.avfx` model particles)~~
- [x] ~~Multiple masks (see `vfx/action/mgc_siles1/eff/mgc_sile1t0c.avfx` TC1)~~
- [x] ~~Emote VFX select~~
- [x] ~~Fix Penumbra folder select (kinda)~~
- [x] ~~Fix expac textools export~~
- [x] ~~Monster list(??)~~
- [x] ~~Zone VFX select~~
- [x] ~~Auto fix bg vfx~~
- [x] ~~Fix model export (lots of broken shit)~~
- [x] ~~New weapon vfx~~
- [x] ~~Save individual parts (particles, emitters, etc.)~~
- [x] ~~Penumbra export~~
- [x] ~~Fix textools export~~
- [x] ~~-Don't write to the game folder lmao~~
- [x] ~~Fix selectable lag~~
- [x] ~~Default VFX~~
- [x] ~~Test in GPose~~
- [x] ~~Better clips UI~~
- [x] ~~Model import~~
- [x] ~~Model export~~
- [x] ~~Export texture~~
- [x] ~~Model modification (order / adding / deleting)~~
- [x] ~~Better search (doesn't have to be exact match)~~
- [x] ~~Raw extract~~
- [x] ~~Open recent~~
- [x] ~~More settings~~
- [x] ~~Verify on each load + show output with icon~~
- [x] ~~UI Cleanup (fewer trees)~~
- [x] ~~TexTools export~~
- [x] ~~Fix `ItPr` / `ItEm` (emitter comes after particle, all counted as part of the same block? see `Flash` VFX)~~
- [x] ~~Fix `BvPr = 255` issue (see `Rolling Thunder / Target` VFX)~~
- [x] ~~Bind Prp1/Prp2 (see `Thunder 2` VFX)~~
- [x] ~~Rework texture and model views~~
- [x] ~~Emitter sound~~
- [x] ~~Fix issue when adding / removing an item switches tabs. This is because the id of the tab changes, like `Particles (3) -> Particles (2)`~~
- [x] ~~Binder properties view~~
- [x] ~~Update README preview image~~