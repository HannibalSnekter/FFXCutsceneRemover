﻿using System.Collections.Generic;

namespace FFXCutsceneRemover.Resources
{
    /* This class contains most of the transitions. Transitions added here are automatically evalutated in the main loop. */
    static class Transitions
    {
        static readonly AmmesTransition AmmesTransition = new AmmesTransition { ForceLoad = false, Description = "Sinspawn Ammes", Suspendable = false, Repeatable = true };
        static readonly DiveTransition DiveTransition = new DiveTransition { ForceLoad = false, Description = "Tidus falls into water", Suspendable = false, Repeatable = true };
        static readonly GeosTransition GeosTransition = new GeosTransition { ForceLoad = false, Description = "Geosgaeno", Suspendable = false, Repeatable = true };
        static readonly BoatTransition BoatTransition = new BoatTransition { ForceLoad = false, Description = "S.S Liki", Suspendable = false, Repeatable = true };
        static readonly SinFinTransition SinFinTransition = new SinFinTransition { ForceLoad = false, Description = "Pre Sin Fin", Suspendable = false, Repeatable = true };
        static readonly EchuillesTransition EchuillesTransition = new EchuillesTransition { ForceLoad = false, EnableYuna = 16, EnableKimahri = 16, EnableLulu = 16, Formation = new byte[] { 0x00, 0x04, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, Description = "Echuilles", Suspendable = false, Repeatable = true };
        static readonly GuiTransition GuiTransition = new GuiTransition { ForceLoad = false, Description = "Sinspawn Gui", Suspendable = false, Repeatable = true };
        static readonly SeymourTransition SeymourTransition = new SeymourTransition { ForceLoad = false, Description = "Pre-Seymour", Suspendable = false, Repeatable = true };
        static readonly UnderLakeTransition UnderLakeTransition = new UnderLakeTransition { ForceLoad = false, Description = "Under Macalania Lake", Suspendable = false, Repeatable = true };
        static readonly BikanelTransition BikanelTransition = new BikanelTransition {ForceLoad = false, Description = "Bikanel Desert", Suspendable = false, Repeatable = true};
        static readonly HomeTransition HomeTransition = new HomeTransition { ForceLoad = false, Description = "Home Fights", Suspendable = false, Repeatable = true };
        

        public static readonly Dictionary<IGameState, Transition> StandardTransitions = new Dictionary<IGameState, Transition>()
            {
            
            

            // SPECIAL
#if DEBUG
            { new GameState { Input = 2304, BattleState = 10 },  new Transition { BattleState = 778, ForceLoad = false, Description = "End any battle by holding start + select"} },
#endif
            { new GameState { RoomNumber = 348 }, new Transition { RoomNumber = 23, Description = "Skip Intro", Repeatable = true} },
            { new GameState { RoomNumber = 349 }, new Transition { RoomNumber = 23, Description = "Skip Intro", Repeatable = true} },
            // START OF ZANARKAND
            { new GameState { RoomNumber = 132, Storyline = 0 }, new Transition { RoomNumber = 368, Storyline = 3, SpawnPoint = 0, Description = "Beginning"} },
            { new GameState { RoomNumber = 368, Storyline = 3, Menu = 1, FangirlsOrKidsSkip = 1 }, new Transition { FangirlsOrKidsSkip = 3 , ForceLoad = false, Description = "Fangirls or kids, whichever Tidus talks to second"} },
            { new GameState { RoomNumber = 368, Storyline = 3, Menu = 1, FangirlsOrKidsSkip = 2 }, new Transition { FangirlsOrKidsSkip = 3 , ForceLoad = false, Description = "Fangirls or kids, whichever Tidus talks to second"} },
            { new GameState { RoomNumber = 368, Storyline = 4 }, new Transition { RoomNumber = 376, Storyline = 4, SpawnPoint = 0, Description = "Tidus leaves fans"} },
            { new GameState { RoomNumber = 376, Storyline = 4 }, new Transition { RoomNumber = 376, Storyline = 5, SpawnPoint = 0, Description = "Tidus looks at Jecht sign"} },
            { new GameState { RoomNumber = 371, Storyline = 5 }, new Transition { RoomNumber = 370, Storyline = 7, SpawnPoint = 0, Description = "Tidus navigates through the crowd and Blitzball FMV"} },
            { new GameState { RoomNumber = 370, Storyline = 7 }, new AuronTransition {ForceLoad = false, Description = "Tidus wakes up and follows Auron", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 366, Storyline = 8 }, new Transition { RoomNumber = 389, Storyline = 12, SpawnPoint = 0, Description = "Tidus sees the fayth and follows Auron" } },

            { new GameState { RoomNumber = 389, Storyline = 13 }, AmmesTransition },
            { new GameState { RoomNumber = 389, Storyline = 14 }, AmmesTransition },
            { new GameState { RoomNumber = 389, Storyline = 15 }, AmmesTransition },
            { new GameState { RoomNumber = 389, Storyline = 16 }, AmmesTransition },

            //{ new GameState { RoomNumber = 389, Storyline = 14 }, new Transition { RoomNumber = 389, Storyline = 15, SpawnPoint = 0, Description = "Sinspawn Ammes?" } }, // NOT WORKING - This CS ends with a battle.
            { new GameState { RoomNumber = 367, Storyline = 16 }, new Transition { RoomNumber = 367, Storyline = 18, SpawnPoint = 0, Description = "Tidus sees Jecht sign again" } },
            { new GameState { RoomNumber = 367, Storyline = 18 }, new TankerTransition {ForceLoad = false, Description = "Tanker", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 367, Storyline = 19 }, new Transition { RoomNumber = 367, Storyline = 20, SpawnPoint = 0, Description = "Tidus and Auron run as bridge explodes"} },
            { new GameState { RoomNumber = 367, Storyline = 20 }, new Transition { RoomNumber = 384, Storyline = 20, SpawnPoint = 0, Description = "This is your story FMV" } },
                                                            // Tidus wakes up inside Sin
            { new GameState { RoomNumber = 384, Storyline = 20, State = 0 }, new Transition { RoomNumber = 385, Description = "Tidus swimming looking at himself as a child"} },
            { new GameState { RoomNumber = 385, Storyline = 20 }, new Transition { RoomNumber = 48, Storyline = 30, SpawnPoint = 0, Description = "A dream of being alone" } },
            // END OF ZANARKAND
            // START OF BAAJ TEMPLE
            { new GameState { RoomNumber = 48, Storyline = 30 }, new Transition { RoomNumber = 48, Storyline = 42, SpawnPoint = 0, Description = "Tidus wakes up"} }, // Bug: Tidus is walking in water; should be swimming
            //{ new GameState { RoomNumber = 49, Storyline = 42 }, DiveTransition }, // Currently doesn't work because Tidus is unable to move after the skip. Need a way to change movement lock value.
            //{ new GameState { RoomNumber = 49, Storyline = 44 }, DiveTransition }, //
            { new GameState { RoomNumber = 49, Storyline = 44 }, GeosTransition },
            { new GameState { RoomNumber = 49, Storyline = 46 }, GeosTransition },
            { new GameState { RoomNumber = 49, Storyline = 48 }, new Transition { RoomNumber = 50, Storyline = 48, SpawnPoint = 0, Description = "Escape from Geogaesno "} },
            { new GameState { RoomNumber = 50, Storyline = 48 }, new Transition { RoomNumber = 50, Storyline = 50, SpawnPoint = 0, Description = "Tidus in a collapsed corridor"} }, // Bug: Boss music still playing
            { new GameState { RoomNumber = 63, Storyline = 50 }, new Transition { RoomNumber = 63, Storyline = 52, SpawnPoint = 1, Description = "Tidus needs fire"} },
            { new GameState { RoomNumber = 63, Storyline = 54 }, new Transition { RoomNumber = 63, Storyline = 55, SpawnPoint = 0, Description = "Tidus makes fire"} },
            { new GameState { RoomNumber = 165, Storyline = 55 }, new Transition { RoomNumber = 63, Storyline = 55, SpawnPoint = 0, Description = "Tidus has a dream about Auron"} },
            { new GameState { RoomNumber = 63, Storyline = 55 }, new KlikkTransition {ForceLoad = false, Description = "Klikk", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 63, Storyline = 58 }, new Transition { RoomNumber = 71, Storyline = 60, SpawnPoint = 0, Description = "Rikku punches Tidus"} },
            { new GameState { RoomNumber = 71, Storyline = 60 }, new Transition { RoomNumber = 71, Storyline = 66, SpawnPoint = 0, BaajFlag1 = 1, Description = "Tidus wakes up on boat + Sphere Grid tutorial"} },
            { new GameState { RoomNumber = 71, Storyline = 66 }, new AlBhedBoatTransition {ForceLoad = false, Description = "Rikku explains the mission", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 64, Storyline = 70 }, new Transition { RoomNumber = 64, Storyline = 74, SpawnPoint = 0, Description = "They enter the submerged ruins" } },
                                            // Tidus bashes the console
            { new GameState { RoomNumber = 64, Storyline = 76 }, new UnderwaterRuinsTransition {ForceLoad = false, Description = "Underwater Ruins", Suspendable = false, Repeatable = true} }, //Needs work
                                            // Tidus bashes the machine + Tros arrives
                                            // They leave the submerged ruins
            //{ new GameState { RoomNumber = 380, Storyline = 84 }, new UnderwaterRuinsTransition2 {ForceLoad = false, Description = "Underwater Ruins Outside", Suspendable = false, Repeatable = true} }, // Lights come on in submerged ruins
            { new GameState { RoomNumber = 380, Storyline = 84, State = 0 }, new Transition { RoomNumber = 71, Storyline = 90, SpawnPoint = 0, Description = "Airship is shown" } },
            { new GameState { RoomNumber = 71, Storyline = 90, State = 1 }, new Transition { RoomNumber = 71, Storyline = 100, SpawnPoint = 0, RikkuName = new byte[]{0x61, 0x78, 0x7A, 0x7A, 0x84, 0x0}, Description = "Tidus gets back onto the boat"} },
            //{ new GameState { RoomNumber = 71, Storyline = 100 }, new Transition { TargetActorID = 1, Target_x = -40.772f, Target_y = 0.0f, Target_z = -20.171f, ConsoleOutput = false, Description = "Reposition Tidus"} },
            { new GameState { RoomNumber = 71, Storyline = 100, State = 1 }, new Transition { RoomNumber = 70, Storyline = 110, Description = "Rikku suggests going to Luca"} },                                     
            // END OF BAAJ TEMPLE
            // START OF BESAID
            //{ new GameState { RoomNumber = 70, Storyline = 111, CutsceneAlt = 16}, new Transition {Description = "Skip Save Prompt"} }, // For use with BeachTransition
            { new GameState { RoomNumber = 70, Storyline = 111 }, new Transition { Storyline = 113, SpawnPoint = 0, Description = "Tidus wakes up in the sea"} }, //Bug(Minor): Aurochs don't spawn on the beach
            //{ new GameState { RoomNumber = 70, Storyline = 111}, new BeachTransition {ForceLoad = false, Description = "Tidus wakes up in the sea", Suspendable = false, Repeatable = true} }, // Needs work to be used
            //{ new GameState { RoomNumber = 41, Storyline = 119}, new LagoonTransition {ForceLoad = false, Description = "Lagoon", Suspendable = false, Repeatable = true} }, // Causes menu to be unable to open and menu crashing
            { new GameState { RoomNumber = 41, Storyline = 119, CutsceneAlt = 73 }, new Transition { RoomNumber = 67, Storyline = 124, Description = "Wakka asks Tidus to join his team"} },
            { new GameState { RoomNumber = 67, Storyline = 124 }, new Transition { RoomNumber = 69, Storyline = 130, SpawnPoint = 0, Description = "Wakka explains his life story"} },
            { new GameState { RoomNumber = 133, Storyline = 130, }, new Transition { RoomNumber = 17, Storyline = 134, SpawnPoint = 3, Description = "Tidus arrives at Besaid Village" } },
            { new GameState { RoomNumber = 84, Storyline = 134 }, new Transition { RoomNumber = 84, Storyline = 136, SpawnPoint = 0, Description = "Tidus enters the temple"} },
            { new GameState { RoomNumber = 84, Storyline = 136, State = 1 }, new Transition { Storyline = 140, Description = "Tidus speaks to the priest" } },
            //{ new GameState { RoomNumber = 145, Storyline = 140, Dialogue1 = 4, DialogueOption = 0 }, new Transition { Storyline = 154, Description = "Priest enters Wakka's tent" } },
            { new GameState { RoomNumber = 191, Storyline = 152 }, new Transition { RoomNumber = 145, Storyline = 154, SpawnPoint = 0, Description = "Tidus dreams about a flashback"} },
            { new GameState { RoomNumber = 42, Storyline = 154, State = 1}, new Transition { RoomNumber = 122, Storyline = 162, Description = "Tidus goes back into the temple"} },
            { new GameState { RoomNumber = 122, Storyline = 162, State = 1, BesaidFlag1 = 8}, new Transition { RoomNumber = 103, Storyline = 164, Description = "Wakka catches up with Tidus in trials"} },
            { new GameState { RoomNumber = 103, Storyline = 164}, new Transition { RoomNumber = 42, Storyline = 170, EnableValefor = 17, Description = "Tidus meets Lulu and Kimahri + FMV "} },
            { new GameState { RoomNumber = 42, Storyline = 170 }, new Transition { RoomNumber = 42, Storyline = 172, SpawnPoint = 0, Description = "The gang leave the cloister of trials"} },
            { new GameState { RoomNumber = 83, Storyline = 172, State = 1 }, new ValeforTransition {ForceLoad = false, Description = "Naming Valefor", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 68, Storyline = 184 }, new Transition { RoomNumber = 252, Storyline = 190, Description = "Tidus sleeping"} },
            { new GameState { RoomNumber = 252, Storyline = 190, State = 1 }, new Transition { RoomNumber = 60, Storyline = 196, Description = "Tidus has a dream about Yuna, Tidus wakes up + FMV" } },
                                            // Tidus wakes up again (Party healed at this point)
            { new GameState { RoomNumber = 17, Storyline = 200 }, new BrotherhoodTransition { RoomNumber = 69, Storyline = 210, SpawnPoint = 515, EnableYuna = 17, EnableLulu = 17, TidusWeaponDamageBoost = 5, Description = "Yuna says goodbye to Besaid" } },
            { new GameState { RoomNumber = 67, Storyline = 210 }, new Transition { RoomNumber = 67, Storyline = 214, SpawnPoint = 3, Description = "Yuna says goodbye to Besaid again"} },
            { new GameState { RoomNumber = 21, Storyline = 214 }, new KimahriTransition {ForceLoad = false, Description = "Kimahri", Formation = new byte[] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 19, Storyline = 218, State = 0 }, new Transition { RoomNumber = 301, Storyline = 220, Description = "S.S. Liki departs" } },
            // END OF BESAID
            // START OF SS LIKI
            { new GameState { RoomNumber = 301, Storyline = 220 }, new Transition { RoomNumber = 301, Storyline = 228, SpawnPoint = 256, Description = "Tidus goofing around" } },
            { new GameState { RoomNumber = 301, Storyline = 240, SSWinnoFlag1 = 0x08 }, new Transition { RoomNumber = 301, PositionTidusAfterLoad = true, TargetActorID = 1, Target_x = 3.932615995f, Target_y = -49.99765015f, Target_z = 189.9013977f, Target_rot = 1.676406384f, Description = "Lord Braska's daughter?" } },
            { new GameState { RoomNumber = 301, Storyline = 240, SSWinnoFlag1 = 0x28 }, new Transition { RoomNumber = 301, Storyline = 242, PositionTidusAfterLoad = true, TargetActorID = 1, Target_x = -29.5f, Target_y = -49.99765015f, Target_z = 86.0f, Target_rot = -0.2413983196f, Description = "She's the daughter of High Summoner Braska" } },
            { new GameState { RoomNumber = 61, Storyline = 244, SSWinnoFlag1 = 0xA8  }, new Transition {Storyline = 248, Description = "Tidus talks to Yuna" } },
            { new GameState { RoomNumber = 61, Storyline = 248 }, SinFinTransition},
            { new GameState { RoomNumber = 61, Storyline = 260 }, SinFinTransition},
            { new GameState { RoomNumber = 282, Storyline = 272 }, EchuillesTransition },
            { new GameState { RoomNumber = 282, Storyline = 280 }, EchuillesTransition },
            { new GameState { RoomNumber = 220, Storyline = 287 }, new Transition { RoomNumber = 139, Storyline = 290, Description = "Recovering on the boat" } },
            //{ new GameState { RoomNumber = 139, Storyline = 290 }, new Transition { RoomNumber = 43, Storyline = 292, KilikaMapFlag = 0x00, Description = "Map shown"} }, Removed and assimilated into "Undocking in Kilika" due to crashes
            // END OF SS LIKI
            // START OF KILIKA
            { new GameState { RoomNumber = 139, Storyline = 290 }, new Transition { RoomNumber = 43, Storyline = 294, SpawnPoint = 0, /*KilikaMapFlag = 0x01,*/ EnableYuna = 17, EnableKimahri = 17, EnableLulu = 17, Formation = new byte[]{0x05, 0x04, 0x00, 0x01, 0x03, 0xFF, 0xFF, 0xFF}, Description = "Undocking in Kilika" } },
            { new GameState { RoomNumber = 53, Storyline = 294, State = 0 }, new Transition { RoomNumber = 152, Storyline = 300, Description = "Sending" } },
            { new GameState { RoomNumber = 152, Storyline = 300 }, new Transition { RoomNumber = 152, Storyline = 302, SpawnPoint = 0, FullHeal = true, Description = "Tidus wakes up" } },
            { new GameState { RoomNumber = 16, Storyline = 304, State = 1 }, new Transition { Storyline = 308, SpawnPoint = 2, Description = "Tidus speaks to Wakka"} },
            { new GameState { RoomNumber = 18, Storyline = 308 }, new Transition { RoomNumber = 18, Storyline = 312, SpawnPoint = 0, Description = "Camera pan + Yuna wants a new guardian" } },
            { new GameState { RoomNumber = 65, Storyline = 315 }, new Transition { RoomNumber = 65, Storyline = 322, SpawnPoint = 0, Description = "Race up the stairs"} },
            { new GameState { RoomNumber = 65, Storyline = 322}, new GeneauxTransition {ForceLoad = false, Description = "Pre-Geneaux", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 65, Storyline = 326, State = 1 }, new Transition { RoomNumber = 78, Storyline = 328, SpawnPoint = 1, Description = "No replacement for Chappu"} },
            { new GameState { RoomNumber = 78, Storyline = 328 }, new Transition { RoomNumber = 78, Storyline = 330, SpawnPoint = 1, Description = "Arrival at temple"} },
            { new GameState { RoomNumber = 96, Storyline = 330 }, new Transition { RoomNumber = 96, Storyline = 335, SpawnPoint = 0, Description = "Camera pan in Kilika Temple + pray" } },
            { new GameState { RoomNumber = 44, Storyline = 335 }, new Transition { RoomNumber = 108, Storyline = 340, SpawnPoint = 0, Description = "Tidus is denied access" } },
            //{ new GameState { RoomNumber = 108, Storyline = 340}, new KilikaTrialsTransition {ForceLoad = false, ConsoleOutput = false, Description = "Camera Pan", Suspendable = false, Repeatable = true} }, // Camera pan inside the trials -Doesn't work yet
            { new GameState { RoomNumber = 45, Storyline = 340 }, new Transition { Storyline = 346, Description = "Guardians are annoyed at Tidus + Fayth explanation"} },
            { new GameState { RoomNumber = 45, Storyline = 346, State = 1 }, new IfritTransition {ForceLoad = false, Description = "Naming Ifrit", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 78, Storyline = 348, State = 1 }, new Transition { RoomNumber = 18, Storyline = 360, SpawnPoint = 1, Description = "Tidus misses home" } },
            { new GameState { RoomNumber = 16, Storyline = 360, State = 1 }, new Transition { RoomNumber = 94, Storyline = 370, SpawnPoint = 256, Description = "Setting off to Luca"} },
            // END OF KILIKA
            // START OF SS WINNO
            { new GameState { RoomNumber = 94, Storyline = 370 }, new Transition { RoomNumber = 167, Storyline = 372, SSWinnoFlag2 = 1, Description = "Opening scenes"} },
            { new GameState { RoomNumber = 237, Storyline = 372, SSWinnoFlag2 = 1 }, new Transition { RoomNumber = 237, Storyline = 372, SSWinnoFlag1 = 170, SSWinnoFlag2 = 9, Description = "Meet O'aka"} },
            { new GameState { RoomNumber = 94, Storyline = 380, SSWinnoFlag2 = 25 }, new Transition { Storyline = 380, SSWinnoFlag2 = 31, SpawnPoint = 2, Description = "Eavesdropping on Lulu and Wakka"} },
            { new GameState { RoomNumber = 94, Storyline = 385 }, new Transition { RoomNumber = 191, SpawnPoint = 256, Description = "Tidus looks at the blitzball"} },
            { new GameState { RoomNumber = 191, Storyline = 385 }, new Transition { RoomNumber = 94, Storyline = 387, SpawnPoint = 0, Description = "Zanarkand flashback"} },
                                            // Tidus fails Jecht shot + Yuna arrives
            { new GameState { RoomNumber = 94, Storyline = 395, State = 0 }, new Transition { RoomNumber = 267, Storyline = 402, FullHeal = true, Description = "Tidus speaks to Yuna"} },
            // END OF WINNO
            // START OF LUCA
            { new GameState { RoomNumber = 267, Storyline = 402 }, new Transition { RoomNumber = 377, Storyline = 404, Description = "Luca FMV + Kilika Beasts undock"} },
            { new GameState { RoomNumber = 377, Storyline = 404 }, new Transition { RoomNumber = 267, Storyline = 405, Description = "Inside the Cafe"} },
            { new GameState { RoomNumber = 267, Storyline = 405 }, new Transition { RoomNumber = 267, Storyline = 425, SpawnPoint = 2, Description = "Besaid Aurochs undock"} },
            { new GameState { RoomNumber = 268, Storyline = 427, State = 0 }, new Transition { RoomNumber = 355, Storyline = 430, Description = "Seymour arrives" } },
            { new GameState { RoomNumber = 72, Storyline = 430, State = 0 }, new Transition { Storyline = 440, SpawnPoint = 1797, Description = "Yuna enters the changing room"} },
            { new GameState { RoomNumber = 72, Storyline = 440, State = 0 }, new Transition { RoomNumber = 123, Storyline = 450, SpawnPoint = 4, Description = "Speaking to the Al Bhed"} },
            { new GameState { RoomNumber = 123, Storyline = 450, LucaFlag = 0}, new Transition { LucaFlag = 8, ForceLoad = false, Description = "Camera pan"} },
            { new GameState { RoomNumber = 77, Storyline = 450 }, new Transition { Storyline = 455, SpawnPoint = 1, Description = "Crowd mob Yuna"} },
            { new GameState { RoomNumber = 104, Storyline = 455, LucaFlag2 = 0}, new Transition { LucaFlag2 = 2, ForceLoad = false, Description = "Tidus and Yuna talk about Luca"} },
            { new GameState { RoomNumber = 159, Storyline = 455 }, new Transition { RoomNumber = 57, Storyline = 484, Description = "Tidus and Yuna at the cafe"} },
            { new GameState { RoomNumber = 57, Storyline = 484 }, new Transition { RoomNumber = 121, Storyline = 486, Description = "Mika begins the tournament"} },
            { new GameState { RoomNumber = 121, Storyline = 486 }, new Transition { RoomNumber = 159, Storyline = 488, Description = "Al Bhed Auroch game starts"} },
            { new GameState { RoomNumber = 159, Storyline = 488 }, new Transition { RoomNumber = 104, Storyline = 490, Description = "Kimahri Yuna's gone"} },
            { new GameState { RoomNumber = 104, Storyline = 490 }, new Transition { RoomNumber = 77, Storyline = 492, SpawnPoint = 1, EnableYuna = 16, EnableWakka = 16, Formation = new byte[]{0x5, 0x0, 0x3, 0xFF, 0xFF}, Description = "Lulu explains the situation" } },
                                            // Machina fights
                                            // Looking at the scoreboard
            { new GameState { RoomNumber = 121, Storyline = 492 }, new Transition { RoomNumber = 88, Storyline = 500, LucaFlag = 9, SpawnPoint = 258, Description = "Wakka takes a beating"} },
            { new GameState { RoomNumber = 299, Storyline = 502 }, new Transition { RoomNumber = 113, Storyline = 502, Description = "They jump on the boat"} },
            { new GameState { RoomNumber = 113, Storyline = 502}, new OblitzeratorTransition {ForceLoad = false, Description = "Oblitzerator", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 121, Storyline = 508 }, new Transition { RoomNumber = 88, Storyline = 514, SpawnPoint = 0, Description = "Aurochs win the game" } },
            { new GameState { RoomNumber = 72, Storyline = 514  }, new Transition { Storyline = 518, LucaFlag = 11, SpawnPoint = 1797, Description = "Wakka is injured"} },
            { new GameState { RoomNumber = 72, Storyline = 518, State = 0  }, new Transition { Storyline = 520, Description = "Wakka subs himself"} },
            { new GameState { RoomNumber = 72, Storyline = 520  }, new Transition { RoomNumber = 124, Storyline = 535, Description = "Lulu speaks to Wakka" } },
            //{ new GameState { RoomNumber = 124, Storyline = 535}, new BlitzballTransition1 {ForceLoad = false, Description = "Pre-Blitzball", Suspendable = false, Repeatable = true} },
            //{ new GameState { RoomNumber = 124, Storyline = 535  }, new Transition { RoomNumber = 62, Description = "Pre-Blitzball" } }, // Bug: After the first half, everyone learns loads of techniques for some reason
            { new GameState { RoomNumber = 72, Storyline = 540}, new Transition { RoomNumber = 347, Storyline = 560, Description = "Halftime talk"} },
            { new GameState { RoomNumber = 124, Storyline = 560}, new Transition { RoomNumber = 250, Storyline = 565, Description = "Fans are getting impatient"} },
            { new GameState { RoomNumber = 250, Storyline = 565}, new Transition { RoomNumber = 124, Storyline = 575, Description = "Wakka chants"} },
            { new GameState { RoomNumber = 124, Storyline = 575}, new BlitzballTransition2 {ForceLoad = false, Description = "Ah! It's Wakka!", Suspendable = false, Repeatable = true} },
            //{ new GameState { RoomNumber = 124, Storyline = 575 }, new Transition { RoomNumber = 62, Description = "Wakka joins the game"} }, // Bug: Tidus is still in the team, need to sub Wakka in somehow
            { new GameState { RoomNumber = 250, Storyline = 582 }, new Transition { RoomNumber = 125, Storyline = 583, Description = "Aurochs win/lose the game"} },
            { new GameState { RoomNumber = 125, Storyline = 583}, new SahaginTransition {ForceLoad = false, Description = "Pre-Sahagins", Formation = new byte[]{0x04, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF}, Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 57, Storyline = 588}, new Transition {Storyline = 600, Description = "Lulu what's happening"}},
            { new GameState { RoomNumber = 57, Storyline = 600}, new GarudaTransition {ForceLoad = false, Description = "Vouivre to Anima", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 104, Storyline = 610  }, new Transition { RoomNumber = 107, Storyline = 615, Description = "Wakka quits the Aurochs" } },
            { new GameState { RoomNumber = 107, Storyline = 615  }, new Transition { RoomNumber = 89, Storyline = 616, Description = "Wakka joins Yuna"} },
            { new GameState { RoomNumber = 89, Storyline = 616  }, new Transition { Storyline = 617, SpawnPoint = 1, EnableAuron = 17, EnableYuna = 17, EnableKimahri = 17, EnableLulu = 17, Formation = new byte[]{0x00, 0x04, 0x01, 0x02, 0x03, 0x05, 0xFF, 0xFF}, FullHeal = true, Description = "Tidus shouts at Auron"} },
            { new GameState { RoomNumber = 107, Storyline = 617 }, new Transition { Storyline = 630, SpawnPoint = 0, LucaFlag = 15, Description = "Tidus and Auron join the group"} },
            { new GameState { RoomNumber = 107, Storyline = 630, State = 0 }, new Transition { RoomNumber = 95, Storyline = 730, SpawnPoint = 256, Description = "HA HA HA HA"} },
            // END OF LUCA
            // START OF MI'IHEN
            { new GameState { RoomNumber = 95, Storyline = 730 }, new Transition { Storyline = 734, MiihenFlag1 = 5, MiihenFlag2 = 4, /*PositionTidusAfterLoad = true, Target_x = -3.003187895f, Target_y = 0.0f, Target_z = -8.161786079f, Target_rot = 1.576849937f,*/ Description = "Tidus runs up the stairs"} },
            { new GameState { RoomNumber = 120, Storyline = 734, MiihenFlag1 = 133}, new Transition { MiihenFlag1 = 141, ForceLoad = false, Description = "Meet Calli"} },
            { new GameState { RoomNumber = 127, Storyline = 734, MiihenFlag1 = 141 }, new Transition { MiihenFlag1 = 221, MiihenFlag2 = 148, ForceLoad = false, Description = "Luzzu, Gatta and Shelinda scenes"} },
            { new GameState { RoomNumber = 58, Storyline = 734, State = 0 }, new Transition { RoomNumber = 171, Storyline = 755, Description = "Auron is tired"} },
            { new GameState { RoomNumber = 112, Storyline = 755 }, new Transition { RoomNumber = 171, Storyline = 760, SpawnPoint = 0, FullHeal = true, Description = "Tidus chats with Yuna" } },
                                            // Tidus chats to a guy
            { new GameState { RoomNumber = 171, Storyline = 762}, new RinTransition {ForceLoad = false, Description = "Meet Rin", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 58, Storyline = 767, MiihenFlag3 = 0 }, new Transition { MiihenFlag3 = 1 , ForceLoad = false, Description = "To the chocobo corral"} },
            { new GameState { RoomNumber = 58, Storyline = 767, MiihenFlag3 = 1 }, new ChocoboEaterTransition {ForceLoad = false, Description = "Chocobo Eater", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 58, Storyline = 770, MiihenFlag3 = 3 }, new Transition { RoomNumber = 115, Storyline = 772, SpawnPoint = 515, Description = "Chocobo Eater loss - Fall down the cliff"} },
            { new GameState { RoomNumber = 58, Storyline = 770, MiihenFlag3 = 5 }, new Transition { Storyline = 772, SpawnPoint = 2, MiihenFlag3 = 13, Description = "Chocobo Eater win - Rin thanks the party"} },
            { new GameState { RoomNumber = 116, Storyline = 772, MiihenFlag4 = 0}, new Transition { MiihenFlag4 = 4 , ForceLoad = false, Description = "Luzzu and Gatta move a cart"} },
            { new GameState { RoomNumber = 59, Storyline = 777, State = 0}, new Transition { Storyline = 787, SpawnPoint = 3, Description = "Seymour helps out"} },
            // END OF MI'IHEN
            // START OF MUSHROOM ROCK ROAD
            { new GameState { RoomNumber = 79, Storyline = 787 }, new Transition { RoomNumber = 79, Storyline = 825, SpawnPoint = 0, Description = "Tidus distrusts Seymour"} },
            { new GameState { RoomNumber = 119, Storyline = 825 }, new Transition { Storyline = 845, MRRFlag1 = 0x02, MRRFlag2 = 0x01, Description = "Preparing for Sin" } },
            { new GameState { RoomNumber = 119, Storyline = 845 }, GuiTransition },
            { new GameState { RoomNumber = 119, Storyline = 850 }, GuiTransition },
            { new GameState { RoomNumber = 119, Storyline = 857 }, GuiTransition },
            { new GameState { RoomNumber = 119, Storyline = 857, BattleState = 522, CutsceneAlt = 1 }, new Transition { Storyline = 860, Description = "Post-Sinspawn Gui + FMV" } },
            { new GameState { RoomNumber = 119, Storyline = 860 }, GuiTransition },
            { new GameState { RoomNumber = 247, Storyline = 865 }, GuiTransition },
                                            // Post-Sinspawn Gui 2
            { new GameState { RoomNumber = 254, Storyline = 882 }, new Transition { RoomNumber = 254, Storyline = 893, EnableSeymour = 16, Description = "Tidus wakes up + sees Gatta"} },
            { new GameState { RoomNumber = 254, Storyline = 893 }, new Transition { RoomNumber = 247, Storyline = 899, Description = "Sin FMV + Tidus chases after Sin"} },
            { new GameState { RoomNumber = 247, Storyline = 899 }, new Transition { RoomNumber = 218, Storyline = 902, Description = "Yuna tries to summon"} },
            { new GameState { RoomNumber = 218, Storyline = 902 }, new Transition { RoomNumber = 341, Storyline = 910, Description = "Tidus is swimming"} },
            { new GameState { RoomNumber = 341, Storyline = 910 }, new Transition { RoomNumber = 134, Storyline = 910, Description = "Nucleus"} },
            { new GameState { RoomNumber = 134, Storyline = 910 }, new Transition { RoomNumber = 131, Storyline = 910, Description = "Zanarkand flashback"} },
            { new GameState { RoomNumber = 131, Storyline = 910 }, new Transition { RoomNumber = 131, Storyline = 922, SpawnPoint = 3, Description = "Tidus monologue on beach"} },
            { new GameState { RoomNumber = 131, Storyline = 922, State = 0 }, new Transition { RoomNumber = 131, Storyline = 928, SpawnPoint = 3, Description = "Kinoc retreats"} },
            { new GameState { RoomNumber = 131, Storyline = 928, State = 0 }, new Transition { RoomNumber = 131, Storyline = 938, SpawnPoint = 0, Description = "Tidus speaks to Auron"} },
            { new GameState { RoomNumber = 131, Storyline = 938}, new AftermathTransition {ForceLoad = false, Description = "Leaving Mushroom Rock Road", Suspendable = false, Repeatable = true } },
            // END OF MRR
            // START OF DJOSE HIGHROAD
            { new GameState { RoomNumber = 93, Storyline = 960 }, new Transition { RoomNumber = 93, Storyline = 961, SpawnPoint = 768, Description = "Kimahri speaks"} },
            { new GameState { RoomNumber = 93, Storyline = 961, CutsceneAlt = 1678 }, new Transition { RoomNumber = 76, Storyline = 962, SpawnPoint = 1, Description = "Tidus is eager to go to Zanarkand"} },
            { new GameState { RoomNumber = 76, Storyline = 962 }, new Transition { Storyline = 970, SpawnPoint = 0, Description = "Tidus whoa"} },
            { new GameState { RoomNumber = 82, Storyline = 970 }, new Transition { Storyline = 971, SpawnPoint = 0, Description = "Arrival at Djose Temple"} },
            { new GameState { RoomNumber = 81, Storyline = 971 }, new Transition { Storyline = 985, SpawnPoint = 0, Description = "Meet Isaaru"} },
            { new GameState { RoomNumber = 214, Storyline = 990 }, new Transition { RoomNumber = 214, Storyline = 995, SpawnPoint = 0, Description = "Entering the Djose trials"} },
            //{ new GameState { RoomNumber = 90, Storyline = 998, State = 0 }, new Transition { RoomNumber = 90, Storyline = 1000, SpawnPoint = 0, Description = "Yuna enters the chamber"} }, : Bug: Spawned out of the cutscene
            { new GameState { RoomNumber = 90, Storyline = 998}, new DjoseTransition {ForceLoad = false, Description = "Djose - Antechamber", Suspendable = false, Repeatable = true } },
            { new GameState { RoomNumber = 91, Storyline = 1003}, new IxionTransition { Description = "Naming Ixion", Suspendable = false, Repeatable = true } },
            { new GameState { RoomNumber = 82, Storyline = 1005 }, new Transition { RoomNumber = 82, Storyline = 1010, SpawnPoint = 4, Description = "Tidus wakes up"} },
            { new GameState { RoomNumber = 82, Storyline = 1015, State = 0 }, new Transition { RoomNumber = 82, Storyline = 1020, SpawnPoint = 2, Description = "Yuna has bed hair"} },
            { new GameState { RoomNumber = 76, Storyline = 1020, State = 0 }, new Transition { RoomNumber = 76, Storyline = 1025, SpawnPoint = 0, Description = "Clasko wait for me"} },
            { new GameState { RoomNumber = 93, Storyline = 1028 }, new Transition { RoomNumber = 93, Storyline = 1030, SpawnPoint = 1, Description = "Moonflow here we come"} },
            // END OF DJOSE HIGHROAD
            // START OF MOONFLOW
            { new GameState { RoomNumber = 75, Storyline = 1030 }, new Transition { RoomNumber = 75, Storyline = 1032, MoonflowFlag = 1, Description = "Biran and Yenke taunt"} },
            { new GameState { RoomNumber = 105, Storyline = 1032, State = 0 }, new Transition { RoomNumber = 105, Storyline = 1040, SpawnPoint = 1, Description = "Tidus sees the Moonflow river"} },
            { new GameState { RoomNumber = 105, Storyline = 1040 }, new Transition { RoomNumber = 187, Storyline = 1045, SpawnPoint = 0, Description = "Tidus sees a shoopuf"} },
            { new GameState { RoomNumber = 235, Storyline = 1045, MoonflowFlag = 1}, new Transition { MoonflowFlag = 65, Description = "The chocobos cannot cross"} },
            { new GameState { RoomNumber = 291, Storyline = 1045}, new Transition { RoomNumber = 99, Storyline = 1045, Description = "Shoopuf launching + map"} },
            { new GameState { RoomNumber = 99, Storyline = 1045}, new Transition { RoomNumber = 291, Storyline = 1048, Description = "A sunken city"} },
            { new GameState { RoomNumber = 291, Storyline = 1048}, new Transition { RoomNumber = 99, Storyline = 1060, Description = "A debate about machina"} },
            //{ new GameState { RoomNumber = 99, Storyline = 1060}, new ExtractorTransition { Description = "Extractor", Suspendable = false, Repeatable = true } },
                                            // Post-Extractor
		    { new GameState { RoomNumber = 291, Storyline = 1060 }, new Transition { RoomNumber = 236, Storyline = 1070, SpawnPoint = 0, Description = "Back on the shoopuf"} },
            { new GameState { RoomNumber = 109, Storyline = 1070 }, new Transition { RoomNumber = 109, Storyline = 1085, SpawnPoint = 0, EnableRikku = 17, MoonflowFlag2 = 36, RikkuOutfit = 0, Description = "Reunite with Rikku + FMV"} },
            { new GameState { RoomNumber = 97, Storyline = 1085, MoonflowFlag2 = 36 }, new Transition { MoonflowFlag2 = 100, Description = "Map shown"} },
            // END OF MOONFLOW
            // START OF GUADOSALAM
            { new GameState { RoomNumber = 135, Storyline = 1085 }, new Transition { RoomNumber = 135, Storyline = 1096, SpawnPoint = 0, Description = "Meet Tromell + Customise tutorial"} },
            { new GameState { RoomNumber = 163, Storyline = 1096, State = 1}, new Transition { RoomNumber = 163, Storyline = 1104, SpawnPoint = 1, Description = "Tromell invites the gang in"} },
            { new GameState { RoomNumber = 141, Storyline = 1104}, new Transition { RoomNumber = 197, Description = "Seymour's house"} },
            { new GameState { RoomNumber = 197, Storyline = 1104}, new Transition { RoomNumber = 217, Storyline = 1118, Description = "Seymour proposes to Yuna"} },
            { new GameState { RoomNumber = 217, Storyline = 1118}, new Transition { RoomNumber = 163, Storyline = 1126, SpawnPoint = 1, Description = "Yuna drinks a glass of water"} },
            { new GameState { RoomNumber = 135, Storyline = 1126, State = 1}, new Transition { RoomNumber = 135, Storyline = 1132, SpawnPoint = 257, Description = "The gang discuss the proposal"} },
            { new GameState { RoomNumber = 257, Storyline = 1132}, new Transition { RoomNumber = 257, Storyline = 1138, SpawnPoint = 0, Description = "Tidus freaks out about the undead"} },
            { new GameState { RoomNumber = 257, Storyline = 1138}, new Transition { RoomNumber = 257, Storyline = 1154, SpawnPoint = 0, Description = "Auron and Rikku stay behind"} },
            { new GameState { RoomNumber = 257, Storyline = 1154, XCoordinate = 233.3042755f }, new Transition { RoomNumber = 193, Description = "Tidus enters the Farplane"} },
            { new GameState { RoomNumber = 134, Storyline = 1170}, new Transition { RoomNumber = 193, Storyline = 1172, Description = "Zanarkand flashback"} },
            { new GameState { RoomNumber = 193, Storyline = 1172}, new Transition { RoomNumber = 364, Storyline = 1176, Description = "Tidus is embarrassed"} },
            { new GameState { RoomNumber = 364, Storyline = 1176}, new Transition { RoomNumber = 175, Storyline = 1184, Description = "Jyscal returns"} },
            { new GameState { RoomNumber = 175, Storyline = 1184}, new Transition { RoomNumber = 135, Storyline = 1190, SpawnPoint = 258, Description = "Tidus asks about Jyscal"} },
            { new GameState { RoomNumber = 135, Storyline = 1190, State = 1}, new Transition { RoomNumber = 135, Storyline = 1194, SpawnPoint = 258, Description = "Affection scene"} },
            { new GameState { RoomNumber = 135, Storyline = 1194, State = 1}, new Transition { RoomNumber = 135, Storyline = 1196, SpawnPoint = 4, Description = "Tidus speaks to Shelinda"} },
            { new GameState { RoomNumber = 135, Storyline = 1196, State = 1}, new Transition { RoomNumber = 135, Storyline = 1200, Description = "Yuna asks Jyscal what she can do"} },
            { new GameState { RoomNumber = 135, Storyline = 1200}, new Transition { RoomNumber = 135, Storyline = 1210, SpawnPoint = 257, Description = "Macarena Temple"} },       
            // END OF GUADOSALAM
            // START OF THUNDER PLAINS
            { new GameState { RoomNumber = 140, Storyline = 1300}, new Transition { RoomNumber = 140, Storyline = 1310, SpawnPoint = 0, Description = "Map + Rikku afraid + tutorial"} },
            { new GameState { RoomNumber = 140, Storyline = 1310, CutsceneAlt = 2525}, new Transition { RoomNumber = 256, Storyline = 1310, SpawnPoint = 0, Description = "Rikku freaks out"} },
            { new GameState { RoomNumber = 256, Storyline = 1310}, new Transition { RoomNumber = 263, Storyline = 1315, SpawnPoint = 0, Description = "Rikku asks to go to the agency"} },
            { new GameState { RoomNumber = 264, Storyline = 1320}, new Transition { RoomNumber = 263, Storyline = 1325, FullHeal = true, Description = "Tidus barges in Yuna's room + Sleep"} },
            { new GameState { RoomNumber = 263, Storyline = 1335}, new Transition { RoomNumber = 256, Storyline = 1340, Description = "Leaving the agency"} },
            { new GameState { RoomNumber = 162, Storyline = 1340}, new Transition { RoomNumber = 162, Storyline = 1375, Description = "Yuna decides to marry Seymour"} },
            // END OF THUNDER PLAINS
		    // START OF MACALANIA
            { new GameState { RoomNumber = 110, Storyline = 1375}, new Transition { RoomNumber = 110, Storyline = 1400, Description = "Arriving at Macalania"} },
            { new GameState { RoomNumber = 110, Storyline = 1400, State = 0}, new Transition { RoomNumber = 110, Storyline = 1407, SpawnPoint = 5, Description = "Tidus is worried about Yuna"} },
            { new GameState { RoomNumber = 241, Storyline = 1407}, new Transition { RoomNumber = 241, Storyline = 1413, MacalaniaFlag = 32, Description = "Barthello has lost Dona + Butterfly guy"} },
            { new GameState { RoomNumber = 221, Storyline = 1413 }, new Transition { RoomNumber = 221, Storyline = 1420, SpawnPoint = 0, Description = "Pre-Spherimorph Auron Smash"} },
            { new GameState { RoomNumber = 248, Storyline = 1420 }, new SpherimorphTransition {ForceLoad = false , Description = "Spherimorph", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 164, Storyline = 1470 }, new TromellTransition {ForceLoad = false , Description = "Tromell leads Yuna away", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 102, Storyline = 1485 }, new CrawlerTransition {ForceLoad = false, Description = "Pre Crawler", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 106, Storyline = 1504 }, new Transition { Storyline = 1530, Description = "Jysscal Skip"} },
            { new GameState { RoomNumber = 80, Storyline = 1530}, SeymourTransition},
            { new GameState { RoomNumber = 80, Storyline = 1540}, SeymourTransition},
            { new GameState { RoomNumber = 239, Storyline = 1545 }, new Transition { Storyline = 1557, ForceLoad = false, Description = "Backflip Skip"} },
            { new GameState { RoomNumber = 102, Storyline = 1570 }, new WendigoTransition {ForceLoad = false, Description = "Wendigo", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 54, Storyline = 1600 }, UnderLakeTransition },
            { new GameState { RoomNumber = 54, Storyline = 1607 }, UnderLakeTransition },
            { new GameState { RoomNumber = 54, Storyline = 1610 }, UnderLakeTransition },
		    
		    // START OF BIKANEL
            { new GameState { RoomNumber = 136, Storyline = 1715 }, BikanelTransition  },
            { new GameState { RoomNumber = 136, Storyline = 1718, EnableRikku = 0, State = 1 }, new Transition
            {
                Storyline = 1720, SpawnPoint = 3, EnableRikku = 17, BikanelFlag = 32, Description = "Wakka Glare",
                Formation = new byte[]{ 0x0, 0x2, 0x5, 0x4, 0x3, 0x6, 0xFF }
            } },
            { new GameState { RoomNumber = 280, Storyline = 1820}, HomeTransition},
            { new GameState { RoomNumber = 280, Storyline = 1885 }, HomeTransition},
            { new GameState { Storyline = 1940, EncounterStatus = 89 }, new Transition { EncounterStatus = 88, ForceLoad = false, Description = "Disabling Encounters"} },
            { new GameState { RoomNumber = 261, Storyline = 1940 }, new Transition { RoomNumber = 194, Storyline = 1950, SpawnPoint = 1, Description = "Home to Airship"} },
            { new GameState { RoomNumber = 194, Storyline = 1990 }, new Transition { Storyline = 2000, SpawnPoint = 1, Description = "Airship Bridge Cutscene"} },
            { new GameState { RoomNumber = 351, Storyline = 2020 }, new Transition { Storyline = 2040, ForceLoad = false, Description = "Red carpet has teeth"} },
            { new GameState { RoomNumber = 277, Storyline = 2040 }, new EvraeTransition {ForceLoad = false, Description = "Pre Evrae", Suspendable = false, Repeatable = true} },
		    // END OF HOME
		    // START OF BEVELLE
		    { new GameState { RoomNumber = 205, Storyline = 2060, MusicId = 181 }, new Transition { Storyline = 2075, SpawnPoint = 0, Description = "Evrae to Guards"} },
            { new GameState { RoomNumber = 205, Storyline = 2085}, new Transition { RoomNumber = 180, Storyline = 2135, Description = "Bevelle Guards to Trials"} },
            { new GameState { RoomNumber = 226, Storyline = 2135}, new Transition { Storyline = 2150, Description = "Trials to Bahamut naming"} }, // TODO: Remove this and skip straight to Via Purifico. Add Bahamut manually
            { new GameState { RoomNumber = 226, Storyline = 2150}, new BahamutTransition {ForceLoad = false, Description = "Naming Bahamut", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 226, Storyline = 2155, Menu = 0}, new Transition
            {
                RoomNumber = 198, Storyline = 2220, SpawnPoint = 0, Description = "Bahamut to Via Purifico",
                Formation = new byte[]{ 0x1, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                EnableTidus = 0, EnableYuna = 17, EnableAuron = 0, EnableKimahri = 0, EnableWakka = 0, EnableLulu = 0, EnableRikku = 0,
                ViaPurificoPlatform = 1
            } },
            { new GameState { RoomNumber = 198, Storyline = 2220, EnableAuron = 17}, new IsaaruTransition {ForceLoad = false, Description = "Isaaru", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 209, Storyline = 2220}, new AltanaTransition {ForceLoad = false, Description = "Evrae Altana", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 208, Storyline = 2220}, new Transition
            {
                Storyline = 2275, SpawnPoint = 2, ForceLoad = true, Description = "Enter Highbridge",
                Formation = new byte[]{ 0x0, 0x1, 0x4, 0x6, 0x2, 0x5, 0xFF },
                EnableLulu = 17, EnableYuna = 17, EnableAuron = 17
            } },
            { new GameState { RoomNumber = 208, Storyline = 2280}, new NatusTransition { ForceLoad = false, Description = "Seymour Natus", Suspendable = false, Repeatable = true } },
            { new GameState { RoomNumber = 183, Storyline = 2290}, new Transition { RoomNumber = 183, Storyline = 2300, SpawnPoint = 0, ForceLoad = false, Description = "Natus Death"} },
		    // END OF BEVELLE
		    // START OF CALM LANDS & GAGAZET
		    { new GameState { RoomNumber = 206, Storyline = 2300, CutsceneAlt = 3712}, new Transition { RoomNumber = 177, Storyline = 2385, SpawnPoint = 1, MacalaniaFlag = 162, Description = "Lake Scene"} },
            { new GameState { RoomNumber = 223, Storyline = 2385}, new Transition { Storyline = 2400, CalmLandsFlag = 548, ForceLoad = false, Description = "Calm Lands Intro + Gorge flag"} },
            { new GameState { RoomNumber = 279, Storyline = 2400}, new DefenderXTransition {ForceLoad = false, Description = "Defender X", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 279, Storyline = 2420, MovementLock = 48, XCoordinate = 265.3771973f }, new Transition { RoomNumber = 259, Storyline = 2510, RoomNumberAlt = 266, SpawnPoint = 0, Description = "Yuna reflects"} },
            { new GameState { RoomNumber = 259, Storyline = 2510}, new RonsoTransition {ForceLoad = false, Description = "Biran + Yenke", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 259, Storyline = 2510, CutsceneAlt = 70}, new Transition {Storyline = 2530, SpawnPoint = 1, Description = "Ronso Singing"} },
            { new GameState { RoomNumber = 285, Storyline = 2530}, new FluxTransition {ForceLoad = false, Description = "Seymour Flux", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 309, Storyline = 2560}, new Transition { RoomNumber = 134, Storyline = 2585, Description = "Fayth FMV + Tidus collapses"} },
            { new GameState { RoomNumber = 134, Storyline = 2585}, new Transition { Storyline = 2590, SpawnPoint = 0, Description = "Tidus wakes up in Zanarkand"} },
            { new GameState { RoomNumber = 165, Storyline = 2595, State = 1 }, new Transition { RoomNumber = 249, Storyline = 2610, Description = "Tidus enters his house"} },
            //{ new GameState { RoomNumber = 134, Storyline = 2600, State = 1 }, new Transition { RoomNumber = 249, Storyline = 2610, Description = "The fayth asks Tidus to end the dream"} }, // Bug: Game softlocks with Tidus entering house skip, so this little bit of movement is lost for now
            { new GameState { RoomNumber = 249, Storyline = 2610, }, new Transition { RoomNumber = 309, Storyline = 2610, Description = "The dream disintegrates"} },
            { new GameState { RoomNumber = 309, Storyline = 2610, }, new Transition { Storyline = 2585, Description = "Tidus wakes up"} },
            { new GameState { RoomNumber = 272, Storyline = 2585}, new Transition { GagazetCaveFlag = 29120, Description = "Gagazet Cave scenes"} },
            { new GameState { RoomNumber = 311, Storyline = 2585}, new SanctuaryTransition {ForceLoad = false, Description = "Sanctuary Keeper", Suspendable = false, Repeatable = true} },
		    // END OF GAGAZET
		    // START OF ZANARKAND
		    { new GameState { RoomNumber = 132, Storyline = 2680, State = 0}, new Transition { RoomNumber = 363, Storyline = 2767, SpawnPoint = 0, Description = "Zanarkand Campfire"} },
            { new GameState { RoomNumber = 320, Storyline = 2767}, new Transition { SpawnPoint = 0, Description = "Zanarkand Trials Begin"} },
            //{ new GameState { RoomNumber = 317, Storyline = 2775}, new SpectralKeeperTransition {ForceLoad = false, Description = "Pre Spectral Keeper", Suspendable = false, Repeatable = true} }, //This skip is very unstable and is nigh impossible to implement without crashes. Commented out for now.
            { new GameState { RoomNumber = 320, Storyline = 2780, BattleState = 522}, new SpectralKeeperTransition2 {ForceLoad = false, Description = "Post Spectral Keeper", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 318, Storyline = 2790}, new Transition { Storyline = 2815, Description = "Spectral Keeper to Yunalesca"} },
            { new GameState { RoomNumber = 270, Storyline = 2815}, new YunalescaTransition {ForceLoad = false, Description = "Pre-Yunalesca", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 315, Storyline = 2850}, new Transition { RoomNumber = 194, Storyline = 2900, SpawnPoint = 2, Description = "End of Zanarkand"} },
		    // END OF ZANARKAND
		    // START OF SIN
		    { new GameState { RoomNumber = 211, Storyline = 2900, XCoordinate = -9.918679f}, new Transition { Storyline = 2915, SpawnPoint = 7, AirshipDestinations = 2048, Description = "Yuna/Kimahri talk about defeating Sin"} },
            { new GameState { RoomNumber = 208, Storyline = 2920, CutsceneAlt = 91}, new Transition { RoomNumber = 255, Storyline = 2970, SpawnPoint = 0, AirshipDestinations = 2560, Description = "Return from Highbridge"} },
            { new GameState { RoomNumber = 255, Storyline = 2990}, new Transition { RoomNumber = 211, Storyline = 3010, SpawnPoint = 1, AirshipDestinations = 2048, Description = "Sin destination cutscene"} }, //Bug (Minor): Wrong area/spawn
		    { new GameState { RoomNumber = 277, Storyline = 3010}, new Transition { RoomNumber = 199, Storyline = 3085, Description = "Left Fin" } },
            { new GameState { RoomNumber = 199, Storyline = 3085}, new FinLeftTransition {ForceLoad=false, Description = "Left fin Pre-Boss", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 255, Storyline = 3085}, new FinLeftAirshipTransition {ForceLoad=false, Description = "Left Fin Dead", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 200, Storyline = 3085}, new FinRightTransition {ForceLoad=false, Description = "Right Fin Pre-Boss", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 255, Storyline = 3095}, new FinRightAirshipTransition {ForceLoad=false, Description = "Right Fin Dead", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 200, Storyline = 3100}, new Transition { RoomNumber = 201, Storyline = 3105, Description = "Star Players First!" } },
            { new GameState { RoomNumber = 201, Storyline = 3120}, new Transition { RoomNumber = 374, Storyline = 3125, SpawnPoint = 1, ForceLoad = false, Description = "Core Death"} },
            { new GameState { RoomNumber = 202, Storyline = 3125, XCoordinate = 18.58586121f, State = 0}, new Transition { RoomNumber = 374, Storyline = 3135, SpawnPoint = 1, Description = "Yuna monologue"} },
            { new GameState { RoomNumber = 296, Storyline = 3205}, new OmnisTransition {ForceLoad = false, Description = "Pre-BFA", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 327, Storyline = 3250, CutsceneAlt = 5889}, new Transition { RoomNumber = 324, Storyline = 3250, SpawnPoint = 0, Description = "Enter Tower of the Dead"} },
            { new GameState { RoomNumber = 325, Storyline = 3270}, new BFATransition {ForceLoad = false, Description = "Pre-BFA", Suspendable = false, Repeatable = true} },
            { new GameState { RoomNumber = 325, Storyline = 3300, CutsceneAlt = 1180}, new Transition { RoomNumber = 326, Storyline = 3360, Description = "BFA Death. GG!"} },
            { new GameState { RoomNumber = 326, Storyline = 3360, HpEnemyA = 0}, new AeonTransition {ForceLoad=false, Description = "Contest of Aeons!", Suspendable = false, Repeatable = true} }

        };

        public static readonly Dictionary<IGameState, Transition> PostBossBattleTransitions = new Dictionary<IGameState, Transition>()
        {
            { new GameState { RoomNumber = 83, Storyline = 172, State = 1, CutsceneAlt = 0 }, new Transition { RoomNumber = 68, Storyline = 184, Description = "Tidus joins the Aurochs"} },
            { new GameState { HpEnemyA = 2000, Storyline = 280}, new Transition { RoomNumber = 220, Storyline = 287, TargetFramerate = 2, MenuCleanup = true, AddItems = true, Description = "Echuilles"} },
            { new GameState { HpEnemyA = 3000, Storyline = 322 }, new Transition { SpawnPoint = 1, TargetFramerate = 2, MenuCleanup = true, AddItems = true, Description = "Post-Geneaux"} },
            { new GameState { RoomNumber = 45, Storyline = 346, State = 1 }, new Transition { RoomNumber = 78, Storyline = 348, SpawnPoint = 0, EnableIfrit = 17, Description = "Ifrit"} },
            { new GameState { HpEnemyA = 6000, Storyline = 502 }, new Transition { RoomNumber = 121, Storyline = 508, Description = "Oblitzerator"} },
            { new GameState { HpEnemyA = 6000, Storyline = 865 }, new Transition { RoomNumber = 254, Storyline = 882, EnableTidus = 17, EnableKimahri = 17, EnableLulu = 17, EnableWakka = 17, Description = "Sinspawn Gui 2"} },
            { new GameState { HpEnemyA = 12000, Storyline = 1420 }, new Transition { RoomNumber = 221, Storyline = 1470, SpawnPoint = 2, Description = "Spherimorph", AuronOverdrives = 11569} },
            { new GameState { HpEnemyA = 16000, Storyline = 1485 }, new Transition { RoomNumber = 192, Storyline = 1504, SpawnPoint = 1, TargetFramerate = 2, MenuCleanup = true, AddItems = true, Description = "Crawler"} },
            { new GameState { HpEnemyA = 1200, RoomNumber = 102, Storyline = 1570 }, new Transition { RoomNumber = 54, Storyline = 1600, SpawnPoint = 0, Description = "Wendigo"} }, // HP Value is the Guard
            { new GameState { HpEnemyA = 12000, Storyline = 1704 }, new Transition { RoomNumber = 129, Storyline = 1715, SpawnPoint = 0, Description = "Bikanel Zu",
                Formation = new byte[]{ 0x0, 0x2, 0x5, 0x4, 0xFF, 0xFF, 0xFF }, EnableWakka = 17, PositionTidusAfterLoad = true, TargetActorID = 1, Target_x = -20.05244827f, Target_y = -2.300839186f, Target_z = -247.0119934f } },
            { new GameState { HpEnemyA = 9000, Storyline = 1885 }, new Transition { RoomNumber = 280, Storyline = 1940, SpawnPoint = 4, Description = "Home Chimera"} },
            { new GameState { HpEnemyA = 36000, Storyline = 2280 }, new Transition { RoomNumber = 183, Storyline = 2290, SpawnPoint = 4, Description = "Seymour Natus"} },
            { new GameState { HpEnemyA = 70000, Storyline = 2530 }, new Transition { RoomNumber = 285, Storyline = 2560, SpawnPoint = 2, TargetFramerate = 2, MenuCleanup = true, AddItems = true, Description = "Seymour Flux"} },
            { new GameState { HpEnemyA = 40000, Storyline = 2585 }, new Transition { RoomNumber = 311, Storyline = 2680, SpawnPoint = 0, Description = "Sanctuary Keeper"} },
            { new GameState { HpEnemyA = 24000, Storyline = 2815 }, new Transition { RoomNumber = 270, Storyline = 2850, SpawnPoint = 0, Description = "Yunalesca"} },
            { new GameState { HpEnemyA = 80000, Storyline = 3205 }, new  Transition { Storyline = 3250, ForceLoad = false, Description = "Seymour Omnis"} }

        };
    }
}
