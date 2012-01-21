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

#include "Region.h"

tRegion* CreateRegion_Infinite_() {
	tRegion *pRegion = TMALLOC(tRegion);

	pRegion->x1 = 0;
	pRegion->y1 = 0;
	pRegion->x2 = 0;
	pRegion->y2 = 0;
	pRegion->isInfinite = 1;

	return pRegion;
}

tRegion* CreateRegion_Rect_(I32 x, I32 y, I32 width, I32 height) {
	tRegion *pRegion = TMALLOC(tRegion);

	pRegion->x1 = x;
	pRegion->y1 = y;
	pRegion->x2 = x + width;
	pRegion->y2 = y + height;
	pRegion->isInfinite = 0;

	return pRegion;
}

void DisposeRegion_(tRegion *pRegion) {
	free(pRegion);
}

