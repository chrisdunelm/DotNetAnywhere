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

// Only needed to make VS2005 editor work properly!
#include "Config.h"

#include "Text.h"
#include "Pixels.h"
#include "Font.h"
#include "StringFormat.h"
#include "Brush.h"

#ifdef INCLUDE_FREETYPE

#include <ft2build.h>
#include FT_FREETYPE_H
#include FT_CACHE_H
#include FT_SFNT_NAMES_H

#define FT_CHECKERROR(msg) \
	if (ftErr != 0) { \
		printf(msg " Error: %d\n", (int)ftErr); \
		exit(2); \
	}

static FT_Library ftLib = NULL;
static FTC_Manager ftCache = NULL;
static FTC_CMapCache ftCMapCache = NULL;
static FTC_ImageCache ftImageCache = NULL;

static void ShowFTBitmap_All(tGraphics *pGraphics, tBrush *pBrush, I32 x, I32 y, FT_BitmapGlyph pGlyph) {
	FT_Bitmap *pBmp = &pGlyph->bitmap;
	I32 i, j;
	I32 xOrg = x + pGlyph->left;
	I32 xPix, yPix = y - pGlyph->top;
	unsigned char *pMem = pBmp->buffer;
	unsigned char *pMemCol0 = pMem;
	U32 bmpStride = pBmp->pitch;

	if (pBmp->pixel_mode == FT_PIXEL_MODE_MONO) {
		for (j=0; j<pBmp->rows; j++) {
			U8 curByte;
			xPix = xOrg;
			for (i=0; i<pBmp->width; i++) {
				U32 col;
				U32 bitPos = i & 0x7;
				if (bitPos == 0) {
					curByte = *pMem++;
				}
				col = (curByte & (0x80 >> bitPos))?mBrush_GetPixelCol(pBrush, pGraphics, xPix, yPix):0;
				mSetPixel(pGraphics, xPix, yPix, col);
				xPix++;
			}
			yPix++;
			pMem = pMemCol0 + bmpStride;
			pMemCol0 = pMem;
		}
	} else if (pBmp->pixel_mode == FT_PIXEL_MODE_GRAY) {
		for (j = 0; j<pBmp->rows; j++) {
			xPix = xOrg;
			for (i = 0; i<pBmp->width; i++) {
				// Calculate the transparency value needed to display this pixel
				U32 col;
				U32 trans = *pMem++;
				col = mBrush_GetPixelCol(pBrush, pGraphics, xPix, yPix);
				trans *= col >> 24;
				trans /= 255;
				col = (col & 0x00ffffff) | (trans << 24);
				mSetPixel(pGraphics, xPix, yPix, col);
				xPix++;
			}
			yPix++;
			pMem = pMemCol0 + bmpStride;
			pMemCol0 = pMem;
		}
	}
}

typedef void (*tShowFTBitmap)(tGraphics *pGraphics, tBrush *pBrush, I32 x, I32 y, FT_BitmapGlyph pGlyph);
tShowFTBitmap mShowFTBitmap_[FMT_NUM] = {
	ShowFTBitmap_All,
	ShowFTBitmap_All
};
#define mShowFTBitmap(pGraphics, pBrush, x, y, pGlyph) mShowFTBitmap_[pGraphics->pixelFormatIndex](pGraphics, pBrush, x, y, pGlyph)

static FT_Error FaceReq(FTC_FaceID faceID, FT_Library ftLib, FT_Pointer state, FT_Face *pFace) {
	char str[256];
	tFont *pFont = (tFont*)faceID;

	sprintf(str, "%s%s.ttf", FONT_DIR, pFont->pFontFamily->familyName);

	return FT_New_Face(ftLib, str, 0, pFace);
}

// Make sure that the freetype library is ready for use
static void EnsureFT() {
	FT_Error ftErr;

	if (ftLib != NULL) {
		// Don't init anything if it's already been done.
		return;
	}

	// Init the font library
	ftErr = FT_Init_FreeType(&ftLib);
	FT_CHECKERROR("FT_Init_FreeType()");

	// Init the cache system
	ftErr = FTC_Manager_New(ftLib, 0, 0, 0, FaceReq, NULL, &ftCache);
	FT_CHECKERROR("FTC_Manager_New()");
	ftErr = FTC_CMapCache_New(ftCache, &ftCMapCache);
	FT_CHECKERROR("FTC_CMapCache_New()");
	ftErr = FTC_ImageCache_New(ftCache, &ftImageCache);
	FT_CHECKERROR("FTC_ImageCache_New()");
}

void Text_EnsureFontMetrics(tFont *pFont) {
	FT_Error ftErr;
	char str[256];
	FT_Face face;

	EnsureFT();

	sprintf(str, "%s%s.ttf", FONT_DIR, pFont->pFontFamily->familyName);
	ftErr = FT_New_Face(ftLib, str, 0, &face);
	FT_CHECKERROR("FT_New_Face()");
	pFont->ascender = ABS(face->ascender);
	pFont->descender = ABS(face->descender);
	pFont->unitsPerEm = face->units_per_EM;
	pFont->height = face->height;
	pFont->maxAdvanceWidth = face->max_advance_width;
	pFont->maxAdvanceHeight = face->max_advance_height;
	pFont->underlineThickness = face->underline_thickness;
	pFont->underlinePosition = face->underline_position;
	// Get the index of the unicode char-map.
	// This works because the unicode char-map is always selected by default when the font is loaded
	pFont->charMapIndex = FT_Get_Charmap_Index(face->charmap);
	FT_Done_Face(face);

	pFont->pixelSizeX = PT_TO_PIXEL_X(pFont->emSize);
	pFont->pixelSizeY = PT_TO_PIXEL_Y(pFont->emSize);
	pFont->pixelAscender = pFont->ascender * pFont->pixelSizeY / pFont->unitsPerEm;
	pFont->pixelDescender = pFont->descender * pFont->pixelSizeY / pFont->unitsPerEm;
	pFont->pixelHeight = pFont->height * pFont->pixelSizeY / pFont->unitsPerEm;
}

void Text_RemoveFont(tFont *pFont) {
	// A font has been disposed of, so tell the cache system.
	FTC_Manager_RemoveFaceID(ftCache, (FTC_FaceID)pFont);
}

typedef struct tTextLine_ tTextLine;
struct tTextLine_ {
	STRING2 startChar;
	I32 numChars;
	I32 pixelWidth;
	tTextLine *pNext;
};

#define INIT_LINE(pLine, firstChar) pLine = TMALLOC(tTextLine); pLine->startChar = firstChar; pLine->numChars = 0; pLine->pixelWidth = 0; pLine->pNext = NULL

void DrawMeasureString(tGraphics *pGraphics, STRING2 s, tFont *pFont, tBrush *pBrush, I32 x1, I32 y1, I32 x2, I32 y2, tStringFormat *pFormat, U32 *pSzWidth, U32 *pSzHeight) {
	FT_Error ftErr;
	I32 x, y;
	I32 width, height;
	FTC_ImageTypeRec imageTypeRender, imageTypeOutline;
	U8 wrap, lineLimit, rightToLeft, clip;
	U32 alignment, lineAlignment;
	tTextLine *pFirstLine = NULL, *pCurLine;
	STRING2 pText, pPrevWhiteSpace;
	U16 c;
	U32 glyphIndex;
	U32 wasPrevWhiteSpace;
	I32 extraPixelWidth, extraNumChars;
	I32 numLines;

	EnsureFT();

	width = (U32)x2-x1;
	height = (U32)y2-y1;

	imageTypeOutline.face_id = imageTypeRender.face_id = (FTC_FaceID)pFont;
	imageTypeOutline.width = imageTypeRender.width = pFont->pixelSizeX;
	imageTypeOutline.height = imageTypeRender.height = pFont->pixelSizeY;
	switch (pGraphics->textRenderingHint) {
		case TextRenderingHint_SingleBitPerPixelGridFit:
		case TextRenderingHint_SingleBitPerPixel:
			imageTypeRender.flags = FT_LOAD_RENDER | FT_LOAD_MONOCHROME | FT_LOAD_TARGET_MONO;
			imageTypeOutline.flags = FT_LOAD_MONOCHROME | FT_LOAD_TARGET_MONO;
			break;
		default:
			imageTypeRender.flags = FT_LOAD_RENDER | FT_LOAD_TARGET_NORMAL;
			imageTypeOutline.flags = FT_LOAD_TARGET_NORMAL;
			break;
	}

	// wrap - should the text wrap at the end of lines
	wrap = (pFormat->formatFlags & StringFormatFlags_NoWrap)?0:1;
	// lineLimit - true to not show partial lines of text
	lineLimit = (pFormat->formatFlags & StringFormatFlags_LineLimit)?1:0;
	// rightToLeft - text must be printed right-to-left
	rightToLeft = (pFormat->formatFlags & StringFormatFlags_DirectionRightToLeft)?1:0;
	// clip - Clip text
	clip = (pFormat->formatFlags & StringFormatFlags_NoClip)?0:1;
	// alignment - left/right alignment
	alignment = pFormat->alignment;
	// lineAlignment - up/down alignment
	lineAlignment = pFormat->lineAlignment;

	y = y1 + pFont->pixelAscender;

	pText = s;
	pPrevWhiteSpace = NULL;

	INIT_LINE(pFirstLine, pText);
	pCurLine = pFirstLine;
	wasPrevWhiteSpace = 0;
	extraPixelWidth = 0;
	extraNumChars = 0;
	numLines = 1;

	// Calulate the line layout and line pixel widths
	while ((c = *pText) != 0) {
		FT_Glyph glyph;
		U8 isWhiteSpace, isNewLine, isIgnore;

		if (lineLimit) {
			if (y + pFont->pixelDescender > y2) {
				numLines--;
				break;
			}
		} else {
			if (y  - pFont->pixelAscender > y2) {
				numLines--;
				break;
			}
		}

		isWhiteSpace =
			(c == ' ');
		isNewLine = 
			(c == '\n');
		isIgnore =
			(c == '\r');

		if (isIgnore) {
			*pText = 0;
			pText++;
			extraNumChars++;
			continue;
		}
		
		glyphIndex = FTC_CMapCache_Lookup(ftCMapCache, (FTC_FaceID)pFont, pFont->charMapIndex, c);
		ftErr = FTC_ImageCache_Lookup(ftImageCache, &imageTypeOutline, glyphIndex, &glyph, NULL);
		FT_CHECKERROR("FTC_ImageCache_Lookup() layout calculation");

		if (isWhiteSpace) {
			pPrevWhiteSpace = pText;
			if (!wasPrevWhiteSpace) {
				pCurLine->numChars += extraNumChars;
				pCurLine->pixelWidth += extraPixelWidth;
				extraNumChars = 0;
				extraPixelWidth = 0;
			}
		} else {
			if (isNewLine) {
				pText++;
				pCurLine->numChars += extraNumChars;
				pCurLine->pixelWidth += extraPixelWidth;
				INIT_LINE(pCurLine->pNext, pText);
			} else if (pCurLine->pixelWidth + extraPixelWidth + (glyph->advance.x >> 16) > width &&
				pCurLine->numChars > 0) {
				// Create new line (but only if there's already at least 1 character in this line)
				if (pPrevWhiteSpace == NULL) {
					// No white-space yet, so just split here
					pCurLine->numChars += extraNumChars;
					pCurLine->pixelWidth += extraPixelWidth;
					INIT_LINE(pCurLine->pNext, pText);
				} else {
					// Split after previous white-space
					INIT_LINE(pCurLine->pNext, pPrevWhiteSpace+1);
					pText = pPrevWhiteSpace + 1;
					pPrevWhiteSpace = NULL;
				}
				isNewLine = 1;
			}
			if (isNewLine) {
				pCurLine = pCurLine->pNext;
				extraNumChars = 0;
				extraPixelWidth = 0;
				y += pFont->pixelHeight;
				numLines++;
				continue;
			}
		}

		extraNumChars++;
		extraPixelWidth += glyph->advance.x >> 16;

		pText++;
		wasPrevWhiteSpace = isWhiteSpace;
	}
	pCurLine->numChars += extraNumChars;
	pCurLine->pixelWidth += extraPixelWidth;

	// If only measuring string, then calculate results, otherwise render string
	if (pBrush == NULL) {

		U32 maxWidth = 0;
		U32 height = 0;

		pCurLine = pFirstLine;
		while (pCurLine != NULL) {
			if (pCurLine->pixelWidth > (I32)maxWidth) {
				maxWidth = pCurLine->pixelWidth;
			}
			height += pFont->pixelHeight;
			pCurLine = pCurLine->pNext;
		}

		*pSzWidth = maxWidth;
		*pSzHeight = height;

	} else {

		// Render all the lines of text
		switch (lineAlignment) {
		case StringAlignment_Far:
			y = y2 - numLines * pFont->pixelHeight;
			break;
		case StringAlignment_Center:
			y = y1 + ((height - numLines * pFont->pixelHeight) >> 1);
			break;
		default:
			y = y1;
			break;
		}
		y += pFont->pixelAscender;
		pCurLine = pFirstLine;
		while (pCurLine != NULL) {
			U32 i;
			pText = pCurLine->startChar;
			switch (alignment) {
			case StringAlignment_Far:
				x = (rightToLeft)?(x1+pCurLine->pixelWidth):(x2-pCurLine->pixelWidth);
				break;
			case StringAlignment_Center:
				x = (rightToLeft)?(x2 - ((width - pCurLine->pixelWidth) >> 1)):(x1 + ((width - pCurLine->pixelWidth) >> 1));
				break;
			default:
				x = (rightToLeft)?x2:x1;
				break;
			}
			for (i=pCurLine->numChars; i>0; i--, pText++) {
				FT_BitmapGlyph glyph;
				if (*pText == 0) {
					// All 'ignore' characters are set to 0
					continue;
				}
				glyphIndex = FTC_CMapCache_Lookup(ftCMapCache, (FTC_FaceID)pFont, pFont->charMapIndex, *pText);
				ftErr = FTC_ImageCache_Lookup(ftImageCache, &imageTypeRender, glyphIndex, (FT_Glyph*)&glyph, NULL);
				FT_CHECKERROR("FTC_ImageCache_Lookup() rendering");
				if (rightToLeft) {
					x -= glyph->root.advance.x >> 16;
				}
				mShowFTBitmap(pGraphics, pBrush, x, y, glyph);
				if (!rightToLeft) {
					x += glyph->root.advance.x >> 16;
				}
			}
			y += pFont->pixelHeight;
			pCurLine = pCurLine->pNext;
		}
	}

	// Free line structures
	while (pFirstLine != NULL) {
		pCurLine = pFirstLine->pNext;
		free(pFirstLine);
		pFirstLine = pCurLine;
	}
}

void MeasureString_(tGraphics *pGraphics, STRING2 s, tFont *pFont, I32 width, tStringFormat *pFormat, U32 *pSzWidth, U32 *pSzHeight) {
	DrawMeasureString(pGraphics, s, pFont, NULL, 0, 0, width, 0x7fffffff, pFormat, pSzWidth, pSzHeight);
}

void DrawString_(tGraphics *pGraphics, STRING2 s, tFont *pFont, tBrush *pBrush, I32 x1, I32 y1, I32 x2, I32 y2, tStringFormat *pFormat) {
	DrawMeasureString(pGraphics, s, pFont, pBrush, x1, y1, x2, y2, pFormat, NULL, NULL);
}

#else

void Text_EnsureFontMetrics(tFont *pFont) {
	// Do nothing
}

void Text_RemoveFont(tFont *pFont) {
	// Do nothing
}

void DrawString_(tGraphics *pGraphics, STRING2 s, tFont *pFont, tBrush *pBrush, I32 x1, I32 y1, I32 x2, I32 y2, tStringFormat *pFormat) {
	// Do nothing
}

void MeasureString_(tGraphics *pGraphics, STRING2 s, tFont *pFont, I32 width, tStringFormat *pFormat, U32 *pSzWidth, U32 *pSzHeight) {
	*pSzWidth = 0;
	*pSzHeight = 0;
	// Do nothing
}

#endif
