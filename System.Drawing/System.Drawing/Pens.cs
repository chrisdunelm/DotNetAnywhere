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
	public static class Pens {

		private static Pen aliceblue;
		private static Pen antiquewhite;
		private static Pen aqua;
		private static Pen aquamarine;
		private static Pen azure;
		private static Pen beige;
		private static Pen bisque;
		private static Pen black;
		private static Pen blanchedalmond;
		private static Pen blue;
		private static Pen blueviolet;
		private static Pen brown;
		private static Pen burlywood;
		private static Pen cadetblue;
		private static Pen chartreuse;
		private static Pen chocolate;
		private static Pen coral;
		private static Pen cornflowerblue;
		private static Pen cornsilk;
		private static Pen crimson;
		private static Pen cyan;
		private static Pen darkblue;
		private static Pen darkcyan;
		private static Pen darkgoldenrod;
		private static Pen darkgray;
		private static Pen darkgreen;
		private static Pen darkkhaki;
		private static Pen darkmagenta;
		private static Pen darkolivegreen;
		private static Pen darkorange;
		private static Pen darkorchid;
		private static Pen darkred;
		private static Pen darksalmon;
		private static Pen darkseagreen;
		private static Pen darkslateblue;
		private static Pen darkslategray;
		private static Pen darkturquoise;
		private static Pen darkviolet;
		private static Pen deeppink;
		private static Pen deepskyblue;
		private static Pen dimgray;
		private static Pen dodgerblue;
		private static Pen firebrick;
		private static Pen floralwhite;
		private static Pen forestgreen;
		private static Pen fuchsia;
		private static Pen gainsboro;
		private static Pen ghostwhite;
		private static Pen gold;
		private static Pen goldenrod;
		private static Pen gray;
		private static Pen green;
		private static Pen greenyellow;
		private static Pen honeydew;
		private static Pen hotpink;
		private static Pen indianred;
		private static Pen indigo;
		private static Pen ivory;
		private static Pen khaki;
		private static Pen lavender;
		private static Pen lavenderblush;
		private static Pen lawngreen;
		private static Pen lemonchiffon;
		private static Pen lightblue;
		private static Pen lightcoral;
		private static Pen lightcyan;
		private static Pen lightgoldenrodyellow;
		private static Pen lightgray;
		private static Pen lightgreen;
		private static Pen lightpink;
		private static Pen lightsalmon;
		private static Pen lightseagreen;
		private static Pen lightskyblue;
		private static Pen lightslategray;
		private static Pen lightsteelblue;
		private static Pen lightyellow;
		private static Pen lime;
		private static Pen limegreen;
		private static Pen linen;
		private static Pen magenta;
		private static Pen maroon;
		private static Pen mediumaquamarine;
		private static Pen mediumblue;
		private static Pen mediumorchid;
		private static Pen mediumpurple;
		private static Pen mediumseagreen;
		private static Pen mediumslateblue;
		private static Pen mediumspringgreen;
		private static Pen mediumturquoise;
		private static Pen mediumvioletred;
		private static Pen midnightblue;
		private static Pen mintcream;
		private static Pen mistyrose;
		private static Pen moccasin;
		private static Pen navajowhite;
		private static Pen navy;
		private static Pen oldlace;
		private static Pen olive;
		private static Pen olivedrab;
		private static Pen orange;
		private static Pen orangered;
		private static Pen orchid;
		private static Pen palegoldenrod;
		private static Pen palegreen;
		private static Pen paleturquoise;
		private static Pen palevioletred;
		private static Pen papayawhip;
		private static Pen peachpuff;
		private static Pen peru;
		private static Pen pink;
		private static Pen plum;
		private static Pen powderblue;
		private static Pen purple;
		private static Pen red;
		private static Pen rosybrown;
		private static Pen royalblue;
		private static Pen saddlebrown;
		private static Pen salmon;
		private static Pen sandybrown;
		private static Pen seagreen;
		private static Pen seashell;
		private static Pen sienna;
		private static Pen silver;
		private static Pen skyblue;
		private static Pen slateblue;
		private static Pen slategray;
		private static Pen snow;
		private static Pen springgreen;
		private static Pen steelblue;
		private static Pen tan;
		private static Pen teal;
		private static Pen thistle;
		private static Pen tomato;
		private static Pen transparent;
		private static Pen turquoise;
		private static Pen violet;
		private static Pen wheat;
		private static Pen white;
		private static Pen whitesmoke;
		private static Pen yellow;
		private static Pen yellowgreen;

		public static Pen AliceBlue {
			get {
				if (aliceblue == null) {
					aliceblue = new Pen(Color.AliceBlue);
					aliceblue.canChange = false;
				}
				return aliceblue;
			}
		}

		public static Pen AntiqueWhite {
			get {
				if (antiquewhite == null) {
					antiquewhite = new Pen(Color.AntiqueWhite);
					antiquewhite.canChange = false;
				}
				return antiquewhite;
			}
		}

		public static Pen Aqua {
			get {
				if (aqua == null) {
					aqua = new Pen(Color.Aqua);
					aqua.canChange = false;
				}
				return aqua;
			}
		}

		public static Pen Aquamarine {
			get {
				if (aquamarine == null) {
					aquamarine = new Pen(Color.Aquamarine);
					aquamarine.canChange = false;
				}
				return aquamarine;
			}
		}

		public static Pen Azure {
			get {
				if (azure == null) {
					azure = new Pen(Color.Azure);
					azure.canChange = false;
				}
				return azure;
			}
		}

		public static Pen Beige {
			get {
				if (beige == null) {
					beige = new Pen(Color.Beige);
					beige.canChange = false;
				}
				return beige;
			}
		}

		public static Pen Bisque {
			get {
				if (bisque == null) {
					bisque = new Pen(Color.Bisque);
					bisque.canChange = false;
				}
				return bisque;
			}
		}

		public static Pen Black {
			get {
				if (black == null) {
					black = new Pen(Color.Black);
					black.canChange = false;
				}
				return black;
			}
		}

		public static Pen BlanchedAlmond {
			get {
				if (blanchedalmond == null) {
					blanchedalmond = new Pen(Color.BlanchedAlmond);
					blanchedalmond.canChange = false;
				}
				return blanchedalmond;
			}
		}

		public static Pen Blue {
			get {
				if (blue == null) {
					blue = new Pen(Color.Blue);
					blue.canChange = false;
				}
				return blue;
			}
		}

		public static Pen BlueViolet {
			get {
				if (blueviolet == null) {
					blueviolet = new Pen(Color.BlueViolet);
					blueviolet.canChange = false;
				}
				return blueviolet;
			}
		}

		public static Pen Brown {
			get {
				if (brown == null) {
					brown = new Pen(Color.Brown);
					brown.canChange = false;
				}
				return brown;
			}
		}

		public static Pen BurlyWood {
			get {
				if (burlywood == null) {
					burlywood = new Pen(Color.BurlyWood);
					burlywood.canChange = false;
				}
				return burlywood;
			}
		}

		public static Pen CadetBlue {
			get {
				if (cadetblue == null) {
					cadetblue = new Pen(Color.CadetBlue);
					cadetblue.canChange = false;
				}
				return cadetblue;
			}
		}

		public static Pen Chartreuse {
			get {
				if (chartreuse == null) {
					chartreuse = new Pen(Color.Chartreuse);
					chartreuse.canChange = false;
				}
				return chartreuse;
			}
		}

		public static Pen Chocolate {
			get {
				if (chocolate == null) {
					chocolate = new Pen(Color.Chocolate);
					chocolate.canChange = false;
				}
				return chocolate;
			}
		}

		public static Pen Coral {
			get {
				if (coral == null) {
					coral = new Pen(Color.Coral);
					coral.canChange = false;
				}
				return coral;
			}
		}

		public static Pen CornflowerBlue {
			get {
				if (cornflowerblue == null) {
					cornflowerblue = new Pen(Color.CornflowerBlue);
					cornflowerblue.canChange = false;
				}
				return cornflowerblue;
			}
		}

		public static Pen Cornsilk {
			get {
				if (cornsilk == null) {
					cornsilk = new Pen(Color.Cornsilk);
					cornsilk.canChange = false;
				}
				return cornsilk;
			}
		}

		public static Pen Crimson {
			get {
				if (crimson == null) {
					crimson = new Pen(Color.Crimson);
					crimson.canChange = false;
				}
				return crimson;
			}
		}

		public static Pen Cyan {
			get {
				if (cyan == null) {
					cyan = new Pen(Color.Cyan);
					cyan.canChange = false;
				}
				return cyan;
			}
		}

		public static Pen DarkBlue {
			get {
				if (darkblue == null) {
					darkblue = new Pen(Color.DarkBlue);
					darkblue.canChange = false;
				}
				return darkblue;
			}
		}

		public static Pen DarkCyan {
			get {
				if (darkcyan == null) {
					darkcyan = new Pen(Color.DarkCyan);
					darkcyan.canChange = false;
				}
				return darkcyan;
			}
		}

		public static Pen DarkGoldenrod {
			get {
				if (darkgoldenrod == null) {
					darkgoldenrod = new Pen(Color.DarkGoldenrod);
					darkgoldenrod.canChange = false;
				}
				return darkgoldenrod;
			}
		}

		public static Pen DarkGray {
			get {
				if (darkgray == null) {
					darkgray = new Pen(Color.DarkGray);
					darkgray.canChange = false;
				}
				return darkgray;
			}
		}

		public static Pen DarkGreen {
			get {
				if (darkgreen == null) {
					darkgreen = new Pen(Color.DarkGreen);
					darkgreen.canChange = false;
				}
				return darkgreen;
			}
		}

		public static Pen DarkKhaki {
			get {
				if (darkkhaki == null) {
					darkkhaki = new Pen(Color.DarkKhaki);
					darkkhaki.canChange = false;
				}
				return darkkhaki;
			}
		}

		public static Pen DarkMagenta {
			get {
				if (darkmagenta == null) {
					darkmagenta = new Pen(Color.DarkMagenta);
					darkmagenta.canChange = false;
				}
				return darkmagenta;
			}
		}

		public static Pen DarkOliveGreen {
			get {
				if (darkolivegreen == null) {
					darkolivegreen = new Pen(Color.DarkOliveGreen);
					darkolivegreen.canChange = false;
				}
				return darkolivegreen;
			}
		}

		public static Pen DarkOrange {
			get {
				if (darkorange == null) {
					darkorange = new Pen(Color.DarkOrange);
					darkorange.canChange = false;
				}
				return darkorange;
			}
		}

		public static Pen DarkOrchid {
			get {
				if (darkorchid == null) {
					darkorchid = new Pen(Color.DarkOrchid);
					darkorchid.canChange = false;
				}
				return darkorchid;
			}
		}

		public static Pen DarkRed {
			get {
				if (darkred == null) {
					darkred = new Pen(Color.DarkRed);
					darkred.canChange = false;
				}
				return darkred;
			}
		}

		public static Pen DarkSalmon {
			get {
				if (darksalmon == null) {
					darksalmon = new Pen(Color.DarkSalmon);
					darksalmon.canChange = false;
				}
				return darksalmon;
			}
		}

		public static Pen DarkSeaGreen {
			get {
				if (darkseagreen == null) {
					darkseagreen = new Pen(Color.DarkSeaGreen);
					darkseagreen.canChange = false;
				}
				return darkseagreen;
			}
		}

		public static Pen DarkSlateBlue {
			get {
				if (darkslateblue == null) {
					darkslateblue = new Pen(Color.DarkSlateBlue);
					darkslateblue.canChange = false;
				}
				return darkslateblue;
			}
		}

		public static Pen DarkSlateGray {
			get {
				if (darkslategray == null) {
					darkslategray = new Pen(Color.DarkSlateGray);
					darkslategray.canChange = false;
				}
				return darkslategray;
			}
		}

		public static Pen DarkTurquoise {
			get {
				if (darkturquoise == null) {
					darkturquoise = new Pen(Color.DarkTurquoise);
					darkturquoise.canChange = false;
				}
				return darkturquoise;
			}
		}

		public static Pen DarkViolet {
			get {
				if (darkviolet == null) {
					darkviolet = new Pen(Color.DarkViolet);
					darkviolet.canChange = false;
				}
				return darkviolet;
			}
		}

		public static Pen DeepPink {
			get {
				if (deeppink == null) {
					deeppink = new Pen(Color.DeepPink);
					deeppink.canChange = false;
				}
				return deeppink;
			}
		}

		public static Pen DeepSkyBlue {
			get {
				if (deepskyblue == null) {
					deepskyblue = new Pen(Color.DeepSkyBlue);
					deepskyblue.canChange = false;
				}
				return deepskyblue;
			}
		}

		public static Pen DimGray {
			get {
				if (dimgray == null) {
					dimgray = new Pen(Color.DimGray);
					dimgray.canChange = false;
				}
				return dimgray;
			}
		}

		public static Pen DodgerBlue {
			get {
				if (dodgerblue == null) {
					dodgerblue = new Pen(Color.DodgerBlue);
					dodgerblue.canChange = false;
				}
				return dodgerblue;
			}
		}

		public static Pen Firebrick {
			get {
				if (firebrick == null) {
					firebrick = new Pen(Color.Firebrick);
					firebrick.canChange = false;
				}
				return firebrick;
			}
		}

		public static Pen FloralWhite {
			get {
				if (floralwhite == null) {
					floralwhite = new Pen(Color.FloralWhite);
					floralwhite.canChange = false;
				}
				return floralwhite;
			}
		}

		public static Pen ForestGreen {
			get {
				if (forestgreen == null) {
					forestgreen = new Pen(Color.ForestGreen);
					forestgreen.canChange = false;
				}
				return forestgreen;
			}
		}

		public static Pen Fuchsia {
			get {
				if (fuchsia == null) {
					fuchsia = new Pen(Color.Fuchsia);
					fuchsia.canChange = false;
				}
				return fuchsia;
			}
		}

		public static Pen Gainsboro {
			get {
				if (gainsboro == null) {
					gainsboro = new Pen(Color.Gainsboro);
					gainsboro.canChange = false;
				}
				return gainsboro;
			}
		}

		public static Pen GhostWhite {
			get {
				if (ghostwhite == null) {
					ghostwhite = new Pen(Color.GhostWhite);
					ghostwhite.canChange = false;
				}
				return ghostwhite;
			}
		}

		public static Pen Gold {
			get {
				if (gold == null) {
					gold = new Pen(Color.Gold);
					gold.canChange = false;
				}
				return gold;
			}
		}

		public static Pen Goldenrod {
			get {
				if (goldenrod == null) {
					goldenrod = new Pen(Color.Goldenrod);
					goldenrod.canChange = false;
				}
				return goldenrod;
			}
		}

		public static Pen Gray {
			get {
				if (gray == null) {
					gray = new Pen(Color.Gray);
					gray.canChange = false;
				}
				return gray;
			}
		}

		public static Pen Green {
			get {
				if (green == null) {
					green = new Pen(Color.Green);
					green.canChange = false;
				}
				return green;
			}
		}

		public static Pen GreenYellow {
			get {
				if (greenyellow == null) {
					greenyellow = new Pen(Color.GreenYellow);
					greenyellow.canChange = false;
				}
				return greenyellow;
			}
		}

		public static Pen Honeydew {
			get {
				if (honeydew == null) {
					honeydew = new Pen(Color.Honeydew);
					honeydew.canChange = false;
				}
				return honeydew;
			}
		}

		public static Pen HotPink {
			get {
				if (hotpink == null) {
					hotpink = new Pen(Color.HotPink);
					hotpink.canChange = false;
				}
				return hotpink;
			}
		}

		public static Pen IndianRed {
			get {
				if (indianred == null) {
					indianred = new Pen(Color.IndianRed);
					indianred.canChange = false;
				}
				return indianred;
			}
		}

		public static Pen Indigo {
			get {
				if (indigo == null) {
					indigo = new Pen(Color.Indigo);
					indigo.canChange = false;
				}
				return indigo;
			}
		}

		public static Pen Ivory {
			get {
				if (ivory == null) {
					ivory = new Pen(Color.Ivory);
					ivory.canChange = false;
				}
				return ivory;
			}
		}

		public static Pen Khaki {
			get {
				if (khaki == null) {
					khaki = new Pen(Color.Khaki);
					khaki.canChange = false;
				}
				return khaki;
			}
		}

		public static Pen Lavender {
			get {
				if (lavender == null) {
					lavender = new Pen(Color.Lavender);
					lavender.canChange = false;
				}
				return lavender;
			}
		}

		public static Pen LavenderBlush {
			get {
				if (lavenderblush == null) {
					lavenderblush = new Pen(Color.LavenderBlush);
					lavenderblush.canChange = false;
				}
				return lavenderblush;
			}
		}

		public static Pen LawnGreen {
			get {
				if (lawngreen == null) {
					lawngreen = new Pen(Color.LawnGreen);
					lawngreen.canChange = false;
				}
				return lawngreen;
			}
		}

		public static Pen LemonChiffon {
			get {
				if (lemonchiffon == null) {
					lemonchiffon = new Pen(Color.LemonChiffon);
					lemonchiffon.canChange = false;
				}
				return lemonchiffon;
			}
		}

		public static Pen LightBlue {
			get {
				if (lightblue == null) {
					lightblue = new Pen(Color.LightBlue);
					lightblue.canChange = false;
				}
				return lightblue;
			}
		}

		public static Pen LightCoral {
			get {
				if (lightcoral == null) {
					lightcoral = new Pen(Color.LightCoral);
					lightcoral.canChange = false;
				}
				return lightcoral;
			}
		}

		public static Pen LightCyan {
			get {
				if (lightcyan == null) {
					lightcyan = new Pen(Color.LightCyan);
					lightcyan.canChange = false;
				}
				return lightcyan;
			}
		}

		public static Pen LightGoldenrodYellow {
			get {
				if (lightgoldenrodyellow == null) {
					lightgoldenrodyellow = new Pen(Color.LightGoldenrodYellow);
					lightgoldenrodyellow.canChange = false;
				}
				return lightgoldenrodyellow;
			}
		}

		public static Pen LightGray {
			get {
				if (lightgray == null) {
					lightgray = new Pen(Color.LightGray);
					lightgray.canChange = false;
				}
				return lightgray;
			}
		}

		public static Pen LightGreen {
			get {
				if (lightgreen == null) {
					lightgreen = new Pen(Color.LightGreen);
					lightgreen.canChange = false;
				}
				return lightgreen;
			}
		}

		public static Pen LightPink {
			get {
				if (lightpink == null) {
					lightpink = new Pen(Color.LightPink);
					lightpink.canChange = false;
				}
				return lightpink;
			}
		}

		public static Pen LightSalmon {
			get {
				if (lightsalmon == null) {
					lightsalmon = new Pen(Color.LightSalmon);
					lightsalmon.canChange = false;
				}
				return lightsalmon;
			}
		}

		public static Pen LightSeaGreen {
			get {
				if (lightseagreen == null) {
					lightseagreen = new Pen(Color.LightSeaGreen);
					lightseagreen.canChange = false;
				}
				return lightseagreen;
			}
		}

		public static Pen LightSkyBlue {
			get {
				if (lightskyblue == null) {
					lightskyblue = new Pen(Color.LightSkyBlue);
					lightskyblue.canChange = false;
				}
				return lightskyblue;
			}
		}

		public static Pen LightSlateGray {
			get {
				if (lightslategray == null) {
					lightslategray = new Pen(Color.LightSlateGray);
					lightslategray.canChange = false;
				}
				return lightslategray;
			}
		}

		public static Pen LightSteelBlue {
			get {
				if (lightsteelblue == null) {
					lightsteelblue = new Pen(Color.LightSteelBlue);
					lightsteelblue.canChange = false;
				}
				return lightsteelblue;
			}
		}

		public static Pen LightYellow {
			get {
				if (lightyellow == null) {
					lightyellow = new Pen(Color.LightYellow);
					lightyellow.canChange = false;
				}
				return lightyellow;
			}
		}

		public static Pen Lime {
			get {
				if (lime == null) {
					lime = new Pen(Color.Lime);
					lime.canChange = false;
				}
				return lime;
			}
		}

		public static Pen LimeGreen {
			get {
				if (limegreen == null) {
					limegreen = new Pen(Color.LimeGreen);
					limegreen.canChange = false;
				}
				return limegreen;
			}
		}

		public static Pen Linen {
			get {
				if (linen == null) {
					linen = new Pen(Color.Linen);
					linen.canChange = false;
				}
				return linen;
			}
		}

		public static Pen Magenta {
			get {
				if (magenta == null) {
					magenta = new Pen(Color.Magenta);
					magenta.canChange = false;
				}
				return magenta;
			}
		}

		public static Pen Maroon {
			get {
				if (maroon == null) {
					maroon = new Pen(Color.Maroon);
					maroon.canChange = false;
				}
				return maroon;
			}
		}

		public static Pen MediumAquamarine {
			get {
				if (mediumaquamarine == null) {
					mediumaquamarine = new Pen(Color.MediumAquamarine);
					mediumaquamarine.canChange = false;
				}
				return mediumaquamarine;
			}
		}

		public static Pen MediumBlue {
			get {
				if (mediumblue == null) {
					mediumblue = new Pen(Color.MediumBlue);
					mediumblue.canChange = false;
				}
				return mediumblue;
			}
		}

		public static Pen MediumOrchid {
			get {
				if (mediumorchid == null) {
					mediumorchid = new Pen(Color.MediumOrchid);
					mediumorchid.canChange = false;
				}
				return mediumorchid;
			}
		}

		public static Pen MediumPurple {
			get {
				if (mediumpurple == null) {
					mediumpurple = new Pen(Color.MediumPurple);
					mediumpurple.canChange = false;
				}
				return mediumpurple;
			}
		}

		public static Pen MediumSeaGreen {
			get {
				if (mediumseagreen == null) {
					mediumseagreen = new Pen(Color.MediumSeaGreen);
					mediumseagreen.canChange = false;
				}
				return mediumseagreen;
			}
		}

		public static Pen MediumSlateBlue {
			get {
				if (mediumslateblue == null) {
					mediumslateblue = new Pen(Color.MediumSlateBlue);
					mediumslateblue.canChange = false;
				}
				return mediumslateblue;
			}
		}

		public static Pen MediumSpringGreen {
			get {
				if (mediumspringgreen == null) {
					mediumspringgreen = new Pen(Color.MediumSpringGreen);
					mediumspringgreen.canChange = false;
				}
				return mediumspringgreen;
			}
		}

		public static Pen MediumTurquoise {
			get {
				if (mediumturquoise == null) {
					mediumturquoise = new Pen(Color.MediumTurquoise);
					mediumturquoise.canChange = false;
				}
				return mediumturquoise;
			}
		}

		public static Pen MediumVioletRed {
			get {
				if (mediumvioletred == null) {
					mediumvioletred = new Pen(Color.MediumVioletRed);
					mediumvioletred.canChange = false;
				}
				return mediumvioletred;
			}
		}

		public static Pen MidnightBlue {
			get {
				if (midnightblue == null) {
					midnightblue = new Pen(Color.MidnightBlue);
					midnightblue.canChange = false;
				}
				return midnightblue;
			}
		}

		public static Pen MintCream {
			get {
				if (mintcream == null) {
					mintcream = new Pen(Color.MintCream);
					mintcream.canChange = false;
				}
				return mintcream;
			}
		}

		public static Pen MistyRose {
			get {
				if (mistyrose == null) {
					mistyrose = new Pen(Color.MistyRose);
					mistyrose.canChange = false;
				}
				return mistyrose;
			}
		}

		public static Pen Moccasin {
			get {
				if (moccasin == null) {
					moccasin = new Pen(Color.Moccasin);
					moccasin.canChange = false;
				}
				return moccasin;
			}
		}

		public static Pen NavajoWhite {
			get {
				if (navajowhite == null) {
					navajowhite = new Pen(Color.NavajoWhite);
					navajowhite.canChange = false;
				}
				return navajowhite;
			}
		}

		public static Pen Navy {
			get {
				if (navy == null) {
					navy = new Pen(Color.Navy);
					navy.canChange = false;
				}
				return navy;
			}
		}

		public static Pen OldLace {
			get {
				if (oldlace == null) {
					oldlace = new Pen(Color.OldLace);
					oldlace.canChange = false;
				}
				return oldlace;
			}
		}

		public static Pen Olive {
			get {
				if (olive == null) {
					olive = new Pen(Color.Olive);
					olive.canChange = false;
				}
				return olive;
			}
		}

		public static Pen OliveDrab {
			get {
				if (olivedrab == null) {
					olivedrab = new Pen(Color.OliveDrab);
					olivedrab.canChange = false;
				}
				return olivedrab;
			}
		}

		public static Pen Orange {
			get {
				if (orange == null) {
					orange = new Pen(Color.Orange);
					orange.canChange = false;
				}
				return orange;
			}
		}

		public static Pen OrangeRed {
			get {
				if (orangered == null) {
					orangered = new Pen(Color.OrangeRed);
					orangered.canChange = false;
				}
				return orangered;
			}
		}

		public static Pen Orchid {
			get {
				if (orchid == null) {
					orchid = new Pen(Color.Orchid);
					orchid.canChange = false;
				}
				return orchid;
			}
		}

		public static Pen PaleGoldenrod {
			get {
				if (palegoldenrod == null) {
					palegoldenrod = new Pen(Color.PaleGoldenrod);
					palegoldenrod.canChange = false;
				}
				return palegoldenrod;
			}
		}

		public static Pen PaleGreen {
			get {
				if (palegreen == null) {
					palegreen = new Pen(Color.PaleGreen);
					palegreen.canChange = false;
				}
				return palegreen;
			}
		}

		public static Pen PaleTurquoise {
			get {
				if (paleturquoise == null) {
					paleturquoise = new Pen(Color.PaleTurquoise);
					paleturquoise.canChange = false;
				}
				return paleturquoise;
			}
		}

		public static Pen PaleVioletRed {
			get {
				if (palevioletred == null) {
					palevioletred = new Pen(Color.PaleVioletRed);
					palevioletred.canChange = false;
				}
				return palevioletred;
			}
		}

		public static Pen PapayaWhip {
			get {
				if (papayawhip == null) {
					papayawhip = new Pen(Color.PapayaWhip);
					papayawhip.canChange = false;
				}
				return papayawhip;
			}
		}

		public static Pen PeachPuff {
			get {
				if (peachpuff == null) {
					peachpuff = new Pen(Color.PeachPuff);
					peachpuff.canChange = false;
				}
				return peachpuff;
			}
		}

		public static Pen Peru {
			get {
				if (peru == null) {
					peru = new Pen(Color.Peru);
					peru.canChange = false;
				}
				return peru;
			}
		}

		public static Pen Pink {
			get {
				if (pink == null) {
					pink = new Pen(Color.Pink);
					pink.canChange = false;
				}
				return pink;
			}
		}

		public static Pen Plum {
			get {
				if (plum == null) {
					plum = new Pen(Color.Plum);
					plum.canChange = false;
				}
				return plum;
			}
		}

		public static Pen PowderBlue {
			get {
				if (powderblue == null) {
					powderblue = new Pen(Color.PowderBlue);
					powderblue.canChange = false;
				}
				return powderblue;
			}
		}

		public static Pen Purple {
			get {
				if (purple == null) {
					purple = new Pen(Color.Purple);
					purple.canChange = false;
				}
				return purple;
			}
		}

		public static Pen Red {
			get {
				if (red == null) {
					red = new Pen(Color.Red);
					red.canChange = false;
				}
				return red;
			}
		}

		public static Pen RosyBrown {
			get {
				if (rosybrown == null) {
					rosybrown = new Pen(Color.RosyBrown);
					rosybrown.canChange = false;
				}
				return rosybrown;
			}
		}

		public static Pen RoyalBlue {
			get {
				if (royalblue == null) {
					royalblue = new Pen(Color.RoyalBlue);
					royalblue.canChange = false;
				}
				return royalblue;
			}
		}

		public static Pen SaddleBrown {
			get {
				if (saddlebrown == null) {
					saddlebrown = new Pen(Color.SaddleBrown);
					saddlebrown.canChange = false;
				}
				return saddlebrown;
			}
		}

		public static Pen Salmon {
			get {
				if (salmon == null) {
					salmon = new Pen(Color.Salmon);
					salmon.canChange = false;
				}
				return salmon;
			}
		}

		public static Pen SandyBrown {
			get {
				if (sandybrown == null) {
					sandybrown = new Pen(Color.SandyBrown);
					sandybrown.canChange = false;
				}
				return sandybrown;
			}
		}

		public static Pen SeaGreen {
			get {
				if (seagreen == null) {
					seagreen = new Pen(Color.SeaGreen);
					seagreen.canChange = false;
				}
				return seagreen;
			}
		}

		public static Pen SeaShell {
			get {
				if (seashell == null) {
					seashell = new Pen(Color.SeaShell);
					seashell.canChange = false;
				}
				return seashell;
			}
		}

		public static Pen Sienna {
			get {
				if (sienna == null) {
					sienna = new Pen(Color.Sienna);
					sienna.canChange = false;
				}
				return sienna;
			}
		}

		public static Pen Silver {
			get {
				if (silver == null) {
					silver = new Pen(Color.Silver);
					silver.canChange = false;
				}
				return silver;
			}
		}

		public static Pen SkyBlue {
			get {
				if (skyblue == null) {
					skyblue = new Pen(Color.SkyBlue);
					skyblue.canChange = false;
				}
				return skyblue;
			}
		}

		public static Pen SlateBlue {
			get {
				if (slateblue == null) {
					slateblue = new Pen(Color.SlateBlue);
					slateblue.canChange = false;
				}
				return slateblue;
			}
		}

		public static Pen SlateGray {
			get {
				if (slategray == null) {
					slategray = new Pen(Color.SlateGray);
					slategray.canChange = false;
				}
				return slategray;
			}
		}

		public static Pen Snow {
			get {
				if (snow == null) {
					snow = new Pen(Color.Snow);
					snow.canChange = false;
				}
				return snow;
			}
		}

		public static Pen SpringGreen {
			get {
				if (springgreen == null) {
					springgreen = new Pen(Color.SpringGreen);
					springgreen.canChange = false;
				}
				return springgreen;
			}
		}

		public static Pen SteelBlue {
			get {
				if (steelblue == null) {
					steelblue = new Pen(Color.SteelBlue);
					steelblue.canChange = false;
				}
				return steelblue;
			}
		}

		public static Pen Tan {
			get {
				if (tan == null) {
					tan = new Pen(Color.Tan);
					tan.canChange = false;
				}
				return tan;
			}
		}

		public static Pen Teal {
			get {
				if (teal == null) {
					teal = new Pen(Color.Teal);
					teal.canChange = false;
				}
				return teal;
			}
		}

		public static Pen Thistle {
			get {
				if (thistle == null) {
					thistle = new Pen(Color.Thistle);
					thistle.canChange = false;
				}
				return thistle;
			}
		}

		public static Pen Tomato {
			get {
				if (tomato == null) {
					tomato = new Pen(Color.Tomato);
					tomato.canChange = false;
				}
				return tomato;
			}
		}

		public static Pen Transparent {
			get {
				if (transparent == null) {
					transparent = new Pen(Color.Transparent);
					transparent.canChange = false;
				}
				return transparent;
			}
		}

		public static Pen Turquoise {
			get {
				if (turquoise == null) {
					turquoise = new Pen(Color.Turquoise);
					turquoise.canChange = false;
				}
				return turquoise;
			}
		}

		public static Pen Violet {
			get {
				if (violet == null) {
					violet = new Pen(Color.Violet);
					violet.canChange = false;
				}
				return violet;
			}
		}

		public static Pen Wheat {
			get {
				if (wheat == null) {
					wheat = new Pen(Color.Wheat);
					wheat.canChange = false;
				}
				return wheat;
			}
		}

		public static Pen White {
			get {
				if (white == null) {
					white = new Pen(Color.White);
					white.canChange = false;
				}
				return white;
			}
		}

		public static Pen WhiteSmoke {
			get {
				if (whitesmoke == null) {
					whitesmoke = new Pen(Color.WhiteSmoke);
					whitesmoke.canChange = false;
				}
				return whitesmoke;
			}
		}

		public static Pen Yellow {
			get {
				if (yellow == null) {
					yellow = new Pen(Color.Yellow);
					yellow.canChange = false;
				}
				return yellow;
			}
		}

		public static Pen YellowGreen {
			get {
				if (yellowgreen == null) {
					yellowgreen = new Pen(Color.YellowGreen);
					yellowgreen.canChange = false;
				}
				return yellowgreen;

			}
		}
	
	}
}
