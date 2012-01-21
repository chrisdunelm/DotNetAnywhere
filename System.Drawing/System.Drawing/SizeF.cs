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
	public struct SizeF {

		private float width, height;

		public static readonly SizeF Empty;

		public SizeF(PointF pt) {
			this.width = pt.X;
			this.height = pt.Y;
		}

		public SizeF(SizeF size) {
			this.width = size.width;
			this.height = size.height;
		}

		public SizeF(float width, float height) {
			this.width = width;
			this.height = height;
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

		public bool IsEmpty {
			get {
				return this.width == 0.0f && this.height == 0.0f;
			}
		}

		public static bool operator ==(SizeF a, SizeF b) {
			return a.width == b.width && a.height == b.height;
		}

		public static bool operator !=(SizeF a, SizeF b) {
			return a.width != b.width || a.height != b.height;
		}

		public override bool Equals(object obj) {
			return obj is SizeF && (SizeF)obj == this;
		}

		public override int GetHashCode() {
			return (int)this.width ^ (int)this.height;
		}

		public override string ToString() {
			return string.Format("{{Width={0}, Height={1}}}", width, height);
		}
	}
}
