using System;
using UnityEngine;
using Verse;

namespace ProperShotguns
{
	public class ProperShotgunsSettings : ModSettings
	{
		public void DoWindowContents(Rect wrect)
		{
			Listing_Standard listing_Standard = new Listing_Standard();
			Color color = GUI.color;
			listing_Standard.Begin(wrect);
			GUI.color = color;
			Text.Font = GameFont.Small;
			Text.Anchor = TextAnchor.UpperLeft;
			listing_Standard.Gap(12f);
			listing_Standard.AddLabeledRadioList(Translator.Translate("DamageRoundingMode"), ProperShotgunsSettings.damageRoundModes, ref ProperShotgunsSettings.damageRoundMode, null);
			listing_Standard.End();
			base.Mod.GetSettings<ProperShotgunsSettings>().Write();
		}

		public override void ExposeData()
		{
			Scribe_Values.Look<string>(ref ProperShotgunsSettings.damageRoundMode, "damageRoundMode", "    Standard", false);
		}

		public static string damageRoundMode = "    Standard";

		private static string[] damageRoundModes = new string[]
		{
			"    Standard",
			"    Random"
		};
	}
}
