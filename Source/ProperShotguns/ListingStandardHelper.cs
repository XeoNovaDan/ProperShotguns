using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ProperShotguns
{
	public static class ListingStandardHelper
	{
		public static float Gap
		{
			get
			{
				return ListingStandardHelper.gap;
			}
			set
			{
				ListingStandardHelper.gap = value;
			}
		}

		public static float LineGap
		{
			get
			{
				return ListingStandardHelper.lineGap;
			}
			set
			{
				ListingStandardHelper.lineGap = value;
			}
		}

		public static void AddHorizontalLine(this Listing_Standard listing_Standard, float? gap = null)
		{
			listing_Standard.Gap(gap ?? ListingStandardHelper.lineGap);
			listing_Standard.GapLine(gap ?? ListingStandardHelper.lineGap);
		}

		public static void AddLabelLine(this Listing_Standard listing_Standard, string label, float? height = null)
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			Rect rect = listing_Standard.GetRect(height);
			Widgets.Label(rect, label);
		}

		public static Rect GetRect(this Listing_Standard listing_Standard, float? height = null)
		{
			return listing_Standard.GetRect(height ?? Text.LineHeight);
		}

		public static Rect LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf, float leftPartPct = 0.5f, float? height = null)
		{
			Rect rect = listing_Standard.GetRect(height);
			leftHalf = GenUI.Rounded(GenUI.LeftPart(rect, leftPartPct));
			return rect;
		}

		public static Rect LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf, out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
		{
			Rect rect = listing_Standard.LineRectSpilter(out leftHalf, leftPartPct, height);
			rightHalf = GenUI.Rounded(GenUI.RightPart(rect, 1f - leftPartPct));
			return rect;
		}

		public static void AddLabeledRadioList(this Listing_Standard listing_Standard, string header, string[] labels, ref string val, float? headerHeight = null)
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			listing_Standard.AddLabelLine(header, headerHeight);
			listing_Standard.AddRadioList(ListingStandardHelper.GenerateLabeledRadioValues(labels), ref val, null);
		}

		public static void AddLabeledRadioList<T>(this Listing_Standard listing_Standard, string header, Dictionary<string, T> dict, ref T val, float? headerHeight = null)
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			listing_Standard.AddLabelLine(header, headerHeight);
			listing_Standard.AddRadioList(ListingStandardHelper.GenerateLabeledRadioValues<T>(dict), ref val, null);
		}

		private static void AddRadioList<T>(this Listing_Standard listing_Standard, List<ListingStandardHelper.LabeledRadioValue<T>> items, ref T val, float? height = null)
		{
			foreach (ListingStandardHelper.LabeledRadioValue<T> labeledRadioValue in items)
			{
				listing_Standard.Gap(ListingStandardHelper.Gap);
				Rect rect = listing_Standard.GetRect(height);
				bool flag = Widgets.RadioButtonLabeled(rect, labeledRadioValue.Label, EqualityComparer<T>.Default.Equals(labeledRadioValue.Value, val));
				if (flag)
				{
					val = labeledRadioValue.Value;
				}
			}
		}

		private static List<ListingStandardHelper.LabeledRadioValue<string>> GenerateLabeledRadioValues(string[] labels)
		{
			List<ListingStandardHelper.LabeledRadioValue<string>> list = new List<ListingStandardHelper.LabeledRadioValue<string>>();
			foreach (string text in labels)
			{
				list.Add(new ListingStandardHelper.LabeledRadioValue<string>(text, text));
			}
			return list;
		}

		private static List<ListingStandardHelper.LabeledRadioValue<T>> GenerateLabeledRadioValues<T>(Dictionary<string, T> dict)
		{
			List<ListingStandardHelper.LabeledRadioValue<T>> list = new List<ListingStandardHelper.LabeledRadioValue<T>>();
			foreach (KeyValuePair<string, T> keyValuePair in dict)
			{
				list.Add(new ListingStandardHelper.LabeledRadioValue<T>(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		public static void AddLabeledTextField(this Listing_Standard listing_Standard, string label, ref string settingsValue, float leftPartPct = 0.5f)
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			Rect rect;
			Rect rect2;
			listing_Standard.LineRectSpilter(out rect, out rect2, leftPartPct, null);
			Widgets.Label(rect, label);
			string text = settingsValue.ToString();
			settingsValue = Widgets.TextField(rect2, text);
		}

		public static void AddLabeledNumericalTextField<T>(this Listing_Standard listing_Standard, string label, ref T settingsValue, float leftPartPct = 0.5f, float minValue = 1f, float maxValue = 100000f) where T : struct
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			Rect rect;
			Rect rect2;
			listing_Standard.LineRectSpilter(out rect, out rect2, leftPartPct, null);
			Widgets.Label(rect, label);
			string text = settingsValue.ToString();
			Widgets.TextFieldNumeric<T>(rect2, ref settingsValue, ref text, minValue, maxValue);
		}

		public static void AddLabeledCheckbox(this Listing_Standard listing_Standard, string label, ref bool settingsValue)
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			listing_Standard.CheckboxLabeled(label, ref settingsValue, null);
		}

		public static void AddLabeledSlider(this Listing_Standard listing_Standard, string label, ref float value, float leftValue, float rightValue)
		{
			listing_Standard.Gap(ListingStandardHelper.Gap);
			Rect rect;
			Rect rect2;
			listing_Standard.LineRectSpilter(out rect, out rect2, 0.5f, null);
			Widgets.Label(rect, label);
			float num = value;
			value = Widgets.HorizontalSlider(GenUI.BottomPart(rect2, 0.7f), num, leftValue, rightValue, false, null, null, null, -1f);
		}

		public static void SliderLabeled(this Listing_Standard ls, string label, ref int val, string format, float min = 0f, float max = 100f, string tooltip = null)
		{
			float num = (float)val;
			ls.SliderLabeled(label, ref num, format, min, max, null);
			val = (int)num;
		}

		public static void SliderLabeled(this Listing_Standard ls, string label, ref float val, string format, float min = 0f, float max = 1f, string tooltip = null)
		{
			Rect rect = ls.GetRect(Text.LineHeight);
			Rect rect2 = GenUI.Rounded(GenUI.LeftPart(rect, 0.7f));
			Rect rect3 = GenUI.Rounded(GenUI.LeftPart(GenUI.Rounded(GenUI.RightPart(rect, 0.3f)), 0.67f));
			Rect rect4 = GenUI.Rounded(GenUI.RightPart(rect, 0.1f));
			TextAnchor anchor = Text.Anchor;
			Text.Anchor = TextAnchor.MiddleLeft;
			Widgets.Label(rect2, label);
			float num = Widgets.HorizontalSlider(rect3, val, min, max, true, null, null, null, -1f);
			val = num;
			Text.Anchor = TextAnchor.MiddleRight;
			Widgets.Label(rect4, string.Format(format, val));
			bool flag = !GenText.NullOrEmpty(tooltip);
			if (flag)
			{
				TooltipHandler.TipRegion(rect, tooltip);
			}
			Text.Anchor = anchor;
			ls.Gap(ls.verticalSpacing);
		}

		private static float gap = 12f;

		private static float lineGap = 3f;

		public class LabeledRadioValue<T>
		{
			public LabeledRadioValue(string label, T val)
			{
				this.Label = label;
				this.Value = val;
			}

			public string Label
			{
				get
				{
					return this.label;
				}
				set
				{
					this.label = value;
				}
			}

			public T Value
			{
				get
				{
					return this.val;
				}
				set
				{
					this.val = value;
				}
			}

			private string label;

			private T val;
		}
	}
}
