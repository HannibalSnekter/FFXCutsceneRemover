using FFX_Cutscene_Remover.ComponentUtil;
using FFXCutsceneRemover.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using FFXCutsceneRemover.Logging;

/*
 * Main loops for the Cutscene Remover program.
 */
namespace FFXCutsceneRemover
{
    class Randomiser
    {
        public Process Game;

        private Transition PreviouslyExecutedTransition;

        public void MainLoop(MemoryWatchers MemoryWatchers)
        {
            Dictionary<IGameState, Transition> randomiserTransitions = RandomiserTransitions.StandardTransitions;
            foreach (var transition in randomiserTransitions)
            {
                if (transition.Key.CheckState() && MemoryWatchers.ForceLoad.Current == 0)
                {
                    ExecuteTransition(transition.Value, "Executing Randomiser Transition - No Description");
                }
            }
        }

        // Save the previous transition so that we don't execute the same transition multiple times in a row.
        private void ExecuteTransition(Transition transition, string defaultDescription = "")
        {
            bool suspended = false;

            if (transition != PreviouslyExecutedTransition || transition.Repeatable)
            {
                if (transition.Suspendable)
                {
                    Game.Suspend();
                    suspended = true;
                }

                transition.Execute(defaultDescription);

                PreviouslyExecutedTransition = transition;

                if (suspended)
                {
                    Game.Resume();
                }
            }
        }
    }
}
