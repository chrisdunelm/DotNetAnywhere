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
using System.Drawing.Imaging;

namespace System.Drawing {
	public sealed class Bitmap : Image {

		public Bitmap(int width, int height)
			: this(width, height, PixelFormat.Format32bppArgb) { }

		public Bitmap(int width, int height, PixelFormat pixelFormat) {
			base.native = LibIGraph._CreateBitmap(width, height, pixelFormat);
			if (base.native == IntPtr.Zero) {
				throw new ArgumentException();
			}
			base.width = width;
			base.height = height;
			base.pixelFormat = pixelFormat;
		}

		internal Bitmap(IntPtr native, int width, int height, PixelFormat pixelFormat) {
			if (native == IntPtr.Zero) {
				throw new ArgumentException("Cannot create Bitmap");
			}
			base.native = native;
			base.width = width;
			base.height = height;
			base.pixelFormat = pixelFormat;
		}

	}
}
