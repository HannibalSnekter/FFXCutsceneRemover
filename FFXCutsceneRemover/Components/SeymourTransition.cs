﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using FFXCutsceneRemover.Logging;
using FFX_Cutscene_Remover.ComponentUtil;

namespace FFXCutsceneRemover
{
    class SeymourTransition : Transition
    {
        static private byte[] formation = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0xFF };
        public override void Execute(string defaultDescription = "")
        {
            Process process = memoryWatchers.Process;

            int baseAddress = base.memoryWatchers.GetBaseAddress();

            if (base.memoryWatchers.MovementLock.Current == 0 && Stage == 0)
            {
                process.Suspend();

                base.Execute();

                MemoryWatcher<byte> shivaEnabled1 = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3211C));
                WriteValue<byte>(shivaEnabled1, 0x11);

                MemoryWatcher<byte> shivaEnabled2 = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD326E4));
                WriteValue<byte>(shivaEnabled2, 0x11);

                BaseCutsceneValue = base.memoryWatchers.EventFileStart.Current;

                Stage += 1;

                process.Resume();
            }
            else if (base.memoryWatchers.MovementLock.Current == 0x10 && Stage == 1)
            {
                process.Suspend();

                WriteValue<int>(base.memoryWatchers.SeymourTransition, BaseCutsceneValue + 0x75DF);
                WriteValue<byte>(base.memoryWatchers.CutsceneTiming, 0);

                formation = process.ReadBytes(base.memoryWatchers.Formation.Address, 7);

                Transition actorPositions;
                //Position Party Member 1
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { (short)(formation[0] + 1) }, Target_x = 35.668f, Target_y = 0.0f, Target_z = -42.0f };
                actorPositions.Execute();

                //Position Party Member 2
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { (short)(formation[1] + 1) }, Target_x = 3.668f, Target_y = 0.0f, Target_z = -55.0f };
                actorPositions.Execute();

                //Position Party Member 3
                actorPositions = new Transition { ForceLoad = false, ConsoleOutput = false, TargetActorIDs = new short[] { (short)(formation[2] + 1) }, Target_x = -28.332f, Target_y = 0.0f, Target_z = -42.0f };
                actorPositions.Execute();

                Stage += 1;

                process.Resume();
            }
            else if (base.memoryWatchers.SeymourTransition2.Current == (BaseCutsceneValue + 0x77D6) && Stage == 2)
            {
                process.Suspend();
                WriteValue<int>(base.memoryWatchers.SeymourTransition2, BaseCutsceneValue + 0x7F85);

                Stage += 1;

                process.Resume();
            }
            else if (base.memoryWatchers.Menu.Current == 1 && base.memoryWatchers.CutsceneAlt.Current == 0 && Stage == 3)
            {
                process.Suspend();

                new Transition { Storyline = 1545, ConsoleOutput = false, PositionTidusAfterLoad = true, Target_x = 1.470157981f, Target_y = 0.0f, Target_z = 0.3889299929f, Target_rot = -0.1195230484f, Target_var1 = 53 }.Execute();

                Stage += 1;

                process.Resume();
            }
        }
    }
}