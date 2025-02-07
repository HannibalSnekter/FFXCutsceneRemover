﻿using FFXCutsceneRemover.Logging;
using FFX_Cutscene_Remover.ComponentUtil;
using System.Diagnostics;
using System.Collections.Generic;

namespace FFXCutsceneRemover
{
    class GuiTransition : Transition
    {
        static private byte[] GuiFormation = { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0xFF, 0xFF, 0xFF, 0xFF };

        static private List<short> CutsceneAltList2 = new List<short>(new short[] { 4449, 4450 });

        public override void Execute(string defaultDescription = "")
        {
            Process process = memoryWatchers.Process;

            if (base.memoryWatchers.MovementLock.Current == 0x20 && Stage == 0)
            {
                base.Execute();

                BaseCutsceneValue = base.memoryWatchers.EventFileStart.Current;

                Stage += 1;

            }
            else if (base.memoryWatchers.GuiTransition.Current == (BaseCutsceneValue + 0xE711) && Stage == 1)
            {
                /*/
                process.Suspend();

                WriteValue<int>(base.memoryWatchers.GuiTransition, BaseCutsceneValue + 0xE978);

                process.Resume();
                //*/

                Stage += 1;
            }
            else if (base.memoryWatchers.GuiTransition.Current == (BaseCutsceneValue + 0xEC58) && Stage == 2)
            {
                /*/
                process.Suspend();

                new Transition { ForceLoad = false, Storyline = 857, ConsoleOutput = false }.Execute();

                WriteValue<int>(base.memoryWatchers.GuiTransition, BaseCutsceneValue + 0xF00A);

                // Reposition Party Members just off screen to run into battle
                Transition actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, PartyTarget_x = 427.0f, PartyTarget_z = 3350.0f, PositionPartyOffScreen = true };
                actorPositions.Execute();

                //Position Gui
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { 4213 }, Target_x = 399.9999695f, Target_z = 3100.0f };
                actorPositions.Execute();

                process.Resume();
                //*/

                Stage += 1;
            }
            else if (base.memoryWatchers.Storyline.Current == 860 && Stage == 3)
            {
                process.Suspend();

                GuiFormation = process.ReadBytes(memoryWatchers.Formation.Address, 10);

                new Transition { RoomNumber = 247, Storyline = 865, Description = "Auron Look out + FMV " }.Execute();

                Stage += 1;

                process.Resume();
            }
            else if (CutsceneAltList2.Contains(base.memoryWatchers.CutsceneAlt.Current) && Stage == 4)
            {
                BaseCutsceneValue2 = base.memoryWatchers.EventFileStart.Current;

                Stage += 1;
            }
            else if (base.memoryWatchers.Gui2Transition.Current == (BaseCutsceneValue2 + 0x270B) && Stage == 5)
            {
                /*/
                process.Suspend();

                WriteValue<int>(base.memoryWatchers.Gui2Transition, BaseCutsceneValue2 + 0x29C0);

                Transition actorPositions;
                //Position Yuna
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { 2 }, Target_x = 225.0f, Target_z = 3140.0f };
                actorPositions.Execute();

                //Position Auron
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { 3 }, Target_x = 155.0f, Target_z = 3140.0f };
                actorPositions.Execute();

                //Position Seymour
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { 8 }, Target_x = 190.0f, Target_z = 3150.0f };
                actorPositions.Execute();

                //Position Gui
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { 4213 }, Target_x = 190.0f, Target_z = 3294.0f };
                actorPositions.Execute();

                process.Resume();
                //*/

                Stage += 1;
            }
            else if (base.memoryWatchers.Gui2Transition.Current == (BaseCutsceneValue2 + 0x2B3D) && Stage == 6)
            {
                process.Suspend();

                Transition FormationSwitch = new Transition { ForceLoad = false, ConsoleOutput = true, FormationSwitch = Transition.formations.PostGui, Formation = GuiFormation, Description = "Fix party after Gui" };
                FormationSwitch.Execute();

                Stage += 1;

                process.Resume();
            }
        }
    }
}
