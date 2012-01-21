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

#include "StringFormat.h"

tStringFormat* CreateStringFormat_(U32 alignment, U32 formatFlags, U32 lineAlignment, U32 stringTrimming) {
	tStringFormat *pFormat = TMALLOC(tStringFormat);

	pFormat->alignment = alignment;
	pFormat->formatFlags = formatFlags;
	pFormat->lineAlignment = lineAlignment;
	pFormat->stringTrimming = stringTrimming;

	return pFormat;
}

void DisposeStringFormat_(tStringFormat *pStringFormat) {
	free(pStringFormat);
}

void StringFormat_SetTrimming_(tStringFormat *pStringFormat, U32 stringTrimming) {
	pStringFormat->stringTrimming = stringTrimming;
}

void StringFormat_SetAlignment_(tStringFormat *pStringFormat, U32 alignment) {
	pStringFormat->alignment = alignment;
}

void StringFormat_SetLineAlignment_(tStringFormat *pStringFormat, U32 lineAlignment) {
	pStringFormat->lineAlignment = lineAlignment;
}

void StringFormat_SetFormatFlags_(tStringFormat *pStringFormat, U32 formatFlags) {
	pStringFormat->formatFlags = formatFlags;
}
