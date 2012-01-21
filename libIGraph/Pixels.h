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

#ifndef __PIXELS_H
#define __PIXELS_H

#include "libIGraph.h"
#include "Graphics.h"

typedef void (*tSetPixel)(tGraphics *pGraphics, I32 x, I32 y, U32 col);
extern tSetPixel mSetPixel_[FMT_NUM];
#define mSetPixel(pGraphics, x, y, col) mSetPixel_[pGraphics->pixelFormatIndex](pGraphics, x, y, col)
extern tSetPixel mSetPixelNoBlend_[FMT_NUM];
#define mSetPixelNoBlend(pGraphics, x, y, col) mSetPixelNoBlend_[pGraphics->pixelFormatIndex](pGraphics, x, y, col)

#endif
