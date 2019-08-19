using System;
using System.Collections;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace ProperShotguns
{
	
    [StaticConstructorOnStartup]
    public static class StaticConstructorClass
    {

        static StaticConstructorClass()
        {

            foreach (var tDef in DefDatabase<ThingDef>.AllDefs)
            {
                // Add verb caches to all projectile defs
                if (typeof(Projectile).IsAssignableFrom(tDef.thingClass))
                {
                    if (tDef.comps == null)
                        tDef.comps = new List<CompProperties>();
                    tDef.comps.Add(new CompProperties(typeof(CompProjectileVerbCache)));
                }
            }

        }

    }

}
