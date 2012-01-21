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
	public struct Size {

		public static readonly Size Empty = new Size();

		private int width, height;

		public Size(int width, int height) {
			this.width = width;
			this.height = height;
		}

		public Size(Point pt) {
			this.width = pt.X;
			this.height = pt.Y;
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

		public bool IsEmpty {
			get {
				return this.width == 0 && this.height == 0;
			}
		}

		public static Size Add(Size a, Size b) {
			return new Size(a.Width + b.Width, a.Height + b.Height);
		}

		public static Size Subtract(Size a, Size b) {
			return new Size(a.Width - b.Width, a.Height - b.Height);
		}

		public static Size operator +(Size a, Size b) {
			return new Size(a.Width + b.Width, a.Height + b.Height);
		}

		public static Size operator -(Size a, Size b) {
			return new Size(a.Width - b.Width, a.Height - b.Height);
		}

		public static bool operator ==(Size a, Size b) {
			return ((a.Width == b.Width) && (a.Height == b.Height));
		}

		public static bool operator !=(Size a, Size b) {
			return ((a.Width != b.Width) || (a.Height != b.Height));
		}

		public static explicit operator Point(Size sz) {
			return new Point(sz.Width, sz.Height);
		}

		public override bool Equals(object o) {
			if (!(o is Size)) {
				return false;
			}
			return this == (Size)o;
		}

		public override int GetHashCode() {
			return width ^ height;
		}

		public override string ToString() {
			return String.Format("{{Width={0}, Height={1}}}", width, height);
		}

	}
}
