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
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace System.Drawing {
	public sealed class Graphics : MarshalByRefObject, IDisposable {

		public static Graphics FromHdc(IntPtr hdc) {
			if (hdc != IntPtr.Zero) {
				throw new NotImplementedException();
			}
			int xSize, ySize;
			PixelFormat pixelFormat;
			IntPtr native = LibIGraph.GetScreen(out xSize, out ySize, out pixelFormat);
			if (native == IntPtr.Zero) {
				throw new Exception("CreateScreen() failed");
			}
			return new Graphics(xSize, ySize, pixelFormat, native);
		}

		public static Graphics FromImage(Image image) {
			if (image == null) {
				throw new ArgumentNullException("image");
			}
			IntPtr native = LibIGraph.GetGraphicsFromImage(image.native);
			if (native == IntPtr.Zero) {
				throw new ArgumentException("Failed to get Graphics from given Image", "image");
			}
			return new Graphics(image.Width, image.Height, image.PixelFormat, native);
		}

		private IntPtr native = IntPtr.Zero;
		private int xSizePixels, ySizePixels;
		private PixelFormat pixelFormat;
		private TextRenderingHint textRenderingHint;
		private Region clip;

		private Graphics(int xSize, int ySize, PixelFormat pixelFormat, IntPtr native) {
			this.xSizePixels = xSize;
			this.ySizePixels = ySize;
			this.pixelFormat = pixelFormat;
			this.native = native;
			this.textRenderingHint = TextRenderingHint.SystemDefault;
			this.Clip = new Region();
		}

		~Graphics() {
			this.Dispose();
		}

		public void Dispose() {
			if (this.native != IntPtr.Zero) {
				LibIGraph.DisposeGraphics(this.native);
				this.native = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}

		public Region Clip {
			get {
				return this.clip;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException();
				}
				this.clip = value;
				LibIGraph.Graphics_SetClip(this.native, value.native);
			}
		}

		public void ResetClip() {
			this.Clip = new Region();
		}

		public void Clear(Color color) {
			LibIGraph.Clear(this.native, color.ToArgb());
		}

		public TextRenderingHint TextRenderingHint {
			get {
				return this.textRenderingHint;
			}
			set {
				LibIGraph.TextRenderingHint_Set(this.native, value);
				this.textRenderingHint = value;
			}
		}

		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2) {
			LibIGraph.DrawLine_Ints(this.native, pen.native, x1, y1, x2, y2);
		}

		public void DrawLine(Pen pen, Point pt1, Point pt2) {
			LibIGraph.DrawLine_Ints(this.native, pen.native, pt1.X, pt1.Y, pt2.X, pt2.Y);
		}

		public void DrawLines(Pen pen, Point[] points) {
			int num = points.Length - 1;
			for (int i = 0; i < num; i++) {
				this.DrawLine(pen, points[i], points[i + 1]);
			}
		}

		public void DrawPolygon(Pen pen, Point[] points) {
			this.DrawLines(pen, points);
			this.DrawLine(pen, points[points.Length - 1], points[0]);
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height) {
			DrawLines(pen, new Point[] {
				new Point(x, y),
				new Point(x + width, y),
				new Point(x + width, y + height),
				new Point(x, y + height),
				new Point(x, y)
			});
		}

		public void DrawRectangle(Pen pen, Rectangle rect) {
			this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawRectangle(Pen pen, float x, float y, float width, float height) {
			this.DrawRectangle(pen, (int)x, (int)y, (int)width, (int)height);
		}

		public void FillRectangle(Brush brush, int x, int y, int width, int height) {
			LibIGraph.FillRectangle_Ints(this.native, brush.native, x, y, width, height);
		}

		public void FillRectangle(Brush brush, Rectangle rect) {
			this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height) {
			this.FillRectangle(brush, (int)x, (int)y, (int)width, (int)height);
		}

		public void FillRectangle(Brush brush, RectangleF rect) {
			this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawEllipse(Pen pen, int x, int y, int width, int height) {
			LibIGraph.DrawEllipse_Ints(this.native, pen.native, x, y, width, height);
		}

		public void DrawEllipse(Pen pen, Rectangle rect) {
			this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void FillEllipse(Brush brush, int x, int y, int width, int height) {
			LibIGraph.FillEllipse_Ints(this.native, brush.native, x, y, width, height);
		}

		public void FillEllipse(Brush brush, Rectangle rect) {
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format) {
			LibIGraph.DrawString(this.native, s, font.native, brush.native, (int)x, (int)y, int.MaxValue, int.MaxValue, (format == null) ? IntPtr.Zero : format.native);
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y) {
			this.DrawString(s, font, brush, x, y, StringFormat.GenericDefault);
		}

		public void DrawString(string s, Font font, Brush brush, PointF point) {
			this.DrawString(s, font, brush, point.X, point.Y, StringFormat.GenericDefault);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF rect) {
			LibIGraph.DrawString(this.native, s, font.native, brush.native,
				(int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom,
				StringFormat.GenericDefault.native);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF rect, StringFormat format) {
			LibIGraph.DrawString(this.native, s, font.native, brush.native,
				(int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom, format.native);
		}

		public SizeF MeasureString(string s, Font font) {
			return this.MeasureString(s, font, int.MaxValue, StringFormat.GenericDefault);
		}

		public SizeF MeasureString(string s, Font font, int width) {
			return this.MeasureString(s, font, width, StringFormat.GenericDefault);
		}

		public SizeF MeasureString(string s, Font font, int width, StringFormat format) {
			int szWidth, szHeight;
			LibIGraph.MeasureString(this.native, s, font.native, width, format.native, out szWidth, out szHeight);
			return new SizeF(szWidth, szHeight);
		}

		public void DrawImage(Image image, int x, int y) {
			LibIGraph.DrawImageUnscaled(this.native, image.native, x, y);
		}

		public void DrawImageUnscaled(Image image, int x, int y) {
			LibIGraph.DrawImageUnscaled(this.native, image.native, x, y);
		}

		public void CopyFromScreen(int srcX, int srcY, int destX, int destY, Size size) {
			LibIGraph.Graphics_CopyFromScreen(this.native, srcX, srcY, destX, destY, size.Width, size.Height);
		}

		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size size) {
			this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, size);
		}
	}
}
