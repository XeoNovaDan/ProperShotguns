using HarmonyLib;
using System;
using UnityEngine;
using Verse;

namespace ProperShotguns
{
	public class ProperShotguns : Mod
	{
		public ProperShotguns(ModContentPack content) : base(content)
		{
			harmonyInstance = new Harmony("XeoNovaDan.ProperShotguns");
			settings = GetSettings<ProperShotgunsSettings>();
		}

		public override string SettingsCategory()
		{
			return "ProperShotguns.SettingsCategory".Translate();
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			settings.DoWindowContents(inRect);
		}

		public static Harmony harmonyInstance;
		public static ProperShotgunsSettings settings;

	}
}
