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
using Harmony;
using UnityEngine;

namespace ProperShotguns
{
	[StaticConstructorOnStartup]
	internal static class HarmonyPatches
	{

        // I misplaced the actual source code so I had to export the DLL to a new VS project ;-;

		static HarmonyPatches()
		{
			HarmonyInstance harmonyInstance = HarmonyInstance.Create("XeoNovaDan.ProperShotguns");

			harmonyInstance.Patch(AccessTools.Property(typeof(Projectile), "DamageAmount").GetGetMethod(),
                postfix: new HarmonyMethod(patchType, "PostfixDamageAmount"));

            //harmonyInstance.Patch(AccessTools.Method(typeof(ThingDef), nameof(ThingDef.SpecialDisplayStats)),
            //    transpiler: new HarmonyMethod(patchType, nameof(TranspileSpecialDisplayStats)));
        }

		public static void PostfixDamageAmount(Projectile __instance, ref int __result)
		{
			ShotgunExtension shotgunExtension = __instance.def.GetModExtension<ShotgunExtension>() ?? ShotgunExtension.defaultValues;
			float num = (float)__result / shotgunExtension.pelletCount;
			if (ProperShotgunsSettings.damageRoundMode == "    Standard")
				__result = Mathf.RoundToInt(num);
			else
				__result = GenMath.RoundRandom(num);
		}

        //public static IEnumerable<CodeInstruction> TranspileSpecialDisplayStats(IEnumerable<CodeInstruction> instructions)
        //{
        //    List<CodeInstruction> instructionList = instructions.ToList();

        //    bool foundCodeBlock = false;
        //    bool done = false;

        //    for (int i = 0; i < instructionList.Count; i++)
        //    {
        //        CodeInstruction instruction = instructionList[i];

        //        Log.Message(instruction.ToString());

        //        if (!done && instruction.opcode == OpCodes.Ldfld && instruction.operand == AccessTools.Field(typeof(VerbProperties), "defaultProjectile"))
        //            foundCodeBlock = true;

        //        if (!done && foundCodeBlock && instruction.opcode == OpCodes.Call && instruction.operand == AccessTools.Method(typeof(System.Object), nameof(System.Object.ToString)))
        //        {
        //            yield return new CodeInstruction(instruction.opcode, instruction.operand);
        //            yield return new CodeInstruction(OpCodes.Ldarg_1);
        //            yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Property(typeof(StatRequest), nameof(StatRequest.Thing)).GetGetMethod());

        //            instruction.opcode = OpCodes.Call;
        //            instruction.operand = AccessTools.Method(patchType, nameof(NewDamageFeedbackString));
        //            done = true;
        //        }

        //        yield return instruction;
        //    }
        //}

        //private static string NewDamageFeedbackString(string damageAmount, Thing weapon)
        //{
        //    VerbProperties verb = weapon.def.Verbs.First(v => v.isPrimary);
        //    if (verb.verbClass == typeof(Verb_ShootShotgun) && verb.defaultProjectile.GetModExtension<ShotgunExtension>() is ShotgunExtension extension)
        //    {
        //        float dmg = float.Parse(damageAmount, CultureInfo.InvariantCulture.NumberFormat);
        //        float pelletCount = extension.pelletCount;
        //        float basePelletDmg = dmg / pelletCount;

        //        string retStr = pelletCount + " x ";
        //        if (ProperShotgunsSettings.damageRoundMode == "    Standard")
        //            return retStr + Mathf.RoundToInt(basePelletDmg);
        //        return retStr + Math.Floor(basePelletDmg) + "-" + Math.Ceiling(basePelletDmg);
        //    }
        //    return damageAmount;
        //}

        private static readonly Type patchType = typeof(HarmonyPatches);
	}
}
