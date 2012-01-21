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

namespace System.Drawing.Drawing2D {
	public sealed class HatchBrush : Brush {

		public HatchBrush(HatchStyle hatchStyle, Color foreColor, Color backColor) {
			this.hatchStyle = hatchStyle;
			this.foreCol = foreColor;
			this.backCol = backColor;
			base.native = LibIGraph.CreateBrush_Hatch(hatchStyle, foreCol.ToArgb(), backCol.ToArgb());
		}

		public HatchBrush(HatchStyle hatchStyle, Color foreColor) : this(hatchStyle, foreColor, Color.Black) { }

		private HatchStyle hatchStyle;
		private Color foreCol, backCol;

		public HatchStyle HatchStyle {
			get {
				return this.hatchStyle;
			}
		}

		public Color ForegroundColor {
			get {
				return this.foreCol;
			}
		}

		public Color BackgroundColor {
			get {
				return this.backCol;
			}
		}
	}
}
