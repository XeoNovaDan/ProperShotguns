using System;
using Verse;

namespace ProperShotguns
{
	public class ShotgunExtension : DefModExtension
	{

		private static readonly ShotgunExtension defaultValues = new ShotgunExtension();

		public static ShotgunExtension Get(Def def) => def.GetModExtension<ShotgunExtension>() ?? defaultValues;

		public int pelletCount = 1;

	}
}
