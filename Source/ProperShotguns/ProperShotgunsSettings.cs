using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace ProperShotguns
{
	public class ProperShotgunsSettings : ModSettings
	{

        public void DoWindowContents(Rect wrect)
		{
			var options = new Listing_Standard();
			var color = GUI.color;
			options.Begin(wrect);

			GUI.color = color;
			Text.Font = GameFont.Small;
			Text.Anchor = TextAnchor.UpperLeft;
			options.Gap();

			options.Label("ProperShotguns.ShotgunDamageRounding".Translate());
			var shotgunDamageRoundingOpts = Enum.GetValues(typeof(ShotgunDamageRoundMode)).Cast<ShotgunDamageRoundMode>().ToList();
			for (int i = 0; i < shotgunDamageRoundingOpts.Count; i++)
			{
				var curOpt = shotgunDamageRoundingOpts[i];
				if (options.RadioButton($"ProperShotguns.ShotgunDamageRounding_{curOpt}".Translate(), damageRoundMode == curOpt, 12,
					$"ProperShotguns.ShotgunDamageRounding_{curOpt}_Desc".Translate()))
					damageRoundMode = curOpt;
			}

			options.End();
			base.Mod.GetSettings<ProperShotgunsSettings>().Write();
		}

		public override void ExposeData()
		{
			Scribe_Values.Look(ref damageRoundMode, "damageRoundMode", ShotgunDamageRoundMode.Random);
		}

		public static ShotgunDamageRoundMode damageRoundMode = ShotgunDamageRoundMode.Random;

	}
}
