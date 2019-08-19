using System;
using Verse;

namespace ProperShotguns
{
	public class ShotgunExtension : DefModExtension
	{
		public static readonly ShotgunExtension defaultValues = new ShotgunExtension();

		public int pelletCount = 1;
	}
}
