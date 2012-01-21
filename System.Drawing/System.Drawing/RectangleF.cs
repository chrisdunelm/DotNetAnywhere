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
	public struct RectangleF {
	
		private float x, y, width, height;

		public static readonly RectangleF Empty;

		public RectangleF(PointF location, SizeF size) {
			this.x = location.X;
			this.y = location.Y;
			this.width = size.Width;
			this.height = size.Height;
		}

		public RectangleF(float x, float y, float width, float height) {
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public float X {
			get {
				return this.x;
			}
			set {
				this.x = value;
			}
		}

		public float Y {
			get {
				return this.y;
			}
			set {
				this.y = value;
			}
		}

		public float Width {
			get {
				return this.width;
			}
			set {
				this.width = value;
			}
		}

		public float Height {
			get {
				return this.height;
			}
			set {
				this.height = value;
			}
		}

		public float Left {
			get {
				return this.x;
			}
		}

		public float Right {
			get {
				return this.x + this.width;
			}
		}

		public float Top {
			get {
				return this.y;
			}
		}

		public float Bottom {
			get {
				return this.y + this.height;
			}
		}

		public static implicit operator RectangleF(Rectangle r) {
			return new RectangleF(r.X, r.Y, r.Width, r.Height);
		}
	}
}
