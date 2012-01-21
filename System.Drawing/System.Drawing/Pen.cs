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
	public sealed class Pen : MarshalByRefObject, IDisposable, ICloneable {

		internal IntPtr native = IntPtr.Zero;
		private Color color;
		private float width;
		internal bool canChange = true;

		~Pen() {
			this.Dispose(false);
		}

		public Pen(Color color) : this(color, 1.0f) { }

		public Pen(Color color, float width) {
			if (width < 1.0f) {
				width = 1.0f;
			}
			this.color = color;
			this.width = width;
			native = LibIGraph.CreatePen_Color(width, color.ToArgb());
		}

		public Color Color {
			get {
				return this.color;
			}
			set {
				if (!this.canChange) {
					throw new ArgumentException("This SolidBrush cannot be changed.");
				}
				this.color = value;
				LibIGraph.Pen_SetCol(this.native, value.ToArgb());
			}
		}

		public float Width {
			get {
				return this.width;
			}
			set {
				if (!this.canChange) {
					throw new ArgumentException("This Pen cannot be changed.");
				}
				if (value < 1.0f) {
					value = 1.0f;
				}
				LibIGraph.Pen_SetWidth(this.native, value);
			}
		}

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (!this.canChange && disposing) {
				throw new ArgumentException("This Pen cannot be disposed of.");
			}
			if (this.native != IntPtr.Zero) {
				LibIGraph.DisposePen(this.native);
				this.native = IntPtr.Zero;
			}
		}

		public object Clone() {
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
