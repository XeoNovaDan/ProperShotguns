using System;
using Verse;
using RimWorld;

namespace ProperShotguns
{
	public class CompProjectileVerbCache : ThingComp
    {

        public Type cachedVerbClass;

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref cachedVerbClass, "cachedVerbClass");
            base.PostExposeData();
        }

    }
}
