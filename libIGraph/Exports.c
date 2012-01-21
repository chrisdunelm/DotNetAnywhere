// Copyright (c) 2012 DotNetAnywhere
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

#include "libIGraph.h"

#include "GetScreen.h"
#include "Graphics.h"
#include "DrawLines.h"
#include "DrawRectangles.h"
#include "Pen.h"
#include "Brush.h"
#include "FontFamily.h"
#include "Font.h"
#include "StringFormat.h"
#include "Text.h"
#include "Image.h"
#include "Bitmap.h"
#include "Region.h"
#include "DrawEllipses.h"

EXPORT(tGraphics*) GetScreen(I32 *pXSize, I32 *pYSize, U32 *pPixelFormat) {
	return GetScreen_(pXSize, pYSize, pPixelFormat);
}

EXPORT(void) DisposeGraphics(tGraphics *pGraphics) {
	DisposeGraphics_(pGraphics);
}

EXPORT(void) Clear(tGraphics *pGraphics, U32 col) {
	mClear(pGraphics, col);
}

EXPORT(void) DrawLine_Ints(tGraphics *pGraphics, tPen *pPen, I32 x1, I32 y1, I32 x2, I32 y2) {
	mDrawLine(pGraphics, pPen, x1, y1, x2, y2);
}

EXPORT(void) FillRectangle_Ints(tGraphics *pGraphics, tBrush *pBrush, I32 x, I32 y, I32 width, I32 height) {
	mFillRectangle(pGraphics, pBrush, x, y, width, height);
}

EXPORT(void) DrawString(tGraphics *pGraphics, STRING2 s, tFont *pFont, tBrush *pBrush, I32 x1, I32 y1, I32 x2, I32 y2, tStringFormat *pFormat) {
	DrawString_(pGraphics, s, pFont, pBrush, x1, y1, x2, y2, pFormat);
}

EXPORT(tPen*) CreatePen_Color(float width, U32 col) {
	return CreatePen_Color_(width, col);
}

EXPORT(void) DisposePen(tPen *pPen) {
	DisposePen_(pPen);
}

EXPORT(tBrush*) CreateBrush_Solid(U32 col) {
	return CreateBrush_Solid_(col);
}

EXPORT(void) DisposeBrush(tBrush *pBrush) {
	DisposeBrush_(pBrush);
}

EXPORT(tFontFamily*) CreateFontFamily_Name(STRING name) {
	return CreateFontFamily_Name_(name);
}

EXPORT(void) DisposeFontFamily(tFontFamily *pFontFamily) {
	DisposeFontFamily_(pFontFamily);
}

EXPORT(tFont*) _CreateFont(tFontFamily *pFontFamily, float emSize, U32 fontStyle) {
	return CreateFont_(pFontFamily, emSize, fontStyle);
}

EXPORT(void) DisposeFont(tFont *pFont) {
	DisposeFont_(pFont);
}

EXPORT(tStringFormat*) CreateStringFormat(U32 alignment, U32 formatFlags, U32 lineAlignment, U32 stringTrimming) {
	return CreateStringFormat_(alignment, formatFlags, lineAlignment, stringTrimming);
}

EXPORT(void) DisposeStringFormat(tStringFormat *pStringFormat) {
	DisposeStringFormat_(pStringFormat);
}

EXPORT(void) StringFormat_SetTrimming(tStringFormat *pStringFormat, U32 stringTrimming) {
	StringFormat_SetTrimming_(pStringFormat, stringTrimming);
}

EXPORT(void) TextRenderingHint_Set(tGraphics *pGraphics, U32 textRenderingHint) {
	TextRenderingHint_Set_(pGraphics, textRenderingHint);
}

EXPORT(tBitmap*) _CreateBitmap(I32 width, I32 height, U32 pixelFormat) {
	return CreateBitmap_(width, height, pixelFormat);
}

EXPORT(tGraphics*) GetGraphicsFromImage(tImage *pImage) {
	return GetGraphicsFromImage_(pImage);
}

EXPORT(void) DrawImageUnscaled(tGraphics *pGraphics, tImage *pImage, I32 x, I32 y) {
	DrawImageUnscaled_(pGraphics, pImage, x, y);
}

EXPORT(void) DisposeImage(tImage *pImage) {
	DisposeImage_(pImage);
}

EXPORT(tBitmap*) BitmapFromFile(STRING filename, I32 *pWidth, I32 *pHeight, U32 *pPixelFormat) {
	return BitmapFromFile_(filename, pWidth, pHeight, pPixelFormat);
}

EXPORT(void) StringFormat_SetAlignment(tStringFormat *pStringFormat, U32 alignment) {
	StringFormat_SetAlignment_(pStringFormat, alignment);
}

EXPORT(void) StringFormat_SetLineAlignment(tStringFormat *pStringFormat, U32 lineAlignment) {
	StringFormat_SetLineAlignment_(pStringFormat, lineAlignment);
}

EXPORT(void) StringFormat_SetFormatFlags(tStringFormat *pStringFormat, U32 formatFlags) {
	StringFormat_SetFormatFlags_(pStringFormat, formatFlags);
}

EXPORT(tBrush*) CreateBrush_Hatch(U32 hatchStyle, U32 foreCol, U32 backCol) {
	return CreateBrush_Hatch_(hatchStyle, foreCol, backCol);
}

EXPORT(U32) IsKeyDown_Internal(U32 key) {
	return IsKeyDown_Internal_(key);
}

EXPORT(U32) LatestKeyUp_Internal() {
	return LatestKeyUp_Internal_();
}

EXPORT(U32) LatestKeyDown_Internal() {
	return LatestKeyDown_Internal_();
}

EXPORT(tBrush*) CreateBrush_LinearGradient(I32 x1, I32 y1, I32 x2, I32 y2, U32 col1, U32 col2) {
	return CreateBrush_LinearGradient_(x1, y1, x2, y2, col1, col2);
}

EXPORT(tRegion*) CreateRegion_Rect(I32 x, I32 y, I32 width, I32 height) {
	return CreateRegion_Rect_(x, y, width, height);
}

EXPORT(void) DisposeRegion(tRegion *pRegion) {
	DisposeRegion_(pRegion);
}

EXPORT(void) Graphics_SetClip(tGraphics *pGraphics, tRegion *pRegion) {
	Graphics_SetClip_(pGraphics, pRegion);
}

EXPORT(tRegion*) CreateRegion_Infinite() {
	return CreateRegion_Infinite_();
}

EXPORT(void) MeasureString(tGraphics *pGraphics, STRING2 s, tFont *pFont, I32 width, tStringFormat *pFormat, U32 *pSzWidth, U32 *pSzHeight) {
	MeasureString_(pGraphics, s, pFont, width, pFormat, pSzWidth, pSzHeight);
}

EXPORT(void) DrawEllipse_Ints(tGraphics *pGraphics, tPen *pPen, I32 x, I32 y, I32 width, I32 height) {
	mDrawEllipse(pGraphics, pPen, x, y, width, height);
}

EXPORT(void) FillEllipse_Ints(tGraphics *pGraphics, tBrush *pBrush, I32 x, I32 y, I32 width, I32 height) {
	mFillEllipse(pGraphics, pBrush, x, y, width, height);
}

EXPORT(void) Graphics_CopyFromScreen(tGraphics *pGraphics, I32 srcX, I32 srcY, I32 destX, I32 destY, I32 sizeX, I32 sizeY) {
	Graphics_CopyFromScreen_(pGraphics, srcX, srcY, destX, destY, sizeX, sizeY);
}
