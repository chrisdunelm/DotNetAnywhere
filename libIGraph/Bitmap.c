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

#include "Bitmap.h"
#include "Graphics.h"

tBitmap* CreateBitmap_(I32 width, I32 height, U32 pixelFormat) {
	tBitmap *pBitmap;
	U32 bitsPerPixel;
	U32 memSize, stride;
	U32 ok;
	
	switch (pixelFormat) {
		case PixelFormat_Format32bppArgb:
			bitsPerPixel = 32;
			break;
		case PixelFormat_Gdi:
			bitsPerPixel = 4;
			break;
		default:
			return NULL;
	}

	// Ensure stride is always a multiple of 32 bits
	stride = (width * bitsPerPixel + 31) & 0xffffffe0;
	stride /= 8;
	memSize = stride * height;
	pBitmap = malloc(sizeof(tBitmap) + memSize);

	ok = InitImage(&pBitmap->image, IMAGE_TYPE_BITMAP, width, height, pixelFormat);
	if (!ok) {
		free(pBitmap);
		return NULL;
	}

	memset(pBitmap->pBmp, 0, memSize);
	pBitmap->memSize = memSize;
	pBitmap->stride = stride;

	return pBitmap;
}

tGraphics* GetGraphicsFromBitmap_(tBitmap *pBitmap) {
	tGraphics *pGraphics = InitGraphics();

	pGraphics->memSize = pBitmap->memSize;
	pGraphics->pImage = (tImage*)pBitmap;
	pGraphics->pixelFormat = pBitmap->image.pixelFormat;
	pGraphics->pixelFormatIndex = pBitmap->image.pixelFormatIndex;
	pGraphics->pScan0 = pBitmap->pBmp;
	pGraphics->screenPtr = NULL;
	pGraphics->stride = pBitmap->stride;
	pGraphics->textRenderingHint = TextRenderingHint_SystemDefault;
	pGraphics->xSize = pBitmap->image.width;
	pGraphics->ySize = pBitmap->image.height;

	return pGraphics;
}

