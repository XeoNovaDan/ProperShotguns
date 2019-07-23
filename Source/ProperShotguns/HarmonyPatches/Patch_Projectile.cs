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

	static class Patch_Projectile
	{

        [HarmonyPatch(typeof(Projectile))]
        [HarmonyPatch(nameof(Projectile.DamageAmount), MethodType.Getter)]
        static class Patch_DamageAmount_Getter
        {

            public static void Postfix(Projectile __instance, ref int __result)
            {
                var shotgunExtension = __instance.def.GetModExtension<ShotgunExtension>() ?? ShotgunExtension.defaultValues;
                float adjustedDamage = (float)__result / shotgunExtension.pelletCount;
                if (ProperShotgunsSettings.damageRoundMode == ProperShotgunsSettings.StandardDamageRoundModeString)
                    __result = Mathf.RoundToInt(adjustedDamage);
                else
                    __result = GenMath.RoundRandom(adjustedDamage);
            }

        }

	}

}
