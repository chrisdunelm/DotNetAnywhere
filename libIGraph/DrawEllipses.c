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

#include "DrawEllipses.h"
#include "Pixels.h"

typedef void (*tEllipsePoint)(I32 x, I32 y, I32 cx, I32 cy, tGraphics *pGraphics, void *pData);

// See http://www.cs.drexel.edu/~david/Classes/CS430/Lectures/L-06_Circles.6.pdf for algorithm

static void CalcPoints(I32 width, I32 height, tEllipsePoint cb, I32 cx, I32 cy, tGraphics *pGraphics, void *pData, U32 isFill) {
	I32 rx = width >> 1;
	I32 ry = height >> 1;
	I32 rx2 = rx * rx;
	I32 ry2 = ry * ry;
	I32 x, y, d;

	d = ry2 - rx2 * ry + rx2;
	x = 0;
	y = ry;

	for (; ry2 * x < rx2 * y; x++) {
		if (d < 0) {
			d += ry2 * (2 * x + 3);
			if (!isFill) {
				cb(x, y, cx, cy, pGraphics, pData);
			}
		} else {
			d += ry2 * (2 * x + 3) - rx2 * (2 * y + 2);
			cb(x, y, cx, cy, pGraphics, pData);
			y--;
		}
	}

	d = ry2 * (x * x + x) + rx2 * (y - 1) * (y - 1) - rx2 * ry2;
	for (; y >= 0; y--) {
		cb(x, y, cx, cy, pGraphics, pData);
		if (d < 0) {
			d += ry2 * (2 * x + 2) - rx2 * (2 * y + 3);
			x++;
		} else {
			d -= rx2 * (2 * y + 3);
		}
	}
}

static void DrawEllipse_All_CB(I32 x, I32 y, I32 cx, I32 cy, tGraphics *pGraphics, void *pData) {
	mSetPixel(pGraphics, cx - x, cy - y, ((tPen*)pData)->col);
	if (x != 0) {
		mSetPixel(pGraphics, cx + x, cy - y, ((tPen*)pData)->col);
	}
	if (y != 0) {
		mSetPixel(pGraphics, cx - x, cy + y, ((tPen*)pData)->col);
		if (x != 0) {
			mSetPixel(pGraphics, cx + x, cy + y, ((tPen*)pData)->col);
		}
	}
}

static void DrawEllipse_All(tGraphics *pGraphics, tPen *pPen, I32 x, I32 y, I32 width, I32 height) {
	CalcPoints(width, height, DrawEllipse_All_CB, x + (width>>1), y + (height>>1), pGraphics, pPen, 0);
}

tDrawEllipse mDrawEllipse_[FMT_NUM] = {
	DrawEllipse_All,
	DrawEllipse_All
};

static void FillEllipse_All_CB(I32 x, I32 y, I32 cx, I32 cy, tGraphics *pGraphics, void *pData) {
	I32 i;
	I32 y1 = cy - y;
	I32 y2 = cy + y;
	for (i=cx - x; i<= cx + x; i++) {
		U32 col;
		col = mBrush_GetPixelCol((tBrush*)pData, pGraphics, i, y1);
		mSetPixel(pGraphics, i, y1, col);
		if (y != 0) {
			col = mBrush_GetPixelCol((tBrush*)pData, pGraphics, i, y2);
			mSetPixel(pGraphics, i, y2, col);
		}
	}
}

static void FillEllipse_All(tGraphics *pGraphics, tBrush *pBrush, I32 x, I32 y, I32 width, I32 height) {
	CalcPoints(width, height, FillEllipse_All_CB, x + (width>>1), y + (height>>1), pGraphics, pBrush, 1);
}

tFillEllipse mFillEllipse_[FMT_NUM] = {
	FillEllipse_All,
	FillEllipse_All
};
