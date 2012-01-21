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
	public sealed class LinearGradientBrush : Brush {

		public LinearGradientBrush(Point point1, Point point2, Color col1, Color col2) {
			base.native = LibIGraph.CreateBrush_LinearGradient
				(point1.X, point1.Y, point2.X, point2.Y, col1.ToArgb(), col2.ToArgb());
		}

		public LinearGradientBrush(PointF point1, PointF point2, Color col1, Color col2) {
			base.native = LibIGraph.CreateBrush_LinearGradient
				((int)point1.X, (int)point1.Y, (int)point2.X, (int)point2.Y, col1.ToArgb(), col2.ToArgb());
		}

	}
}
