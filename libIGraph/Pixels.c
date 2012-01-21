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

#include "Pixels.h"

static void SetPixelNoBlend_FMT_4BPP_GRAY(tGraphics *pGraphics, I32 x, I32 y, U32 col) {
	if (x<0 || y<0 || x>=pGraphics->xSize || y>=pGraphics->ySize) {
		return;
	}
	if (!pGraphics->pClip->isInfinite){
		tRegion *pClip = pGraphics->pClip;
		if (x<pClip->x1 || y<pClip->y1 || x>=pClip->x2 || y>=pClip->y2) {
			return;
		}
	}
	{
		U8 *pMem = ((U8*)pGraphics->pScan0) + (y * pGraphics->stride) + (x >> 1);
		U32 gray = COL2GRAY(col);
#ifdef INVERT_4BPP_GRAY
		gray = 255 - gray;
#endif
		if (x&1) {
			// 'Odd' pixels
			*pMem = (*pMem & 0xf0) | (gray >> 4);
		} else {
			// 'Even' pixels
			*pMem = (*pMem & 0x0f) | (gray & 0xf0);
		}
	}
}

static void SetPixelNoBlend_FMT_32BPP_ARGB(tGraphics *pGraphics, I32 x, I32 y, U32 col) {
	if (x<0 || y<0 || x>=pGraphics->xSize || y>=pGraphics->ySize) {
		return;
	}
	if (!pGraphics->pClip->isInfinite){
		tRegion *pClip = pGraphics->pClip;
		if (x<pClip->x1 || y<pClip->y1 || x>=pClip->x2 || y>=pClip->y2) {
			return;
		}
	}
	{
		U32 *pMem;
		pMem = (U32*)(((U8*)pGraphics->pScan0) + y * pGraphics->stride + (x << 2));
		*pMem = col;
	}
}

tSetPixel mSetPixelNoBlend_[FMT_NUM] = {
	SetPixelNoBlend_FMT_4BPP_GRAY,
	SetPixelNoBlend_FMT_32BPP_ARGB
};

static void SetPixel_FMT_4BPP_GRAY(tGraphics *pGraphics, I32 x, I32 y, U32 col) {
	if (x<0 || y<0 || x>=pGraphics->xSize || y>=pGraphics->ySize) {
		return;
	}
	if (!pGraphics->pClip->isInfinite){
		tRegion *pClip = pGraphics->pClip;
		if (x<pClip->x1 || y<pClip->y1 || x>=pClip->x2 || y>=pClip->y2) {
			return;
		}
	}
	{
		U8 *pMem = ((U8*)pGraphics->pScan0) + (y * pGraphics->stride) + (x >> 1);
		U32 opacity = A(col);
		U32 gray = COL2GRAY(col);
#ifdef INVERT_4BPP_GRAY
		gray = 255 - gray;
#endif
		if (x&1) {
			// 'Odd' pixels
			U32 curPix = *pMem;
			U32 newPix = curPix & 0x0f;
			newPix |= newPix << 4; // Bring into range 0 - 255
			newPix = ((newPix * (255 - opacity)) + (gray * opacity)) >> 8;
			*pMem = (curPix & 0xf0) | (newPix >> 4);
		} else {
			// 'Even' pixels
			U32 curPix = *pMem;
			U32 newPix = curPix & 0xf0;
			newPix |= newPix >> 4; // Bring into range 0 - 255
			newPix = ((newPix * (255 - opacity)) + (gray * opacity)) >> 8;
			*pMem = (curPix & 0x0f) | (newPix & 0xf0);
		}
	}
}

static void SetPixel_FMT_32BPP_ARGB(tGraphics *pGraphics, I32 x, I32 y, U32 col) {
	if (x<0 || y<0 || x>=pGraphics->xSize || y>=pGraphics->ySize) {
		return;
	}
	if (!pGraphics->pClip->isInfinite){
		tRegion *pClip = pGraphics->pClip;
		if (x<pClip->x1 || y<pClip->y1 || x>=pClip->x2 || y>=pClip->y2) {
			return;
		}
	}
	{
		U32 *pMem;
		U32 cur;
		U32 rCur, gCur, bCur, aCur;
		U32 rCol, gCol, bCol, aCol;

		pMem = (U32*)(((U8*)pGraphics->pScan0) + y * pGraphics->stride + (x << 2));
		cur = *pMem;

		aCur = A(cur);
		rCur = R(cur);
		gCur = G(cur);
		bCur = B(cur);

		aCol = A(col);
		rCol = R(col);
		gCol = G(col);
		bCol = B(col);

		rCur = (rCol * aCol + rCur * (255 - aCol)) / 255;
		gCur = (gCol * aCol + gCur * (255 - aCol)) / 255;
		bCur = (bCol * aCol + bCur * (255 - aCol)) / 255;
		aCur = (aCol * aCol + aCur * (255 - aCol)) / 255;

		cur = ARGB(aCur,rCur,bCur,gCur);

		*pMem = cur;
	}
}

tSetPixel mSetPixel_[FMT_NUM] = {
	SetPixel_FMT_4BPP_GRAY,
	SetPixel_FMT_32BPP_ARGB
};
