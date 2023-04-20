﻿using FFX_Cutscene_Remover.ComponentUtil;
using System;
using System.Diagnostics;
using FFXCutsceneRemover.Resources;

namespace FFXCutsceneRemover
{
    class AddWakkaAbilitiesTransition : Transition
    {
        bool WakkaDarkAttack = false;
        public override void Execute(string defaultDescription = "")
        {
            base.Execute();

            Process process = memoryWatchers.Process;
            int baseAddress = base.memoryWatchers.GetBaseAddress();

            MemoryWatcher<byte> abilities1 = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3205C + 0x94 * 0x04 + 0x3E));
            byte[] abilitiesBytes1 = process.ReadBytes(abilities1.Address, 12);

            MemoryWatcher<byte> abilities2 = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD35E08 + 0x1C * 0x04));
            byte[] abilitiesBytes2 = process.ReadBytes(abilities2.Address, 12);

            int memorySizeBytes = 1720;
            byte[] SphereGridBytes = process.ReadBytes(memoryWatchers.SphereGrid.Address, memorySizeBytes);

            for (int i = 0; i < memorySizeBytes / 2; i++)
            {
                if (SphereGridBytes[2 * i] == 0x2E & (SphereGridBytes[2 * i + 1] & 0x10) == 0x10)
                {
                    WakkaDarkAttack = true;
                }
            }

            if (WakkaDarkAttack == false)
            {
                byte nodeID = 0x2E;

                int byteNum = RandomiserTransitions.RandoSetupTransition.abilityMemoryLocations[nodeID][0];
                int bitNum = RandomiserTransitions.RandoSetupTransition.abilityMemoryLocations[nodeID][1];

                byte bitValue = (byte)Math.Pow(2, bitNum);

                if ((abilitiesBytes1[byteNum] & bitValue) == 0x00) { abilitiesBytes1[byteNum] += bitValue; }
                if ((abilitiesBytes2[byteNum] & bitValue) == 0x00) { abilitiesBytes2[byteNum] += bitValue; }
            }

            WriteBytes(abilities1, abilitiesBytes1);
            WriteBytes(abilities2, abilitiesBytes2);
        }
    }
}