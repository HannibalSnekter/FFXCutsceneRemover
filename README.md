# FFXCutsceneRemover w/ Randomiser
Cutscene skipping program for the Windows Steam version of Final Fantasy X HD Remaster.
If you encounter any bugs or have any questions, please open an issue on GitHub or contact us on the [FFX Speedrun Discord](https://discord.gg/X3qXHWG).

Includes a randomiser which will randomise the following aspects of the game (Options can be turned off by editing the RandomiserOptions.JSON file, 0 = off, 1 = on):

* Randomises Sphere Grid including Stat, Ability, Empty and Lock nodes.

* Randomises Base stats of the 7 main characters

* Tidus always starts with Flee

* Shops have randomised equipment

### Important

* Sphere grid randomisation has been designed for use with the Standard Sphere Grid and no testing has been done on the Expert Sphere Grid.

* CSR is designed and tested to be used for speedruns only. Using this mod for normal playthroughs is not supported, and the unexpected behaviour may cause game-breaking bugs. If you use this mod for normal playthroughs, it is strongly recommended to manually save regularly and not rely on auto-saves.

* This mod currently only works on Windows using the Steam release of FFX.

### Usage:

1. Download the latest release from the [releases page here](https://github.com/erickt420/FFXCutsceneRemover/releases).

2. Launch FFXCutsceneRemove.exe.

3. Launch Final Fantasy X.

The order in which you launch the mod and the game does not matter.

### Arguments:
You can use multiple arguments by separating them with a space.
- enable debug:  
`FFXCutsceneRemover.exe true`
- change the amount of milliseconds the program sleeps between polling iterations:  
`FFXCutsceneRemover.exe 50`
