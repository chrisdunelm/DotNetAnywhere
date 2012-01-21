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

#ifndef __STRINGFORMAT_H
#define __STRINGFORMAT_H

#include "libIGraph.h"

#define StringFormatFlags_DirectionRightToLeft 0x1
#define StringFormatFlags_DirectionVertical 0x2
#define StringFormatFlags_FitBlackBox 0x4
#define StringFormatFlags_DisplayFormatControl 0x20
#define StringFormatFlags_NoFontFallback 0x400
#define StringFormatFlags_MeasureTrailingSpaces 0x800
#define StringFormatFlags_NoWrap 0x1000
#define StringFormatFlags_LineLimit 0x2000
#define StringFormatFlags_NoClip 0x4000

#define StringAlignment_Near 0
#define StringAlignment_Center 1
#define StringAlignment_Far 2

typedef struct tStringFormat_ tStringFormat;
struct tStringFormat_ {
	U32 alignment;
	U32 formatFlags;
	U32 lineAlignment;
	U32 stringTrimming;
};

tStringFormat* CreateStringFormat_(U32 alignment, U32 formatFlags, U32 lineAlignment, U32 stringTrimming);
void DisposeStringFormat_(tStringFormat *pStringFormat);
void StringFormat_SetTrimming_(tStringFormat *pStringFormat, U32 stringTrimming);
void StringFormat_SetAlignment_(tStringFormat *pStringFormat, U32 alignment);
void StringFormat_SetLineAlignment_(tStringFormat *pStringFormat, U32 lineAlignment);
void StringFormat_SetFormatFlags_(tStringFormat *pStringFormat, U32 formatFlags);

#endif