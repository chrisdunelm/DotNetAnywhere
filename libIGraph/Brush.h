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

#ifndef __BRUSH_H
#define __BRUSH_H

#include "libIGraph.h"

#include "Graphics.h"

#define BRUSHTYPE_NUM 3

#define BRUSHTYPE_SOLID 0
#define BRUSHTYPE_HATCH 1
#define BRUSHTYPE_LINEARGRADIENT 2

typedef struct tBrush_ tBrush;
struct tBrush_ {
	U32 brushType;
};

typedef struct tSolidBrush_ tSolidBrush;
struct tSolidBrush_ {
	tBrush root;
	U32 col;
};

typedef struct tHatchBrush_ tHatchBrush;
struct tHatchBrush_ {
	tBrush root;
	U32 foreCol, backCol;
	U8 *pHatchDef;
};

typedef struct tLinearGradientBrush_ tLinearGradientBrush;
struct tLinearGradientBrush_ {
	tBrush root;
	U32 wrapMode;
	I32 x1, y1;
	I32 x2, y2;
	U32 col1, col2;
	// Pre-calculated rotation matrix to rotate all incoming points by
	I32 rotVec00, rotVec10;
	// Pre-calulated gradient length of one traverse from point1 to point2
	I32 gradLength;
};

tBrush* CreateBrush_Solid_(U32 argb);
tBrush* CreateBrush_Hatch_(U32 hatchStyle, U32 foreCol, U32 backCol);
tBrush* CreateBrush_LinearGradient_(I32 x1, I32 y1, I32 x2, I32 y2, U32 col1, U32 col2);
void DisposeBrush_(tBrush *pBrush);

typedef U32 (*tBrush_GetPixelCol)(tBrush *pBrush, tGraphics *pGraphics, I32 x, I32 y);
extern tBrush_GetPixelCol mBrush_GetPixelCol_[BRUSHTYPE_NUM];
#define mBrush_GetPixelCol(pBrush, pGraphics, x, y) mBrush_GetPixelCol_[(pBrush)->brushType](pBrush, pGraphics, x, y)

#endif
