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

#ifndef __TEXT_H
#define __TEXT_H

#include "libIGraph.h"

#include "Graphics.h"
#include "Brush.h"
#include "Font.h"
#include "StringFormat.h"

#define FontStyle_Regular 0
#define FontStyle_Bold 1
#define FontStyle_Italic 2
#define FontStyle_Underline 4
#define FontStyle_Strikeout 5

void Text_EnsureFontMetrics(tFont *pFont);
void Text_RemoveFont(tFont *pFont);

void MeasureString_(tGraphics *pGraphics, STRING2 s, tFont *pFont, I32 width, tStringFormat *pFormat, U32 *pSzWidth, U32 *pSzHeight);
void DrawString_(tGraphics *pGraphics, STRING2 s, tFont *pFont, tBrush *pBrush, I32 x1, I32 y1, I32 x2, I32 y2, tStringFormat *pFormat);

#endif
