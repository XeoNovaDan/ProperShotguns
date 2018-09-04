using System;
using Verse;

namespace ProperShotguns
{
	public class Verb_ShootShotgun : Verb_Shoot
	{
		protected override bool TryCastShot()
		{
			bool flag = base.TryCastShot();
			ShotgunExtension shotgunExtension = this.verbProps.defaultProjectile.GetModExtension<ShotgunExtension>() ?? ShotgunExtension.defaultValues;
			bool flag2 = flag && shotgunExtension.pelletCount - 1 > 0;
			if (flag2)
			{
				for (int i = 0; i < shotgunExtension.pelletCount - 1; i++)
				{
					base.TryCastShot();
				}
			}
			return flag;
		}
	}
}
