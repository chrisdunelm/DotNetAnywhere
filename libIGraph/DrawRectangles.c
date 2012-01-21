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

#include "DrawRectangles.h"
#include "Pixels.h"

static void FillRectangle_All(tGraphics *pGraphics, tBrush *pBrush, I32 x, I32 y, I32 width, I32 height) {
	I32 x2, y2;
	I32 xIt, yIt;

	x2 = x + width;
	y2 = y + height;

	// Make sure that x,y is always top left, and x2,y2 is always bottom right
	if (x > x2) {
		SWAP_I32(x, x2);
	}
	if (y > y2) {
		SWAP_I32(y, y2);
	}

	for (yIt = y; yIt < y2; yIt++) {
		for (xIt = x; xIt < x2; xIt++) {
			U32 col = mBrush_GetPixelCol(pBrush, pGraphics, xIt, yIt);
			mSetPixel(pGraphics, xIt, yIt, col);
		}
	}
}

tFillRectangle mFillRectangle_[FMT_NUM] = {
	FillRectangle_All,
	FillRectangle_All
};
