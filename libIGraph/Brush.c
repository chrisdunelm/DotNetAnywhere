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

#include "Brush.h"

#include "HatchBrushDefs.h"

#include <math.h>

#define WrapMode_Tile 0
#define WrapMode_TileFlipX 1
#define WrapMode_TileFlipY 2
#define WrapMode_TileFlipXY 3
#define WrapMode_Clamp 4

tBrush* CreateBrush_Solid_(U32 argb) {
	tSolidBrush *pBrush = TMALLOC(tSolidBrush);

	pBrush->root.brushType = BRUSHTYPE_SOLID;
	pBrush->col = argb;

	return (tBrush*)pBrush;
}

tBrush* CreateBrush_Hatch_(U32 hatchStyle, U32 foreCol, U32 backCol) {
	tHatchBrush *pBrush = TMALLOC(tHatchBrush);

	pBrush->root.brushType = BRUSHTYPE_HATCH;
	pBrush->pHatchDef = hatchDefs[hatchStyle];
	pBrush->foreCol = foreCol;
	pBrush->backCol = backCol;

	return (tBrush*)pBrush;
}

// See http://www.osix.net/modules/article/?id=770
// Approx. solution - average error ~0.005%
I32 CalcLen(I32 x, I32 y) {
	I32 a, b;
	x = abs(x);
	y = abs(y);
	if (x > y) {
		a = x; b = y;
	} else {
		b = x; a = y;
	}
	// 106 = (sqrt(2) - 1) << 8
	a = (a << 8) + (106 * b * b) / a;
	a = ((a >> 8) + ((x*x + y*y) / (a >> 8))) >> 1;
	return a;
}

#define LG_BRUSH_ANGLE_PRECISION_BITS 10

tBrush* CreateBrush_LinearGradient_(I32 x1, I32 y1, I32 x2, I32 y2, U32 col1, U32 col2) {
	I32 x, y;
	I32 hyp;
	tLinearGradientBrush *pBrush = TMALLOC(tLinearGradientBrush);

	pBrush->root.brushType = BRUSHTYPE_LINEARGRADIENT;
	pBrush->wrapMode = WrapMode_Tile;
	pBrush->x1 = x1;
	pBrush->y1 = y1;
	pBrush->x2 = x2;
	pBrush->y2 = y2;
	pBrush->col1 = col1;
	pBrush->col2 = col2;

	x = x2 - x1;
	y = y2 - y1;
	hyp = CalcLen(x, y);
	// cos angle
	pBrush->rotVec00 = (x << LG_BRUSH_ANGLE_PRECISION_BITS) / hyp;
	// sin angle
	pBrush->rotVec10 = (y << LG_BRUSH_ANGLE_PRECISION_BITS) / hyp;
	// grad length
	pBrush->gradLength = hyp;

	return (tBrush*)pBrush;
}

void DisposeBrush_(tBrush *pBrush) {
	free(pBrush);
}

static U32 Brush_GetPixelCol_Solid(tBrush *pBrush, tGraphics *pGraphics, I32 x, I32 y) {
	return ((tSolidBrush*)pBrush)->col;
}


static U32 Brush_GetPixelCol_Hatch(tBrush *pBrush, tGraphics *pGraphics, I32 x, I32 y) {
	U8 bits = ((tHatchBrush*)pBrush)->pHatchDef[y & 0x7];
	return (bits & (1 << (x & 0x7)))?((tHatchBrush*)pBrush)->foreCol:((tHatchBrush*)pBrush)->backCol;
}

#define LG(pBrush) ((tLinearGradientBrush*)pBrush)
static U32 Brush_GetPixelCol_LinearGradient(tBrush *pBrush, tGraphics *pGraphics, I32 x, I32 y) {
	I32 xr;
	I32 gradLength;
	U32 col1, col2;
	U32 a, r, g, b;

	gradLength = LG(pBrush)->gradLength;
	// Rotate just the x parameter around point 1
	xr = LG(pBrush)->rotVec00 * (x - LG(pBrush)->x1) + LG(pBrush)->rotVec10 * (y - LG(pBrush)->y1);
	xr >>= LG_BRUSH_ANGLE_PRECISION_BITS;
	xr %= gradLength;
	if (xr < 0) {
		xr += gradLength;
	}

	col1 = LG(pBrush)->col1;
	col2 = LG(pBrush)->col2;

	r = ((I32)R(col1)) + (((((I32)R(col2)) - ((I32)R(col1))) * xr) / gradLength);
	g = ((I32)G(col1)) + (((((I32)G(col2)) - ((I32)G(col1))) * xr) / gradLength);
	b = ((I32)B(col1)) + (((((I32)B(col2)) - ((I32)B(col1))) * xr) / gradLength);
	a = ((I32)A(col1)) + (((((I32)A(col2)) - ((I32)A(col1))) * xr) / gradLength);

	return ARGB(a,r,g,b);
}
#undef LG

tBrush_GetPixelCol mBrush_GetPixelCol_[BRUSHTYPE_NUM] = {
	Brush_GetPixelCol_Solid,
	Brush_GetPixelCol_Hatch,
	Brush_GetPixelCol_LinearGradient
};

