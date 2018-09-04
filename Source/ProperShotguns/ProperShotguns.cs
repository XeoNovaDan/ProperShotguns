using System;
using UnityEngine;
using Verse;

namespace ProperShotguns
{
	public class ProperShotguns : Mod
	{
		public ProperShotguns(ModContentPack content) : base(content)
		{
			base.GetSettings<ProperShotgunsSettings>();
		}

		public override string SettingsCategory()
		{
			return Translator.Translate("ProperShotgunsSettingsCategory");
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			base.GetSettings<ProperShotgunsSettings>().DoWindowContents(inRect);
		}

		public ProperShotgunsSettings settings;
	}
}
