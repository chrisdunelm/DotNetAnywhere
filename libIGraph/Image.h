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

#ifndef __IMAGE_H
#define __IMAGE_H

typedef struct tImage_ tImage;

#include "libIGraph.h"

#include "Graphics.h"

#define IMAGE_TYPE_NUM 1
#define IMAGE_TYPE_BITMAP 0

struct tImage_ {
	U32 imageType;
	I32 width, height;
	U32 pixelFormat;
	U32 pixelFormatIndex;
	tGraphics *pGraphics;
	U32 graphicsRefCount;
};

U32 InitImage(tImage *pImage, U32 imageType, I32 width, I32 height, U32 pixelFormat);
void DisposeImage_(tImage *pImage);
tGraphics* GetGraphicsFromImage_(tImage *pImage);

typedef U32 (*tImage_GetPixel)(tImage *pImage, I32 x, I32 y);
extern tImage_GetPixel mImage_GetPixel_[IMAGE_TYPE_NUM];
#define mImage_GetPixel(pImage, x, y) mImage_GetPixel_[pImage->imageType](pImage, x, y)

#endif
