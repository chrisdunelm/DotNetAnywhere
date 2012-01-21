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

#include "Graphics.h"
#include "GetScreen.h"
#include "Pixels.h"
#include "Image.h"

tGraphics* InitGraphics() {
	tGraphics *pGraphics = TMALLOC(tGraphics);

	memset(pGraphics, 0, sizeof(tGraphics));

	return pGraphics;
}

void DisposeGraphics_(tGraphics *pGraphics) {
	if (pGraphics->screenPtr != NULL) {
		// This is the screen window
		ScreenDispose();
	} else {
		// This is an bitmap of some kind
	}
}

void TextRenderingHint_Set_(tGraphics *pGraphics, U32 textRenderingHint) {
	pGraphics->textRenderingHint = textRenderingHint;
}

static void Clear_WithClipping(tGraphics *pGraphics, U32 col) {
	tRegion *pClip = pGraphics->pClip;
	U32 x0 = (pClip->x1 > 0) ? pClip->x1 : 0;
	U32 x1 = (pClip->x2 < pGraphics->xSize) ? pClip->x2 : pGraphics->xSize;
	U32 y0 = (pClip->y1 > 0) ? pClip->y1 : 0;
	U32 y1 = (pClip->y2 < pGraphics->ySize) ? pClip->y2 : pGraphics->ySize;
	U32 x, y;
	for (y = y0; y<y1; y++) {
		for (x = x0; x<x1; x++) {
			mSetPixelNoBlend(pGraphics, x, y, col);
		}
	}
}

static void Clear_FMT_4BPP_GRAY(tGraphics *pGraphics, U32 col) {
	if (!pGraphics->pClip->isInfinite) {
		Clear_WithClipping(pGraphics, col);
	} else {
		U8 col4 = COL2GRAY(col) >> 4;
#ifdef INVERT_4BPP_GRAY
		col4 = 15 - col4;
#endif
		memset(pGraphics->pScan0, col4 | (col4 << 4), pGraphics->memSize);
	}
}

static void Clear_FMT_32BPP_ARGB(tGraphics *pGraphics, U32 col) {
	if (!pGraphics->pClip->isInfinite) {
		Clear_WithClipping(pGraphics, col);
	} else {
		void *pEnd = ((U8*)pGraphics->pScan0) + pGraphics->stride * pGraphics->ySize;
		U32 *pMem = (U32*)pGraphics->pScan0;
		while ((void*)pMem < pEnd) {
			*pMem++ = col;
		}
	}
}

tClear mClear_[FMT_NUM] = {
	Clear_FMT_4BPP_GRAY,
	Clear_FMT_32BPP_ARGB
};

void DrawImageUnscaled_(tGraphics *pGraphics, tImage *pImage, I32 x, I32 y) {
	I32 xPos, yPos;
	U32 col;

	// TODO: Optimize!!!
	for (yPos = 0; yPos < pImage->height; yPos++) {
		for (xPos = 0; xPos < pImage->width; xPos++) {
			col = mImage_GetPixel(pImage, xPos, yPos);
			mSetPixel(pGraphics, x + xPos, y + yPos, col);
		}
	}
}

void Graphics_SetClip_(tGraphics *pGraphics, tRegion *pRegion) {
	pGraphics->pClip = pRegion;
}

void Graphics_CopyFromScreen_(tGraphics *pGraphics, I32 srcX, I32 srcY, I32 destX, I32 destY, I32 sizeX, I32 sizeY) {
	I32 x, y, dx, dy;
	I32 xEnd = srcX + sizeX;
	I32 yEnd = srcY + sizeY;

	// TODO: Optimize!!!
	for (y=srcY, dy=destY; y<yEnd; y++, dy++) {
		for (x=srcX, dx=destX; x<xEnd; x++, dx++) {
			U32 col = GetScreenPixel(x, y);
			mSetPixel(pGraphics, dx, dy, col);
		}
	}
}
