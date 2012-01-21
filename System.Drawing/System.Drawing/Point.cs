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
	public struct Point {

		public static readonly Point Empty = new Point();

		private int x, y;

		public Point(int dw) {
			this.x = dw >> 16;
			this.y = dw & 0xffff;
		}

		public Point(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public Point(Size sz) {
			this.x = sz.Width;
			this.y = sz.Height;
		}

		public int X {
			get {
				return this.x;
			}
			set {
				this.x = value;
			}
		}

		public int Y {
			get {
				return this.y;
			}
			set {
				this.y = value;
			}
		}

		public bool IsEmpty {
			get {
				return this.x == 0 && this.y == 0;
			}
		}

		public void Offset(int dx, int dy) {
			this.x += dx;
			this.y += dy;
		}

		public void Offset(Point pt) {
			this.x += pt.x;
			this.y += pt.y;
		}

		public static Point Add(Point pt, Size sz) {
			return new Point(pt.x + sz.Width, pt.y + sz.Height);
		}

		public static Point Subtract(Point pt, Size sz) {
			return new Point(pt.x - sz.Width, pt.y - sz.Height);
		}

		public static Point operator +(Point pt, Size sz) {
			return new Point(pt.x + sz.Width, pt.y + sz.Height);
		}

		public static Point operator -(Point pt, Size sz) {
			return new Point(pt.x - sz.Width, pt.y - sz.Height);
		}

		public static bool operator ==(Point a, Point b) {
			return ((a.x == b.x) && (a.y == b.y));
		}

		public static bool operator !=(Point a, Point b) {
			return ((a.x != b.x) || (a.y != b.y));
		}

		public static explicit operator Size(Point pt) {
			return new Size(pt.x, pt.x);
		}

		public override bool Equals(object o) {
			if (!(o is Point)) {
				return false;
			}
			return this == (Point)o;
		}

		public override int GetHashCode() {
			return x ^ y;
		}

		public override string ToString() {
			return string.Format("{{X={0},Y={1}}}", x, y);
		}

	}
}
