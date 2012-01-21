// Copyright (c) 2009 DotNetAnywhere
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing {
	public struct Color {

		public static Color FromKnownColor(KnownColor c) {
			return KnownColors.FromKnownColor(c);
		}

		public static Color FromArgb(int argb) {
			Color col = new Color();
			col.value = argb;
			return col;
		}

		public static Color FromArgb(int alpha, Color baseCol) {
			Color col = new Color();
			col.value = col.value & 0x00ffffff | (alpha << 24);
			return col;
		}

		private int value;

		public int ToArgb() {
			return value;
		}

		static public Color Transparent { get { return KnownColors.FromKnownColor(KnownColor.Transparent); } }
		static public Color AliceBlue { get { return KnownColors.FromKnownColor(KnownColor.AliceBlue); } }
		static public Color AntiqueWhite { get { return KnownColors.FromKnownColor(KnownColor.AntiqueWhite); } }
		static public Color Aqua { get { return KnownColors.FromKnownColor(KnownColor.Aqua); } }
		static public Color Aquamarine { get { return KnownColors.FromKnownColor(KnownColor.Aquamarine); } }
		static public Color Azure { get { return KnownColors.FromKnownColor(KnownColor.Azure); } }
		static public Color Beige { get { return KnownColors.FromKnownColor(KnownColor.Beige); } }
		static public Color Bisque { get { return KnownColors.FromKnownColor(KnownColor.Bisque); } }
		static public Color Black { get { return KnownColors.FromKnownColor(KnownColor.Black); } }
		static public Color BlanchedAlmond { get { return KnownColors.FromKnownColor(KnownColor.BlanchedAlmond); } }
		static public Color Blue { get { return KnownColors.FromKnownColor(KnownColor.Blue); } }
		static public Color BlueViolet { get { return KnownColors.FromKnownColor(KnownColor.BlueViolet); } }
		static public Color Brown { get { return KnownColors.FromKnownColor(KnownColor.Brown); } }
		static public Color BurlyWood { get { return KnownColors.FromKnownColor(KnownColor.BurlyWood); } }
		static public Color CadetBlue { get { return KnownColors.FromKnownColor(KnownColor.CadetBlue); } }
		static public Color Chartreuse { get { return KnownColors.FromKnownColor(KnownColor.Chartreuse); } }
		static public Color Chocolate { get { return KnownColors.FromKnownColor(KnownColor.Chocolate); } }
		static public Color Coral { get { return KnownColors.FromKnownColor(KnownColor.Coral); } }
		static public Color CornflowerBlue { get { return KnownColors.FromKnownColor(KnownColor.CornflowerBlue); } }
		static public Color Cornsilk { get { return KnownColors.FromKnownColor(KnownColor.Cornsilk); } }
		static public Color Crimson { get { return KnownColors.FromKnownColor(KnownColor.Crimson); } }
		static public Color Cyan { get { return KnownColors.FromKnownColor(KnownColor.Cyan); } }
		static public Color DarkBlue { get { return KnownColors.FromKnownColor(KnownColor.DarkBlue); } }
		static public Color DarkCyan { get { return KnownColors.FromKnownColor(KnownColor.DarkCyan); } }
		static public Color DarkGoldenrod { get { return KnownColors.FromKnownColor(KnownColor.DarkGoldenrod); } }
		static public Color DarkGray { get { return KnownColors.FromKnownColor(KnownColor.DarkGray); } }
		static public Color DarkGreen { get { return KnownColors.FromKnownColor(KnownColor.DarkGreen); } }
		static public Color DarkKhaki { get { return KnownColors.FromKnownColor(KnownColor.DarkKhaki); } }
		static public Color DarkMagenta { get { return KnownColors.FromKnownColor(KnownColor.DarkMagenta); } }
		static public Color DarkOliveGreen { get { return KnownColors.FromKnownColor(KnownColor.DarkOliveGreen); } }
		static public Color DarkOrange { get { return KnownColors.FromKnownColor(KnownColor.DarkOrange); } }
		static public Color DarkOrchid { get { return KnownColors.FromKnownColor(KnownColor.DarkOrchid); } }
		static public Color DarkRed { get { return KnownColors.FromKnownColor(KnownColor.DarkRed); } }
		static public Color DarkSalmon { get { return KnownColors.FromKnownColor(KnownColor.DarkSalmon); } }
		static public Color DarkSeaGreen { get { return KnownColors.FromKnownColor(KnownColor.DarkSeaGreen); } }
		static public Color DarkSlateBlue { get { return KnownColors.FromKnownColor(KnownColor.DarkSlateBlue); } }
		static public Color DarkSlateGray { get { return KnownColors.FromKnownColor(KnownColor.DarkSlateGray); } }
		static public Color DarkTurquoise { get { return KnownColors.FromKnownColor(KnownColor.DarkTurquoise); } }
		static public Color DarkViolet { get { return KnownColors.FromKnownColor(KnownColor.DarkViolet); } }
		static public Color DeepPink { get { return KnownColors.FromKnownColor(KnownColor.DeepPink); } }
		static public Color DeepSkyBlue { get { return KnownColors.FromKnownColor(KnownColor.DeepSkyBlue); } }
		static public Color DimGray { get { return KnownColors.FromKnownColor(KnownColor.DimGray); } }
		static public Color DodgerBlue { get { return KnownColors.FromKnownColor(KnownColor.DodgerBlue); } }
		static public Color Firebrick { get { return KnownColors.FromKnownColor(KnownColor.Firebrick); } }
		static public Color FloralWhite { get { return KnownColors.FromKnownColor(KnownColor.FloralWhite); } }
		static public Color ForestGreen { get { return KnownColors.FromKnownColor(KnownColor.ForestGreen); } }
		static public Color Fuchsia { get { return KnownColors.FromKnownColor(KnownColor.Fuchsia); } }
		static public Color Gainsboro { get { return KnownColors.FromKnownColor(KnownColor.Gainsboro); } }
		static public Color GhostWhite { get { return KnownColors.FromKnownColor(KnownColor.GhostWhite); } }
		static public Color Gold { get { return KnownColors.FromKnownColor(KnownColor.Gold); } }
		static public Color Goldenrod { get { return KnownColors.FromKnownColor(KnownColor.Goldenrod); } }
		static public Color Gray { get { return KnownColors.FromKnownColor(KnownColor.Gray); } }
		static public Color Green { get { return KnownColors.FromKnownColor(KnownColor.Green); } }
		static public Color GreenYellow { get { return KnownColors.FromKnownColor(KnownColor.GreenYellow); } }
		static public Color Honeydew { get { return KnownColors.FromKnownColor(KnownColor.Honeydew); } }
		static public Color HotPink { get { return KnownColors.FromKnownColor(KnownColor.HotPink); } }
		static public Color IndianRed { get { return KnownColors.FromKnownColor(KnownColor.IndianRed); } }
		static public Color Indigo { get { return KnownColors.FromKnownColor(KnownColor.Indigo); } }
		static public Color Ivory { get { return KnownColors.FromKnownColor(KnownColor.Ivory); } }
		static public Color Khaki { get { return KnownColors.FromKnownColor(KnownColor.Khaki); } }
		static public Color Lavender { get { return KnownColors.FromKnownColor(KnownColor.Lavender); } }
		static public Color LavenderBlush { get { return KnownColors.FromKnownColor(KnownColor.LavenderBlush); } }
		static public Color LawnGreen { get { return KnownColors.FromKnownColor(KnownColor.LawnGreen); } }
		static public Color LemonChiffon { get { return KnownColors.FromKnownColor(KnownColor.LemonChiffon); } }
		static public Color LightBlue { get { return KnownColors.FromKnownColor(KnownColor.LightBlue); } }
		static public Color LightCoral { get { return KnownColors.FromKnownColor(KnownColor.LightCoral); } }
		static public Color LightCyan { get { return KnownColors.FromKnownColor(KnownColor.LightCyan); } }
		static public Color LightGoldenrodYellow { get { return KnownColors.FromKnownColor(KnownColor.LightGoldenrodYellow); } }
		static public Color LightGreen { get { return KnownColors.FromKnownColor(KnownColor.LightGreen); } }
		static public Color LightGray { get { return KnownColors.FromKnownColor(KnownColor.LightGray); } }
		static public Color LightPink { get { return KnownColors.FromKnownColor(KnownColor.LightPink); } }
		static public Color LightSalmon { get { return KnownColors.FromKnownColor(KnownColor.LightSalmon); } }
		static public Color LightSeaGreen { get { return KnownColors.FromKnownColor(KnownColor.LightSeaGreen); } }
		static public Color LightSkyBlue { get { return KnownColors.FromKnownColor(KnownColor.LightSkyBlue); } }
		static public Color LightSlateGray { get { return KnownColors.FromKnownColor(KnownColor.LightSlateGray); } }
		static public Color LightSteelBlue { get { return KnownColors.FromKnownColor(KnownColor.LightSteelBlue); } }
		static public Color LightYellow { get { return KnownColors.FromKnownColor(KnownColor.LightYellow); } }
		static public Color Lime { get { return KnownColors.FromKnownColor(KnownColor.Lime); } }
		static public Color LimeGreen { get { return KnownColors.FromKnownColor(KnownColor.LimeGreen); } }
		static public Color Linen { get { return KnownColors.FromKnownColor(KnownColor.Linen); } }
		static public Color Magenta { get { return KnownColors.FromKnownColor(KnownColor.Magenta); } }
		static public Color Maroon { get { return KnownColors.FromKnownColor(KnownColor.Maroon); } }
		static public Color MediumAquamarine { get { return KnownColors.FromKnownColor(KnownColor.MediumAquamarine); } }
		static public Color MediumBlue { get { return KnownColors.FromKnownColor(KnownColor.MediumBlue); } }
		static public Color MediumOrchid { get { return KnownColors.FromKnownColor(KnownColor.MediumOrchid); } }
		static public Color MediumPurple { get { return KnownColors.FromKnownColor(KnownColor.MediumPurple); } }
		static public Color MediumSeaGreen { get { return KnownColors.FromKnownColor(KnownColor.MediumSeaGreen); } }
		static public Color MediumSlateBlue { get { return KnownColors.FromKnownColor(KnownColor.MediumSlateBlue); } }
		static public Color MediumSpringGreen { get { return KnownColors.FromKnownColor(KnownColor.MediumSpringGreen); } }
		static public Color MediumTurquoise { get { return KnownColors.FromKnownColor(KnownColor.MediumTurquoise); } }
		static public Color MediumVioletRed { get { return KnownColors.FromKnownColor(KnownColor.MediumVioletRed); } }
		static public Color MidnightBlue { get { return KnownColors.FromKnownColor(KnownColor.MidnightBlue); } }
		static public Color MintCream { get { return KnownColors.FromKnownColor(KnownColor.MintCream); } }
		static public Color MistyRose { get { return KnownColors.FromKnownColor(KnownColor.MistyRose); } }
		static public Color Moccasin { get { return KnownColors.FromKnownColor(KnownColor.Moccasin); } }
		static public Color NavajoWhite { get { return KnownColors.FromKnownColor(KnownColor.NavajoWhite); } }
		static public Color Navy { get { return KnownColors.FromKnownColor(KnownColor.Navy); } }
		static public Color OldLace { get { return KnownColors.FromKnownColor(KnownColor.OldLace); } }
		static public Color Olive { get { return KnownColors.FromKnownColor(KnownColor.Olive); } }
		static public Color OliveDrab { get { return KnownColors.FromKnownColor(KnownColor.OliveDrab); } }
		static public Color Orange { get { return KnownColors.FromKnownColor(KnownColor.Orange); } }
		static public Color OrangeRed { get { return KnownColors.FromKnownColor(KnownColor.OrangeRed); } }
		static public Color Orchid { get { return KnownColors.FromKnownColor(KnownColor.Orchid); } }
		static public Color PaleGoldenrod { get { return KnownColors.FromKnownColor(KnownColor.PaleGoldenrod); } }
		static public Color PaleGreen { get { return KnownColors.FromKnownColor(KnownColor.PaleGreen); } }
		static public Color PaleTurquoise { get { return KnownColors.FromKnownColor(KnownColor.PaleTurquoise); } }
		static public Color PaleVioletRed { get { return KnownColors.FromKnownColor(KnownColor.PaleVioletRed); } }
		static public Color PapayaWhip { get { return KnownColors.FromKnownColor(KnownColor.PapayaWhip); } }
		static public Color PeachPuff { get { return KnownColors.FromKnownColor(KnownColor.PeachPuff); } }
		static public Color Peru { get { return KnownColors.FromKnownColor(KnownColor.Peru); } }
		static public Color Pink { get { return KnownColors.FromKnownColor(KnownColor.Pink); } }
		static public Color Plum { get { return KnownColors.FromKnownColor(KnownColor.Plum); } }
		static public Color PowderBlue { get { return KnownColors.FromKnownColor(KnownColor.PowderBlue); } }
		static public Color Purple { get { return KnownColors.FromKnownColor(KnownColor.Purple); } }
		static public Color Red { get { return KnownColors.FromKnownColor(KnownColor.Red); } }
		static public Color RosyBrown { get { return KnownColors.FromKnownColor(KnownColor.RosyBrown); } }
		static public Color RoyalBlue { get { return KnownColors.FromKnownColor(KnownColor.RoyalBlue); } }
		static public Color SaddleBrown { get { return KnownColors.FromKnownColor(KnownColor.SaddleBrown); } }
		static public Color Salmon { get { return KnownColors.FromKnownColor(KnownColor.Salmon); } }
		static public Color SandyBrown { get { return KnownColors.FromKnownColor(KnownColor.SandyBrown); } }
		static public Color SeaGreen { get { return KnownColors.FromKnownColor(KnownColor.SeaGreen); } }
		static public Color SeaShell { get { return KnownColors.FromKnownColor(KnownColor.SeaShell); } }
		static public Color Sienna { get { return KnownColors.FromKnownColor(KnownColor.Sienna); } }
		static public Color Silver { get { return KnownColors.FromKnownColor(KnownColor.Silver); } }
		static public Color SkyBlue { get { return KnownColors.FromKnownColor(KnownColor.SkyBlue); } }
		static public Color SlateBlue { get { return KnownColors.FromKnownColor(KnownColor.SlateBlue); } }
		static public Color SlateGray { get { return KnownColors.FromKnownColor(KnownColor.SlateGray); } }
		static public Color Snow { get { return KnownColors.FromKnownColor(KnownColor.Snow); } }
		static public Color SpringGreen { get { return KnownColors.FromKnownColor(KnownColor.SpringGreen); } }
		static public Color SteelBlue { get { return KnownColors.FromKnownColor(KnownColor.SteelBlue); } }
		static public Color Tan { get { return KnownColors.FromKnownColor(KnownColor.Tan); } }
		static public Color Teal { get { return KnownColors.FromKnownColor(KnownColor.Teal); } }
		static public Color Thistle { get { return KnownColors.FromKnownColor(KnownColor.Thistle); } }
		static public Color Tomato { get { return KnownColors.FromKnownColor(KnownColor.Tomato); } }
		static public Color Turquoise { get { return KnownColors.FromKnownColor(KnownColor.Turquoise); } }
		static public Color Violet { get { return KnownColors.FromKnownColor(KnownColor.Violet); } }
		static public Color Wheat { get { return KnownColors.FromKnownColor(KnownColor.Wheat); } }
		static public Color White { get { return KnownColors.FromKnownColor(KnownColor.White); } }
		static public Color WhiteSmoke { get { return KnownColors.FromKnownColor(KnownColor.WhiteSmoke); } }
		static public Color Yellow { get { return KnownColors.FromKnownColor(KnownColor.Yellow); } }
		static public Color YellowGreen { get { return KnownColors.FromKnownColor(KnownColor.YellowGreen); } }

	}
}
