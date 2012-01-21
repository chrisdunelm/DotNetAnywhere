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
	public sealed class Font : MarshalByRefObject, IDisposable {

		internal IntPtr native = IntPtr.Zero;

		private FontFamily family;

		public Font(FontFamily family, float emSize)
			: this(family, emSize, FontStyle.Regular) { }

		public Font(FontFamily family, float emSize, FontStyle style) {
			this.family = family;
			this.native = LibIGraph._CreateFont(family.native, emSize, style);
		}

		public Font(string familyName, float emSize) {
			this.family = new FontFamily(familyName);
			this.native = LibIGraph._CreateFont(this.family.native, emSize, FontStyle.Regular);
		}

		~Font() {
			this.Dispose();
		}

		public void Dispose() {
			if (this.native != IntPtr.Zero) {
				LibIGraph.DisposeFont(this.native);
				this.native = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}
	}
}
