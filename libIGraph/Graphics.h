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

#ifndef __GRAPHICS_H
#define __GRAPHICS_H

typedef struct tGraphics_ tGraphics;

#include "libIGraph.h"

#include "Image.h"
#include "Region.h"

#define TextRenderingHint_SystemDefault 0
#define TextRenderingHint_SingleBitPerPixelGridFit 1
#define TextRenderingHint_SingleBitPerPixel 2
#define TextRenderingHint_AntiAliasGridFit 3
#define TextRenderingHint_AntiAlias 4 
#define TextRenderingHint_ClearTypeGridFit 5

#define PixelFormat_Alpha 262144
#define PixelFormat_Canonical 2097152
#define PixelFormat_DontCare 0
#define PixelFormat_Extended 1048576
#define PixelFormat_Format16bppArgb1555 397319
#define PixelFormat_Format16bppGrayScale 1052676
#define PixelFormat_Format16bppRgb555 135173
#define PixelFormat_Format16bppRgb565 135174
#define PixelFormat_Format1bppIndexed 196865
#define PixelFormat_Format24bppRgb 137224
#define PixelFormat_Format32bppArgb 2498570
#define PixelFormat_Format32bppPArgb 925707
#define PixelFormat_Format32bppRgb 139273
#define PixelFormat_Format48bppRgb 1060876
#define PixelFormat_Format4bppIndexed 197634
#define PixelFormat_Format64bppArgb 3424269
#define PixelFormat_Format64bppPArgb 1851406
#define PixelFormat_Format8bppIndexed 198659
#define PixelFormat_Gdi 131072
#define PixelFormat_Indexed 65536
#define PixelFormat_Max 15
#define PixelFormat_PAlpha 524288
#define PixelFormat_Undefined 0

struct tGraphics_ {
	I32 xSize;
	I32 ySize;
	U32 pixelFormat;
	U32 pixelFormatIndex;
	void *pScan0;
	I32 stride;
	void* screenPtr; // Only set if this is a Graphics for the screen
	tImage *pImage; // Only set if this is a Graphics for an Image
	U32 memSize;
	U32 textRenderingHint;
	tRegion *pClip; // Null if no clipping region set
};

tGraphics* InitGraphics();
void DisposeGraphics_(tGraphics *pGraphics);

void TextRenderingHint_Set_(tGraphics *pGraphics, U32 textRenderingHint);

typedef void (*tClear)(tGraphics *pGraphics, U32 col);
extern tClear mClear_[FMT_NUM];
#define mClear(pGraphics, col) mClear_[pGraphics->pixelFormatIndex](pGraphics, col)

void DrawImageUnscaled_(tGraphics *pGraphics, tImage *pImage, I32 x, I32 y);
void Graphics_SetClip_(tGraphics *pGraphics, tRegion *pRegion);
void Graphics_CopyFromScreen_(tGraphics *pGraphics, I32 srcX, I32 srcY, I32 destX, I32 destY, I32 sizeX, I32 sizeY);

#endif
