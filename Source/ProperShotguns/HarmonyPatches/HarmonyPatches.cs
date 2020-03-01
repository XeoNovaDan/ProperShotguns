﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using RimWorld;
using Verse;
using HarmonyLib;
using UnityEngine;

namespace ProperShotguns
{
	[StaticConstructorOnStartup]
	internal static class HarmonyPatches
	{

		static HarmonyPatches()
		{
			var h = new Harmony("XeoNovaDan.ProperShotguns");
            h.PatchAll();
        }

	}
}
