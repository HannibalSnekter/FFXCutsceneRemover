﻿using FFX_Cutscene_Remover.ComponentUtil;
using FFXCutsceneRemover.Logging;
using FFXCutsceneRemover.Resources;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text.Json;
using System.IO;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace FFXCutsceneRemover
{
    class RandoSetupTransition : Transition
    {
        RandoOptions options = JsonSerializer.Deserialize<RandoOptions>(File.ReadAllText(@".\RandomiserOptions.json"));

        private byte[] SphereGridBytes;
        private List<byte> validBytes = new List<byte>();
        private List<byte> abilityBytes = new List<byte>();
        private List<byte> shuffleBytes = new List<byte>();

        // Stat data - HP / MP / STR / DEF / MAG / MDEF / AGI / LCK / EVA / ACC(Offsets only)
        private double[] statMean = new double[] { 585.0f, 56.0f, 12.0f, 10.0f, 12.0f, 11.0f, 8.5f, 18.0f, 14.0f };
        private double[] statStdDev = new double[] { 232.0f, 18.0f, 6.0f, 4.0f, 5.5f, 10.0f, 4.0f, 1.0f, 14.5f };
        private double[] statMin = new double[] { 360.0f, 10.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 15.0f, 5.0f };
        private double[] statTotal = new double[] { 4091.0f, 394.0f, 85.0f, 71.0f, 87.0f, 78.0f, 59.0f, 124.0f, 100.0f };
        private int[] baseStatOffsets = new int[] { 0x04, 0x08, 0x0C, 0x0D, 0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13 };
        private int[] currentStatOffsets = new int[] { 0x24, 0x28, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
        private int[] accuracyValues = new int[] { 5, 10, 25, 5, 3, 3, 3 };

        // Sphere Grid Lock data
        private int[] LockCount = new int[] { 20, 15, 10, 10 };
        private byte[] LockIDs = new byte[] { 0x27, 0x28, 0x00, 0x29 };

        private int index = 0;

        readonly short[][] defaultAbilityNodeLocations = new short[][]
        {
            new short[] { 000, 002 },
            new short[] { 335, 336 },
            new short[] { 540, 541 },
            new short[] { 636, 637 },
            new short[] { 056, 057 },
            new short[] { 220, 221 },
            new short[] { 449, 450 },
        };

        readonly short[][] randomStartNodeLocations = new short[][]
        {
            new short[] { 000, 002 },
            new short[] { 010, 539 },
            new short[] { 011, 828 },
            new short[] { 026, 027 },
            new short[] { 034, 035 },
            new short[] { 038, 041 },
            new short[] { 049, 050 },
            new short[] { 056, 057 },
            new short[] { 071, 072 },
            new short[] { 081, 082 },
            new short[] { 089, 806 },
            new short[] { 096, 156 },
            new short[] { 111, 116 },
            new short[] { 124, 123 },
            new short[] { 135, 702 },
            new short[] { 141, 142 },
            new short[] { 146, 822 },
            new short[] { 166, 744 },
            new short[] { 174, 173 },
            new short[] { 181, 182 },
            new short[] { 191, 192 },
            new short[] { 199, 200 },
            new short[] { 212, 538 },
            new short[] { 214, 129 },
            new short[] { 224, 221 },
            new short[] { 243, 245 },
            new short[] { 246, 748 },
            new short[] { 268, 529 },
            new short[] { 289, 747 },
            new short[] { 313, 314 },
            new short[] { 325, 304 },
            new short[] { 327, 328 },
            new short[] { 340, 343 },
            new short[] { 351, 352 },
            new short[] { 359, 361 },
            new short[] { 368, 369 },
            new short[] { 383, 384 },
            new short[] { 394, 699 },
            new short[] { 405, 406 },
            new short[] { 415, 416 },
            new short[] { 422, 421 },
            new short[] { 429, 485 },
            new short[] { 441, 440 },
            new short[] { 448, 449 },
            new short[] { 454, 455 },
            new short[] { 470, 469 },
            new short[] { 489, 497 },
            new short[] { 490, 491 },
            new short[] { 504, 503 },
            new short[] { 516, 517 },
            new short[] { 553, 554 },
            new short[] { 571, 572 },
            new short[] { 582, 583 },
            new short[] { 590, 591 },
            new short[] { 601, 016 },
            new short[] { 603, 836 },
            new short[] { 618, 740 },
            new short[] { 634, 633 },
            new short[] { 636, 637 },
            new short[] { 644, 641 },
            new short[] { 668, 667 },
            new short[] { 687, 686 },
            new short[] { 693, 684 },
            new short[] { 697, 769 },
            new short[] { 706, 705 },
            new short[] { 725, 726 },
            new short[] { 776, 773 },
            new short[] { 805, 092 },
            new short[] { 855, 851 },
        };

        readonly short[] defaultCharacterNodeLocations = new short[] { 0, 341, 540, 637, 56, 224, 448 };

        public Dictionary<byte, byte[]> abilityMemoryLocations = new Dictionary<byte, byte[]>()
        {
            { 0x2A, new byte[] { 0, 6 } }, // Delay Attack
            { 0x2B, new byte[] { 0, 7 } }, // Delay Buster
            { 0x2C, new byte[] { 1, 0 } },
            { 0x2D, new byte[] { 1, 1 } },
            { 0x2E, new byte[] { 1, 2 } },
            { 0x2F, new byte[] { 1, 3 } },
            { 0x30, new byte[] { 1, 4 } },
            { 0x31, new byte[] { 1, 5 } },
            { 0x32, new byte[] { 1, 6 } },
            { 0x33, new byte[] { 1, 7 } },
            { 0x34, new byte[] { 2, 0 } },
            { 0x35, new byte[] { 2, 1 } },
            { 0x36, new byte[] { 2, 2 } },
            { 0x37, new byte[] { 2, 3 } },
            { 0x38, new byte[] { 2, 4 } },
            { 0x39, new byte[] { 2, 5 } },
            { 0x3A, new byte[] { 2, 6 } },
            { 0x3B, new byte[] { 2, 7 } },
            { 0x3C, new byte[] { 3, 0 } },
            { 0x3D, new byte[] { 3, 1 } },
            { 0x3E, new byte[] { 3, 2 } },
            { 0x3F, new byte[] { 3, 4 } },
            { 0x40, new byte[] { 3, 5 } },
            { 0x41, new byte[] { 3, 3 } },
            { 0x42, new byte[] { 3, 6 } },
            { 0x43, new byte[] { 3, 7 } },
            { 0x44, new byte[] { 4, 0 } },
            { 0x45, new byte[] { 4, 2 } },
            { 0x46, new byte[] { 4, 3 } },
            { 0x47, new byte[] { 4, 4 } },
            { 0x48, new byte[] { 4, 5 } },
            { 0x49, new byte[] { 4, 6 } },
            { 0x4A, new byte[] { 4, 7 } },
            { 0x4B, new byte[] { 5, 0 } },
            { 0x4C, new byte[] { 5, 1 } },
            { 0x4D, new byte[] { 5, 2 } },
            { 0x4E, new byte[] { 5, 3 } },
            { 0x4F, new byte[] { 5, 4 } },
            { 0x50, new byte[] { 5, 5 } },
            { 0x51, new byte[] { 5, 6 } },
            { 0x52, new byte[] { 5, 7 } },
            { 0x53, new byte[] { 5, 0 } },
            { 0x54, new byte[] { 6, 1 } },
            { 0x55, new byte[] { 6, 2 } },
            { 0x56, new byte[] { 6, 3 } },
            { 0x57, new byte[] { 6, 4 } },
            { 0x58, new byte[] { 6, 5 } },
            { 0x59, new byte[] { 6, 6 } },
            { 0x5A, new byte[] { 6, 7 } },
            { 0x5B, new byte[] { 7, 0 } },
            { 0x5C, new byte[] { 7, 1 } },
            { 0x5D, new byte[] { 7, 2 } },
            { 0x5E, new byte[] { 7, 3 } },
            { 0x5F, new byte[] { 7, 4 } },
            { 0x60, new byte[] { 7, 5 } },
            { 0x61, new byte[] { 7, 6 } },
            { 0x62, new byte[] { 7, 7 } },
            { 0x63, new byte[] { 8, 0 } },
            { 0x64, new byte[] { 8, 1 } },
            { 0x65, new byte[] { 8, 2 } },
            { 0x66, new byte[] { 8, 3 } },
            { 0x67, new byte[] { 8, 4 } },
            { 0x68, new byte[] { 8, 5 } },
            { 0x69, new byte[] { 8, 6 } },
            { 0x6A, new byte[] { 8, 7 } },
            { 0x6B, new byte[] { 9, 0 } },
            { 0x6C, new byte[] { 9, 1 } },
            { 0x6D, new byte[] { 9, 2 } },
            { 0x6E, new byte[] { 9, 3 } },
            { 0x6F, new byte[] { 9, 4 } },
            { 0x70, new byte[] { 9, 5 } },
            { 0x71, new byte[] { 9, 6 } },
            { 0x72, new byte[] { 9, 7 } },
            { 0x73, new byte[] { 10, 0 } },
            { 0x74, new byte[] { 10, 1 } },
            { 0x75, new byte[] { 10, 2 } },
            { 0x76, new byte[] { 10, 3 } },
            { 0x77, new byte[] { 11, 0 } },
            { 0x78, new byte[] { 11, 1 } },
            { 0x79, new byte[] { 11, 2 } },
            { 0x7A, new byte[] { 11, 3 } },
            { 0x7B, new byte[] { 11, 4 } },
            { 0x7C, new byte[] { 11, 5 } },
            { 0x7D, new byte[] { 11, 6 } },
            { 0x7E, new byte[] { 11, 7 } },
        };

        public override void Execute(string defaultDescription = "")
        {
            base.Execute();

            RandomiseSphereGrid();

            if (options.RandomiseStats == 1)
            {
                RandomiseBaseStats();
            }            
        }

        private void RandomiseSphereGrid()
        {
            Process process = memoryWatchers.Process;
            int baseAddress = base.memoryWatchers.GetBaseAddress();

            int memorySizeBytes = 1714;
            SphereGridBytes = process.ReadBytes(memoryWatchers.SphereGrid.Address, memorySizeBytes);

            validBytes.Clear();
            abilityBytes.Clear();
            shuffleBytes.Clear();

            // Add node bytes to valid bytes list based on setup parameters
            for (byte i = 0x2A; i < 0x7F; i++)
            {
                validBytes.Add(i);
                abilityBytes.Add(i);
            }

            for (byte i = 0x02; i < 0x27; i++)
            {
                validBytes.Add(i);
            }

            if (options.SwapEmptyNodes == 1)
            {
                validBytes.Add(0x01);
            }

            if (options.SwapLockNodes == 1)
            {
                validBytes.Add(0x00); // Lock Lv.3
                validBytes.Add(0x27); // Lock Lv.1
                validBytes.Add(0x28); // Lock Lv.2
                validBytes.Add(0x29); // Lock Lv.4
            }

            short[][] startingAbilityNodeLocations = new short[7][];
            short[] startingCharacterNodeLocations = new short[7];
            byte[] startingLocationBytes = new byte[14];

            if (options.RandomiseStartLocations == 1)
            {
                LockCount[0] = 30;

                short[][] shuffleLocations = new short[randomStartNodeLocations.Length][];
                randomStartNodeLocations.CopyTo(shuffleLocations, 0);
                shuffleLocations.Shuffle();

                for (int i = 0; i < 7; i++)
                {
                    startingAbilityNodeLocations[i] = shuffleLocations[i];
                    startingCharacterNodeLocations[i] = shuffleLocations[i][0];
                    BitConverter.GetBytes(shuffleLocations[i][0]).CopyTo(startingLocationBytes, 2 * i);
                    DiagnosticLog.Information("Character " + i.ToString() + " start location: " + shuffleLocations[i][0].ToString());
                }

                WriteBytes(memoryWatchers.SphereGridStartLocations, startingLocationBytes);

            }
            else
            {
                defaultAbilityNodeLocations.CopyTo(startingAbilityNodeLocations,0);
                defaultCharacterNodeLocations.CopyTo(startingCharacterNodeLocations, 0);
            }

            // Iterate through the sphere grid picking up all nodes which are to be randomised adding them to a shuffle list and set activated nodes to 0
            for (int i = 0; i < memorySizeBytes / 2; i++)
            {
                byte nodeByte = SphereGridBytes[2 * i];
                if (validBytes.Contains(nodeByte))
                {
                    shuffleBytes.Add(nodeByte);
                }

                SphereGridBytes[2 * i + 1] = 0x00;
            }

            if (options.SwapLockNodes == 1)
            {
                // Redistribute Lock Nodes
                for (int i = 0; i < shuffleBytes.Count; i++)
                {
                    if (LockIDs.Contains(shuffleBytes[i]))
                    {
                        shuffleBytes[i] = 0x01;
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < LockCount[i]; j++)
                    {
                        for (int k = 0; k < shuffleBytes.Count; k++)
                        {
                            if (shuffleBytes[k] == 0x01)
                            {
                                shuffleBytes[k] = LockIDs[i];
                                break;
                            }
                        }
                    }
                }
            }

            // Shuffle the nodes
            shuffleBytes.Shuffle<byte>();

            index = 0;

            // Iterate through the sphere grid and replace each valid node with its new node from the shuffled list
            for (int i = 0; i < memorySizeBytes / 2; i++)
            {
                byte nodeByte = SphereGridBytes[2 * i];
                if (validBytes.Contains(nodeByte))
                {
                    SphereGridBytes[2 * i] = shuffleBytes[index];
                    index += 1;
                }
            }

            //Create a new shuffle list of abilities only and
            shuffleBytes.Clear();
            shuffleBytes = new List<byte>(abilityBytes);

            // Shuffle the nodes
            shuffleBytes.Shuffle<byte>();

            // Remove all abilities currently on a starting location
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    short startingLocation = startingAbilityNodeLocations[i][j];

                    byte nodeID = SphereGridBytes[2 * startingLocation];

                    if (nodeID >= 0x2A)
                    {
                        shuffleBytes.Remove(nodeID);
                    }
                }
            }

            index = 0;

            // Swap each non ability starting node for the next random ability in the shuffled list
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    short startingLocation = startingAbilityNodeLocations[i][j];

                    byte nodeID = SphereGridBytes[2 * startingLocation];

                    if (nodeID < 0x2A)
                    {
                        byte newNodeID = shuffleBytes[index];
                        int newNodeLocation = Array.IndexOf(SphereGridBytes, newNodeID) / 2;
                        SphereGridBytes[2 * startingLocation] = newNodeID;
                        SphereGridBytes[2 * newNodeLocation] = nodeID;
                        index += 1;
                    }
                }
            }

            // If any characters are starting on a lock node swap the lock node with the nearest empty node
            for (int i = 0; i < 7; i++)
            {
                short startingLocation = startingCharacterNodeLocations[i];

                byte nodeID = SphereGridBytes[2 * startingLocation];

                if (LockIDs.Contains(nodeID))
                {
                    short locationID = startingLocation;
                    bool foundEmpty = false;
                    while (!foundEmpty)
                    {
                        locationID += 2;

                        if (SphereGridBytes[2 * locationID] == 0x01)
                        {
                            foundEmpty = true;
                            SphereGridBytes[2 * startingLocation] = 0x01;
                            SphereGridBytes[2 * locationID] = nodeID;
                        }

                        if (locationID == memorySizeBytes - 2)
                        {
                            locationID = 0;
                        }
                    }
                }
            }

            if (options.TidusStartWithFlee == 1)
            {
                short locationID = (short)(startingCharacterNodeLocations[0] * 2);
                byte nodeID = SphereGridBytes[locationID];
                byte newNodeID = 0x3C; // Flee
                int newNodeLocation = Array.IndexOf(SphereGridBytes, newNodeID) / 2;
                SphereGridBytes[locationID] = newNodeID;
                SphereGridBytes[2 * newNodeLocation] = nodeID;
            }

            // Activate starting ability nodes for characters
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    short startingLocation = startingAbilityNodeLocations[i][j];

                    SphereGridBytes[2 * startingLocation + 1] = (byte)Math.Pow(2, i);
                }
            }

            // Remove Lulu's 2 extra activated nodes (Originally Blizzard and Water)
            SphereGridBytes[2 * 222 + 1] = 0x00;
            SphereGridBytes[2 * 223 + 1] = 0x00;

            WriteBytes(memoryWatchers.SphereGrid, SphereGridBytes);

            for (int i = 0; i < 7; i++)
            {
                MemoryWatcher<byte> abilities1 = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3205C + 0x94 * i + 0x3E));
                byte[] abilitiesBytes1 = new byte[] { 0x3B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                MemoryWatcher<byte> abilities2 = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD35E08 + 0x1C * i));
                byte[] abilitiesBytes2 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                for (int j = 0; j < 2; j++)
                {
                    short location = startingAbilityNodeLocations[i][j];
                    byte nodeID = SphereGridBytes[location * 2];
                    if (nodeID >= 0x2A)
                    {
                        int byteNum = abilityMemoryLocations[nodeID][0];
                        int bitNum = abilityMemoryLocations[nodeID][1];
                        abilitiesBytes1[byteNum] += (byte)Math.Pow(2, bitNum);
                        abilitiesBytes2[byteNum] += (byte)Math.Pow(2, bitNum);
                    }
                }

                WriteBytes(abilities1, abilitiesBytes1);
                WriteBytes(abilities2, abilitiesBytes2);

                abilities1 = null;
                abilities2 = null;
                abilitiesBytes1 = null;
                abilitiesBytes2 = null;
            }
        }

        private void RandomiseBaseStats()
        {
            Process process = memoryWatchers.Process;
            int baseAddress = base.memoryWatchers.GetBaseAddress();

            double probability = 0.0f;

            double[] statsStage1 = new double[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            double[] strengthStats = new double[] { 0, 0, 0, 0, 0, 0, 0 };

            // iterate through stats - HP / MP / STR / DEF / MAG / MDEF / AGI / LCK / EVA
            for (int i = 0; i < 9; i++)
            {
                statsStage1 = new double[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                //iterate through characters - Tidus / Yuna / Auron / Kimahri / Wakka / Lulu / Rikku
                for (int j = 0; j < 7; j++)
                {
                    probability = ThreadSafeRandom.ThisThreadsRandom.NextDouble();
                    double statValue = Normal.InvCDF(statMean[i], statStdDev[i], probability);
                    statValue = Math.Max(statValue, statMin[i]);
                    statsStage1[j] = statValue;
                }

                // rebalance stats in line with original total stats
                for (int j = 0; j < 7; j++)
                {
                    double statRescaled = statsStage1[j] / statsStage1.Sum() * statTotal[i];
                    statRescaled = Math.Max(statRescaled, statMin[i]);
                    int statInt = (int)Math.Round(statRescaled);

                    MemoryWatcher<byte> baseStat = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3205C + 0x94 * j + baseStatOffsets[i]));
                    MemoryWatcher<byte> currentStat = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3205C + 0x94 * j + currentStatOffsets[i]));
                    
                    if (i < 2)
                    {
                        byte[] baseStatBytes = BitConverter.GetBytes(statInt);
                        baseStatBytes.Reverse();
                        WriteBytes(baseStat, baseStatBytes);
                        WriteBytes(currentStat, baseStatBytes);
                    }
                    else
                    {

                        if (i == 2)
                        {
                            strengthStats[j] = statInt;
                        }
                        WriteValue<byte>(baseStat, (byte)statInt);
                        WriteValue<byte>(currentStat, (byte)statInt);
                    }
                }
            }

            // Ranks strength values in ascending order
            double[] strengthRanks = Statistics.Ranks(strengthStats, RankDefinition.First);

            for (int j = 0; j < 7; j++)
            {
                MemoryWatcher<byte> baseStat = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3205C + 0x94 * j + baseStatOffsets[9]));
                MemoryWatcher<byte> currentStat = new MemoryWatcher<byte>(new IntPtr(baseAddress + 0xD3205C + 0x94 * j + currentStatOffsets[9]));

                double Rank = strengthRanks[j];
                int statInt = accuracyValues[Math.Abs((int)Rank - 7)];
                WriteValue<byte>(baseStat, (byte)statInt);
                WriteValue<byte>(currentStat, (byte)statInt);
            }

            //Update Memory Watchers to get new values and then do a Full Heal
            memoryWatchers.TidusMaxHP.Update(process);
            memoryWatchers.YunaMaxHP.Update(process);
            memoryWatchers.AuronMaxHP.Update(process);
            memoryWatchers.KimahriMaxHP.Update(process);
            memoryWatchers.WakkaMaxHP.Update(process);
            memoryWatchers.LuluMaxHP.Update(process);
            memoryWatchers.RikkuMaxHP.Update(process);
            memoryWatchers.ValeforMaxHP.Update(process);

            memoryWatchers.TidusMaxMP.Update(process);
            memoryWatchers.YunaMaxMP.Update(process);
            memoryWatchers.AuronMaxMP.Update(process);
            memoryWatchers.KimahriMaxMP.Update(process);
            memoryWatchers.WakkaMaxMP.Update(process);
            memoryWatchers.LuluMaxMP.Update(process);
            memoryWatchers.RikkuMaxMP.Update(process);
            memoryWatchers.ValeforMaxMP.Update(process);

            new Transition { ForceLoad = false, FullHeal = true, ConsoleOutput = false }.Execute();
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}