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

#include "Image.h"
#include "Bitmap.h"

U32 InitImage(tImage *pImage, U32 imageType, I32 width, I32 height, U32 pixelFormat) {
	pImage->imageType = imageType;
	pImage->width = width;
	pImage->height = height;
	pImage->pixelFormat = pixelFormat;
	pImage->graphicsRefCount = 0;
	pImage->pGraphics = NULL;
	switch (pixelFormat) {
		case PixelFormat_Gdi:
			pImage->pixelFormatIndex = FMT_4BPP_GRAY;
			break;
		case PixelFormat_Format32bppArgb:
			pImage->pixelFormatIndex = FMT_32BPP_ARGB;
			break;
		default:
			return 0;
	}
	return 1;
}

void DisposeImage_(tImage *pImage) {
	free(pImage);
}

tGraphics* GetGraphicsFromImage_(tImage *pImage) {
	if (pImage->pGraphics == NULL) {
		// Create brand new graphics for this image
		switch (pImage->imageType) {
			case IMAGE_TYPE_BITMAP:
				pImage->pGraphics = GetGraphicsFromBitmap_((tBitmap*)pImage);
				break;
			default:
				return NULL;
		}
	}

	pImage->graphicsRefCount++;
	return pImage->pGraphics;
}

static U32 Image_GetPixel_Bitmap(tImage *pImage, I32 x, I32 y) {
	tBitmap *pBitmap = (tBitmap*)pImage;

	switch (pBitmap->image.pixelFormatIndex) {
		case FMT_4BPP_GRAY:
			// NOT IMPLEMENTED YET
			return 0xff000000;
		case FMT_32BPP_ARGB:
			return *(U32*)(((U8*)pBitmap->pBmp) + y * pBitmap->stride + (x << 2));
		default:
			return 0;
	}
}

tImage_GetPixel mImage_GetPixel_[IMAGE_TYPE_NUM] = {
	Image_GetPixel_Bitmap
};