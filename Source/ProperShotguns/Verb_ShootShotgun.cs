using System;
using Verse;
using RimWorld;

namespace ProperShotguns
{
	public class Verb_ShootShotgun : Verb_LaunchProjectile
	{

        protected override int ShotsPerBurst => verbProps.burstShotCount;

        public override void WarmupComplete()
        {
            base.WarmupComplete();
            Pawn pawn = currentTarget.Thing as Pawn;
            if (pawn != null && !pawn.Downed && CasterIsPawn && CasterPawn.skills != null)
            {
                float baseExp = pawn.HostileTo(caster) ? SkillTuning.XpPerSecondFiringHostile : SkillTuning.XpPerSecondFiringNonHostile;
                float cycleTime = verbProps.AdjustedFullCycleTime(this, CasterPawn);
                CasterPawn.skills.Learn(SkillDefOf.Shooting, baseExp * cycleTime, false);
            }
        }

        protected override bool TryCastShot()
		{
			bool castedShot = base.TryCastShot();
            if (castedShot && CasterIsPawn)
                CasterPawn.records.Increment(RecordDefOf.ShotsFired);

			ShotgunExtension shotgunExtension = verbProps.defaultProjectile.GetModExtension<ShotgunExtension>() ?? ShotgunExtension.defaultValues;
			if (castedShot && shotgunExtension.pelletCount - 1 > 0)
			{
				for (int i = 0; i < shotgunExtension.pelletCount - 1; i++)
				{
					base.TryCastShot();
				}
			}

			return castedShot;
		}

    }
}
