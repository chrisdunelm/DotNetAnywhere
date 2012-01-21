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
	public struct PointF {

		private float x, y;

		public static readonly PointF Empty;

		public PointF(float x, float y) {
			this.x = x;
			this.y = y;
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

		public bool IsEmpty {
			get {
				return this.x == 0.0f && this.y == 0.0f;
			}
		}

		public static bool operator ==(PointF a, PointF b) {
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=(PointF a, PointF b) {
			return a.x != b.x || a.y != b.y;
		}

		public override bool Equals(object obj) {
			return obj is PointF && (PointF)obj == this;
		}

		public override int GetHashCode() {
			return (int)this.x ^ (int)this.y;
		}

		public override string ToString() {
			return String.Format("{{X={0}, Y={1}}}", this.x, this.y);
		}
	}
}
