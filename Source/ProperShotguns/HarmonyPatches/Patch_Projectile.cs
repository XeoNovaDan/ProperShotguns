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

	static class Patch_Projectile
	{

        [HarmonyPatch(typeof(Projectile), nameof(Projectile.DamageAmount), MethodType.Getter)]
        public static class get_DamageAmount
        {

            public static void Postfix(Projectile __instance, ref int __result)
            {
                var verbCache = __instance.TryGetComp<CompProjectileVerbCache>();
                if (verbCache != null && verbCache.cachedVerbClass is Type t && t.IsAssignableFrom(typeof(Verb_ShootShotgun)))
                {
                    var shotgunExtension = ShotgunExtension.Get(__instance.def);
                    float adjustedDamage = (float)__result / shotgunExtension.pelletCount;

                    // Determine pellet damage
                    switch(ProperShotgunsSettings.damageRoundMode)
                    {
                        case ShotgunDamageRoundMode.Standard:
                            __result = Mathf.RoundToInt(adjustedDamage);
                            break;
                        case ShotgunDamageRoundMode.Random:
                            __result = GenMath.RoundRandom(adjustedDamage);
                            break;
                        case ShotgunDamageRoundMode.Ceil:
                            __result = Mathf.CeilToInt(adjustedDamage);
                            break;
                        case ShotgunDamageRoundMode.Floor:
                            __result = Mathf.FloorToInt(adjustedDamage);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

        }

        [HarmonyPatch(typeof(Projectile), nameof(Projectile.Launch),
            new Type[] { typeof(Thing), typeof(Vector3), typeof(LocalTargetInfo), typeof(LocalTargetInfo), typeof(ProjectileHitFlags), typeof(Thing), typeof(ThingDef) })]
        public static class Launch
        {

            public static void Postfix(Projectile __instance, Thing launcher)
            {
                
                var verbCache = __instance.TryGetComp<CompProjectileVerbCache>();
                if (verbCache != null)
                {
                    var attackTargetSearcher = launcher as IAttackTargetSearcher;
                    if (attackTargetSearcher != null)
                        verbCache.cachedVerbClass = attackTargetSearcher.CurrentEffectiveVerb.verbProps.verbClass;
                    else if (__instance.def.HasModExtension<ShotgunExtension>())
                        verbCache.cachedVerbClass = typeof(Verb_ShootShotgun);
                }
                else
                    Log.Warning($"CompProjectileVerbCache for {__instance} is null.");
            }

        }

    }

}
