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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace System.Drawing {
	internal static class LibIGraph {

		#region Graphics methods

		[DllImport("libIGraph")]
		extern public static IntPtr GetScreen(out int xSize, out int ySize, out PixelFormat pixelFormat);

		[DllImport("libIGraph")]
		extern public static IntPtr GetGraphicsFromImage(IntPtr imageNative);

		[DllImport("libIGraph")]
		extern public static void DisposeGraphics(IntPtr graphics);

		[DllImport("libIGraph")]
		extern public static void TextRenderingHint_Set(IntPtr graphics, TextRenderingHint textRenderingHint);

		[DllImport("libIGraph")]
		extern public static void Clear(IntPtr native, int argb);

		[DllImport("libIGraph")]
		extern public static void DrawLine_Ints(IntPtr graphics, IntPtr pen, int x1, int y1, int x2, int y2);

		[DllImport("libIGraph")]
		extern public static void FillRectangle_Ints(IntPtr graphics, IntPtr brush, int x, int y, int width, int height);

		[DllImport("libIGraph")]
		extern public static void DrawEllipse_Ints(IntPtr graphics, IntPtr pen, int x, int y, int width, int height);

		[DllImport("libIGraph")]
		extern public static void FillEllipse_Ints(IntPtr graphics, IntPtr brush, int x, int y, int width, int height);

		[DllImport("libIGraph", CharSet = CharSet.Unicode)]
		extern public static void DrawString(IntPtr graphics, string s, IntPtr font, IntPtr brush, int x1, int y1, int x2, int y2, IntPtr format);

		[DllImport("libIGraph", CharSet = CharSet.Unicode)]
		extern public static void MeasureString(IntPtr graphics, string s, IntPtr font, int width, IntPtr format, out int szWidth, out int szHeight);

		[DllImport("libIGraph")]
		extern public static void DrawImageUnscaled(IntPtr graphics, IntPtr image, int x, int y);

		[DllImport("libIGraph")]
		extern public static void Graphics_SetClip(IntPtr graphics, IntPtr region);

		[DllImport("libIGraph")]
		extern public static void Graphics_CopyFromScreen(IntPtr graphics, int srcX, int srcY, int destX, int destY, int sizeX, int sizeY);

		#endregion

		#region Pen methods

		[DllImport("libIGraph")]
		extern public static IntPtr CreatePen_Color(float width, int argb);

		[DllImport("libIGraph")]
		extern public static void DisposePen(IntPtr native);

		[DllImport("libIGraph")]
		extern public static void Pen_SetCol(IntPtr native, int argb);

		[DllImport("libIGraph")]
		extern public static void Pen_SetWidth(IntPtr native, float width);

		#endregion

		#region Brush methods

		[DllImport("libIGraph")]
		extern public static IntPtr CreateBrush_Solid(int argb);

		[DllImport("libIGraph")]
		extern public static IntPtr CreateBrush_Hatch(HatchStyle hatchStyle, int foreCol, int backCol);

		[DllImport("libIGraph")]
		extern public static IntPtr CreateBrush_LinearGradient(int x1, int y1, int x2, int y2, int col1, int col2);

		[DllImport("libIGraph")]
		extern public static void DisposeBrush(IntPtr native);

		[DllImport("libIGraph")]
		extern public static void SolidBrush_SetCol(IntPtr native, int argb);

		#endregion

		#region FontFamily methods

		[DllImport("libIGraph", CharSet=CharSet.Ansi)]
		extern public static IntPtr CreateFontFamily_Name(string name);

		[DllImport("libIGraph")]
		extern public static void DisposeFontFamily(IntPtr native);

		#endregion

		#region Font methods

		[DllImport("libIGraph")]
		extern public static IntPtr _CreateFont(IntPtr family, float emSize, FontStyle style);

		[DllImport("libIGraph")]
		extern public static void DisposeFont(IntPtr native);

		#endregion

		#region StringFormat methods

		[DllImport("libIGraph")]
		extern public static IntPtr CreateStringFormat(StringAlignment alignment, StringFormatFlags formatFlags, StringAlignment lineAlignment, StringTrimming stringTrimming);

		[DllImport("libIGraph")]
		extern public static void DisposeStringFormat(IntPtr native);

		[DllImport("libIGraph")]
		extern public static void StringFormat_SetTrimming(IntPtr stringFormat, StringTrimming trimming);

		[DllImport("libIGraph")]
		extern public static void StringFormat_SetAlignment(IntPtr stringFormat, StringAlignment alignment);

		[DllImport("libIGraph")]
		extern public static void StringFormat_SetLineAlignment(IntPtr stringFormat, StringAlignment lineAlignment);

		[DllImport("libIGraph")]
		extern public static void StringFormat_SetFormatFlags(IntPtr stringFormat, StringFormatFlags formatFlags);

		#endregion

		#region Image methods

		[DllImport("libIGraph")]
		extern public static void DisposeImage(IntPtr native);

		#endregion

		#region Bitmap methods

		[DllImport("libIGraph")]
		extern public static IntPtr _CreateBitmap(int width, int height, PixelFormat pixelFormat);

		[DllImport("libIGraph", CharSet = CharSet.Ansi)]
		extern public static IntPtr BitmapFromFile(string filename, out int width, out int height, out PixelFormat pixelFormat);

		#endregion

		#region Region methods

		[DllImport("libIGraph")]
		extern public static IntPtr CreateRegion_Infinite();

		[DllImport("libIGraph")]
		extern public static IntPtr CreateRegion_Rect(int x, int y, int width, int height);

		[DllImport("libIGraph")]
		extern public static void DisposeRegion(IntPtr native);

		#endregion

	}
}
