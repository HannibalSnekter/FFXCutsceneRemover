using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FFXCutsceneRemover.Logging;

namespace FFXCutsceneRemover.Resources
{
    /* This class contains most of the transitions. Transitions added here are automatically evalutated in the main loop. */
    static class RandomiserTransitions
    {
        private static readonly MemoryWatchers MemoryWatchers = MemoryWatchers.Instance;

        public static readonly RandoSetupTransition RandoSetupTransition = new RandoSetupTransition { ForceLoad = false, Description = "Running Randomisation" };

        static readonly RandoShopTransition OakaMacalaniaShopTransition = new RandoShopTransition { ForceLoad = false, Description = "O'aka Lake Macalania", ConsoleOutput = false, shopID = 18, shopSize = 8, shopTier = 2, Repeatable = true };
        static readonly RandoShopTransition LucaShopsTransition = new RandoShopTransition { ForceLoad = false, Description = "Luca (Pre-Airship)", ConsoleOutput = false, shopID = 02, shopSize = 12, shopTier = 0, Repeatable = true };

        public static readonly Dictionary<IGameState, Transition> StandardTransitions = new Dictionary<IGameState, Transition>()
        {
            // Randomise Sphere Grid and Base Stats
            { new GameState { RoomNumber = 376, Storyline = 5, MovementLock = 0x20}, RandoSetupTransition },

            // Edit Quick Hit to be rank 1
            { new GameState { BattleState2 = 1 }, new RandoAbilityBalance { ForceLoad = false, Description = "Rebalancing Abilities"} },

            // Tutorial ability addition and removal
            { new GameState { RoomNumber = 63, Storyline = 52, MovementLock = 0x20}, new AddRikkuAbilitiesTransition { ForceLoad = false, Description = "Add Steal and Use" } },
            { new GameState { RoomNumber = 20, Storyline = 116, MovementLock = 0x20}, new RemoveRikkuAbilitiesTransition { ForceLoad = false, Description = "Remove Steal and Use" } },
            { new GameState { RoomNumber = 69, Storyline = 210, MovementLock = 0x20}, new AddLuluAbilitiesTransition { ForceLoad = false, Description = "Add Fire, Thunder, Water & Blizzard" } },
            { new GameState { RoomNumber = 67, Storyline = 214, MovementLock = 0x20}, new RemoveLuluAbilitiesTransition { ForceLoad = false, Description = "Remove Fire, Thunder, Water & Blizzard" } },
            { new GameState { RoomNumber = 22, Storyline = 217, MovementLock = 0x20}, new AddWakkaAbilitiesTransition { ForceLoad = false, Description = "Add Dark Attack" } },
            { new GameState { RoomNumber = 19, Storyline = 218, MovementLock = 0x20}, new RemoveWakkaAbilitiesTransition { ForceLoad = false, Description = "Remove Dark Attack" } },
            { new GameState { RoomNumber = 18, Storyline = 312, MovementLock = 0x20}, new AddKimahriAbilitiesTransition { ForceLoad = false, Description = "Add Lancet" } },
            { new GameState { RoomNumber = 18, Storyline = 315, MovementLock = 0x20}, new RemoveKimahriAbilitiesTransition { ForceLoad = false, Description = "Remove Lancet" } },
            { new GameState { RoomNumber = 109, Storyline = 1085, MovementLock = 0x20}, new AddRikkuAbilitiesTransition { ForceLoad = false, Description = "Add Steal and Use" } },
            { new GameState { RoomNumber = 97, Storyline = 1085, MovementLock = 0x20}, new RemoveRikkuAbilitiesTransition { ForceLoad = false, Description = "Remove Steal and Use" } },

            // Blitzball Randomisation
            { new GameState { RoomNumber = 72, Storyline = 518}, new RandoBlitzballTransition { Storyline = 520, Description = "Randomise Blitzball"} },

            // Shop Randomisations
            { new GameState { RoomNumber = 144, NPCLastInteraction = 39, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Besaid (Pre-Airship)", ConsoleOutput = false, shopID = 00, shopSize = 8, shopTier = 0, Repeatable = true} },
            { new GameState { RoomNumber = 16, NPCLastInteraction = 18, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Kilika (Pre-Airship)", ConsoleOutput = false, shopID = 01, shopSize = 10, shopTier = 0, Repeatable = true} },
            { new GameState { RoomNumber = 123, NPCLastInteraction = 27, MenuValue1 = 0x1000}, LucaShopsTransition},
            { new GameState { RoomNumber = 104, NPCLastInteraction = 13, MenuValue1 = 0x1000}, LucaShopsTransition},
            { new GameState { RoomNumber = 85, NPCLastInteraction = 3, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "O'aka Luca", ConsoleOutput = false, shopID = 03, shopSize = 6, shopTier = 0, Repeatable = true} },
            { new GameState { RoomNumber = 171, NPCLastInteraction = 27, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Mi'ihen Agency (Pre-Airship)", ConsoleOutput = false, shopID = 04, shopSize = 12, shopTier = 0, Repeatable = true} },
            { new GameState { RoomNumber = 79, NPCLastInteraction = 23, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "O'aka MRR Entrance", ConsoleOutput = false, shopID = 05, shopSize = 7, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 119, NPCLastInteraction = 15, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "O'aka MRR Command", ConsoleOutput = false, shopID = 06, shopSize = 9, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 210, NPCLastInteraction = 20, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Djose Temple (Pre-Airship)", ConsoleOutput = false, shopID = 07, shopSize = 12, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 235, NPCLastInteraction = 14, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Moonflow South - Thin Man (Before Airship)", ConsoleOutput = false, shopID = 08, shopSize = 5, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 235, NPCLastInteraction = 15, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Moonflow South - Woman in white strapless (Before Airship)", ConsoleOutput = false, shopID = 09, shopSize = 5, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 188, NPCLastInteraction = 10, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Moonflow South - Woman in green (Before Airship)", ConsoleOutput = false, shopID = 10, shopSize = 6, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 188, NPCLastInteraction = 11, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Moonflow South - Fat Man (Before Airship)", ConsoleOutput = false, shopID = 11, shopSize = 6, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 187, NPCLastInteraction = 9, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "O'aka Moonflow South", ConsoleOutput = false, shopID = 12, shopSize = 6, shopTier = 1, Repeatable = true} },
            { new GameState { RoomNumber = 172, NPCLastInteraction = 7, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "O'aka Guadosalam", ConsoleOutput = false, shopID = 13, shopSize = 13, shopTier = 2, Repeatable = true} },
            { new GameState { RoomNumber = 172, NPCLastInteraction = 16, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Guadosalam Shop (Pre-Airship)", ConsoleOutput = false, shopID = 14, shopSize = 14, shopTier = 2, Repeatable = true} },
            { new GameState { RoomNumber = 263, NPCLastInteraction = 22, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Thunder Plains Agency (Pre-Airship)", ConsoleOutput = false, shopID = 15, shopSize = 14, shopTier = 2, Repeatable = true} },
            { new GameState { RoomNumber = 221, NPCLastInteraction = 8, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "O'aka Macalania Woods", ConsoleOutput = false, shopID = 16, shopSize = 7, shopTier = 2, Repeatable = true} },
            { new GameState { RoomNumber = 215, NPCLastInteraction = 25, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Lake Macalania Agency (Pre-Airship)", ConsoleOutput = false, shopID = 17, shopSize = 14, shopTier = 2, Repeatable = true} },
            { new GameState { RoomNumber = 164, NPCLastInteraction = 8, MenuValue1 = 0x1000}, OakaMacalaniaShopTransition},
            { new GameState { RoomNumber = 106, NPCLastInteraction = 12, MenuValue1 = 0x1000}, OakaMacalaniaShopTransition},
            { new GameState { RoomNumber = 211, NPCLastInteraction = 12, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Rin Airship Lift (Escaping Home)", ConsoleOutput = false, shopID = 19, shopSize = 12, shopTier = 2, Repeatable = true} },
            { new GameState { RoomNumber = 223, NPCLastInteraction = 23, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Rin's Mobile Agency", ConsoleOutput = false, shopID = 20, shopSize = 7, shopTier = 3, Repeatable = true} },
            { new GameState { RoomNumber = 223, NPCLastInteraction = 94, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Calm Lands Agency (Pre-Airship)", ConsoleOutput = false, shopID = 21, shopSize = 14, shopTier = 3, Repeatable = true} },
            { new GameState { RoomNumber = 259, NPCLastInteraction = 10, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Gagazet Mountain Gate (Pre-Airship)", ConsoleOutput = false, shopID = 22, shopSize = 14, shopTier = 3, Repeatable = true} },
            { new GameState { RoomNumber = 244, NPCLastInteraction = 13, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Wantz Gagazet", ConsoleOutput = false, shopID = 23, shopSize = 14, shopTier = 3, Repeatable = true} },
            { new GameState { RoomNumber = 110, NPCLastInteraction = 15, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Wantz Macalania", ConsoleOutput = false, shopID = 24, shopSize = 14, shopTier = 4, Repeatable = true} },
            //{ new GameState { RoomNumber = 144, NPCLastInteraction = 39, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Besaid (Post-Airship)", ConsoleOutput = false, shopID = 25, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 98, NPCLastInteraction = 6, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Kilika (Post-Airship)", ConsoleOutput = false, shopID = 26, shopSize = 14, shopTier = 4, Repeatable = true} },
            //{ new GameState { RoomNumber = 104, NPCLastInteraction = 13, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Luca Square (Post-Airship)", ConsoleOutput = false, shopID = 27, shopSize = 14, shopTier = 4, Repeatable = true} },
            //{ new GameState { RoomNumber = 123, NPCLastInteraction = 27, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Luca Stadium (Post-Airship)", ConsoleOutput = false, shopID = 28, shopSize = 14, shopTier = 4, Repeatable = true} },
            //{ new GameState { RoomNumber = 171, NPCLastInteraction = 27, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Mi'ihen Agency (Post-Airship)", ConsoleOutput = false, shopID = 29, shopSize = 14, shopTier = 4, Repeatable = true} },
            //{ new GameState { RoomNumber = 210, NPCLastInteraction = 20, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Djose Temple (Post-Airship)", ConsoleOutput = false, shopID = 30, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 172, NPCLastInteraction = 16, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Guadosalam Shop (Post-Airship)", ConsoleOutput = false, shopID = 31, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 263, NPCLastInteraction = 22, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Thunder Plains Agency (Post-Airship)", ConsoleOutput = false, shopID = 32, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 215, NPCLastInteraction = 25, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Lake Macalania Agency (Post-Airship)", ConsoleOutput = false, shopID = 33, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 265, NPCLastInteraction = 9, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Rin (Post-Airship)", ConsoleOutput = false, shopID = 34, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 223, NPCLastInteraction = 94, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Calm Lands Agency (Post-Airship)", ConsoleOutput = false, shopID = 35, shopSize = 14, shopTier = 4, Repeatable = true} },
            { new GameState { RoomNumber = 259, NPCLastInteraction = 10, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Gagazet Mountain Gate (Post-Airship)", ConsoleOutput = false, shopID = 36, shopSize = 14, shopTier = 4, Repeatable = true} },
            //{ new GameState { RoomNumber = 265, NPCLastInteraction = 9, MenuValue1 = 0x1000}, new RandoShopTransition { ForceLoad = false, Description = "Monster Arena", ConsoleOutput = false, shopID = 37, shopSize = 7, shopTier = 4, Repeatable = true} }, // Not randomising monster arena
        };
    }
}
