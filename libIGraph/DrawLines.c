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

#include "DrawLines.h"
#include "Pixels.h"

static void DrawLine_All(tGraphics *pGraphics, tPen *pPen, I32 x1, I32 y1, I32 x2, I32 y2) {
	I32 dx = x2-x1;
	I32 dy = y2-y1;
	I32 overflow, absDx = ABS(dx), absDy = ABS(dy);
	I32 x, y, inc;
	U32 col = pPen->col;

	if (pPen->width <= 1.0f) {
		// If pen size is 1 (or less) then use the fast algorithm for line drawing.
		// Note that this still supports colour transparency.
		if (absDx > absDy) {
			// Scan along x-axis
			if (dx < 0) {
				SWAP_I32(x1, x2);
				SWAP_I32(y1, y2);
			}
			y = y1;
			inc = (y2>y1)?1:-1;
			overflow = absDx >> 1;
			for (x=x1; x<=x2; x++) {
				mSetPixel(pGraphics, x, y, col);
				overflow += absDy;
				if (overflow >= absDx) {
					overflow -= absDx;
					y += inc;
				}
			}
		} else {
			// Scan along y-axis
			if (dy < 0) {
				SWAP_I32(x1, x2);
				SWAP_I32(y1, y2);
			}
			x = x1;
			inc = (x2>x1)?1:-1;
			overflow = absDy >> 1;
			for (y=y1; y<=y2; y++) {
				mSetPixel(pGraphics, x, y, col);
				overflow += absDx;
				if (overflow >= absDy) {
					overflow -= absDy;
					x += inc;
				}
			}
		}
	} else {
		// Use the 'fill-the-poly' algorithm for line drawing of thick lines.

	}
}

tDrawLine mDrawLine_[FMT_NUM] = {
	DrawLine_All,
	DrawLine_All
};

