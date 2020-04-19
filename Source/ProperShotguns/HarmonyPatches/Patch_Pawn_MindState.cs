using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using RimWorld;
using Verse;
using Verse.AI;
using HarmonyLib;
using UnityEngine;

namespace ProperShotguns
{

    public static class Patch_Pawn_MindState
    {

        [HarmonyPatch(typeof(Pawn_MindState), "CanStartFleeingBecauseOfPawnAction")]
        public static class CanStartFleeingBecauseOfPawnAction
        {

            public static void Postfix(Pawn p, ref bool __result)
            {
                // If the game's attempting to make them flee in the same tick that their flee job started, return false
                if (p.CurJobDef == JobDefOf.Flee && p.CurJob.startTick == Find.TickManager.TicksGame)
                    __result = false;
                    
            }

        }

	}

}
