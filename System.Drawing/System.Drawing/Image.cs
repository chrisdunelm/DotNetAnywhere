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
	public abstract class Image : MarshalByRefObject, IDisposable {

		public static Image FromFile(string filename) {
			if (string.IsNullOrEmpty(filename)) {
				throw new ArgumentNullException("filename");
			}
			int width, height;
			PixelFormat pixelFormat;
			IntPtr native = LibIGraph.BitmapFromFile(filename, out width, out height, out pixelFormat);
			return new Bitmap(native, width, height, pixelFormat);
		}

		internal IntPtr native = IntPtr.Zero;

		protected int width, height;
		protected PixelFormat pixelFormat;

		~Image() {
			this.Dispose();
		}

		public void Dispose() {
			if (this.native != IntPtr.Zero) {
				LibIGraph.DisposeImage(this.native);
				GC.SuppressFinalize(this);
				this.native = IntPtr.Zero;
			}
		}

		public int Width {
			get {
				return this.width;
			}
		}

		public int Height {
			get {
				return this.height;
			}
		}

		public PixelFormat PixelFormat {
			get {
				return this.pixelFormat;
			}
		}

		public Size Size {
			get {
				return new Size(this.width, this.height);
			}
		}

	}
}
