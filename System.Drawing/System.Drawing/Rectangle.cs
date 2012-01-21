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
	public struct Rectangle {

		public static readonly Rectangle Empty = new Rectangle();

		public static Rectangle FromLTRB(int left, int top, int right, int bottom) {
			return new Rectangle(left, top, right - left, bottom - top);
		}

		public static Rectangle Inflate(Rectangle rect, int x, int y) {
			Rectangle r = new Rectangle(rect.Location, rect.Size);
			r.Inflate(x, y);
			return r;
		}

		private int x, y, width, height;

		public Rectangle(int x, int y, int width, int height) {
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public Rectangle(Point loc, Size sz) {
			this.x = loc.X;
			this.y = loc.Y;
			this.width = sz.Width;
			this.height = sz.Height;
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

		public int Width {
			get {
				return this.width;
			}
			set {
				this.width = value;
			}
		}

		public int Height {
			get {
				return this.height;
			}
			set {
				this.height = value;
			}
		}

		public int Left {
			get {
				return this.x;
			}
		}

		public int Right {
			get {
				return this.x + this.width;
			}
		}

		public int Top {
			get {
				return this.y;
			}
		}

		public int Bottom {
			get {
				return this.y + this.height;
			}
		}

		public Point Location {
			get {
				return new Point(this.x, this.y);
			}
			set {
				this.x = value.X;
				this.y = value.Y;
			}
		}

		public Size Size {
			get {
				return new Size(this.width, this.height);
			}
			set {
				this.width = value.Width;
				this.height = value.Height;
			}
		}

		public void Inflate(int width, int height) {
			this.x -= width;
			this.y -= height;
			this.width += width * 2;
			this.height += height * 2;
		}


		public void Inflate(Size sz) {
			Inflate(sz.Width, sz.Height);
		}

		public void Offset(Point pt) {
			this.x += pt.X;
			this.y += pt.Y;
		}

		public void Offset(int dx, int dy) {
			this.x += dx;
			this.y += dy;
		}

		public bool Contains(int x, int y) {
			return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
		}

		public bool Contains(Point pt) {
			return Contains(pt.X, pt.Y);
		}

		public bool Contains(Rectangle rect) {
			return rect == Intersect(this, rect);
		}

		public bool IntersectsWith(Rectangle r) {
			return !((Left >= r.Right) || (Right <= r.Left) || (Top >= r.Bottom) || (Bottom <= r.Top));
		}

		private bool IntersectsWithInclusive(Rectangle r) {
			return !((Left > r.Right) || (Right < r.Left) || (Top > r.Bottom) || (Bottom < r.Top));
		}

		public void Intersect(Rectangle r) {
			this = Rectangle.Intersect(this, r);
		}

		public static Rectangle Intersect(Rectangle r1, Rectangle r2) {
			// MS.NET returns a non-empty rectangle if the two rectangles
			// touch each other
			if (!r1.IntersectsWithInclusive(r2)) {
				return Empty;
			}

			return Rectangle.FromLTRB(
				Math.Max(r1.x, r2.x),
				Math.Max(r1.y, r2.y),
				Math.Min(r1.Right, r2.Right),
				Math.Min(r1.Bottom, r2.Bottom));
		}

		public static bool operator ==(Rectangle r1, Rectangle r2) {
			return ((r1.Location == r2.Location) && (r1.Size == r2.Size));
		}

		public static bool operator !=(Rectangle r1, Rectangle r2) {
			return ((r1.Location != r2.Location) || (r1.Size != r2.Size));
		}

		public override bool Equals(object o) {
			if (!(o is Rectangle)) {
				return false;
			}
			return this == (Rectangle)o;
		}

		public override int GetHashCode() {
			return ((this.height + this.width) ^ this.x) + this.y;
		}

		public override string ToString() {
			return String.Format("{{X={0},Y={1},Width={2},Height={3}}}", this.x, this.y, this.width, this.height);
		}

	}
}
