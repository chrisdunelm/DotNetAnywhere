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
	public sealed class SolidBrush : Brush {

		private Color col;
		internal bool canChange = true;

		public SolidBrush(Color col) {
			this.col = col;
			base.native = LibIGraph.CreateBrush_Solid(col.ToArgb());
		}

		public Color Color {
			get {
				return this.col;
			}
			set {
				if (canChange) {
					this.col = value;
					LibIGraph.SolidBrush_SetCol(base.native, value.ToArgb());
				} else {
					throw new ArgumentException("This SolidBrush cannot be changed.");
				}
			}
		}

		protected override void Dispose(bool disposing) {
			if (disposing && !this.canChange) {
				// If this is a system brush, then refuse to dispose of it.
				throw new ArgumentException("This SolidBrush cannot be changed.");
			}
			base.Dispose(disposing);
		}

	}
}
