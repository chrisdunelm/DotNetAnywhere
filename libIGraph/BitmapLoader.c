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

#include "tinyjpeg.h"

typedef struct tBitmapFileHeader_ tBitmapFileHeader;
struct tBitmapFileHeader_ {
	U16 bfType;
	U32 bfSize;
	U16 bfReserved1;
	U16 bfReserved2;
	U32 bfOffBits;
};

typedef struct tBitmapInfoHeader_ tBitmapInfoHeader;
struct tBitmapInfoHeader_ {
	U32 biSize;
	I32 biWidth;
	I32 biHeight;
	U16 biPlanes;
	U16 biBitCount;
	U32 biCompression;
	U32 biSizeImage;
	I32 biXPelsPerMetre;
	I32 biYPelsPerMetre;
	U32 biClrUsed;
	U32 biClrImportant;
};

typedef struct tRGBQuad_ tRGBQuad;
struct tRGBQuad_ {
	U8 blue;
	U8 green;
	U8 red;
	U8 unused;
};

#define BI_RGB        0L
#define BI_RLE8       1L
#define BI_RLE4       2L

tBitmap* LoadBMP(U8 *pFile, I32 *pWidth, I32 *pHeight, U32 *pPixelFormat) {
	tBitmapInfoHeader bih;
	tBitmap *pBmp;
	U8 u8;
	U32 x, y, r, g, b, col, col2;
	U32 width, height;
	U32 linePadding;
	U8 *pFilePicMem;
	U32 *pBmpMem;
	U32 i;
	tRGBQuad *pColTable;

	// Need to do it this way as the data is not 32-bit aligned in file :(
	memcpy(&bih, pFile + 14, sizeof(tBitmapInfoHeader));
	if (bih.biSize != 40) {
		// Only handle BMP v3 files
		return NULL;
	}

	pBmp = CreateBitmap_(bih.biWidth, bih.biHeight, PixelFormat_Format32bppArgb);
	*pWidth = width = pBmp->image.width;
	*pHeight = height = pBmp->image.height;
	*pPixelFormat = pBmp->image.pixelFormat;
	pBmpMem = (U32*)(pBmp->pBmp + (bih.biHeight-1) * pBmp->stride);
	pFilePicMem = pFile + 14 + sizeof(tBitmapInfoHeader);

	switch (bih.biBitCount) {
		case 4:
			pColTable = (tRGBQuad*)pFilePicMem;
			pFilePicMem += 16 * 4;
			break;
		case 8:
			pColTable = (tRGBQuad*)pFilePicMem;
			pFilePicMem += 256 * 4;
			break;
	}

	linePadding = (32 - ((((bih.biBitCount * bih.biWidth) & 0x1f)) >> 3)) & 0x3;

	switch (bih.biBitCount) {
		case 4:
			switch (bih.biCompression) {
				case BI_RGB:
					for (y=0; y<height; y++) {
						for (x=0; x<(width>>1); x++) {
							col2 = *pFilePicMem++;
							col = col2 >> 4;
							r = pColTable[col].red;
							g = pColTable[col].green;
							b = pColTable[col].blue;
							col = ARGB(255, r, g, b);
							*pBmpMem++ = col;
							col = col2 & 0xf;
							r = pColTable[col].red;
							g = pColTable[col].green;
							b = pColTable[col].blue;
							col = ARGB(255, r, g, b);
							*pBmpMem++ = col;
						}
						if (width & 1) {
							col2 = *pFilePicMem++;
							col = col2 >> 4;
							r = pColTable[col].red;
							g = pColTable[col].green;
							b = pColTable[col].blue;
							col = ARGB(255, r, g, b);
							*pBmpMem++ = col;
						}
						pBmpMem -= pBmp->stride >> 1;
						pFilePicMem += linePadding;
					}
					break;
				case BI_RLE4:
					x = y = 0;
					for (;;) {
						u8 = *pFilePicMem++;
						if (u8 == 0) {
							u8 = *pFilePicMem++;
							switch (u8) {
								case 0: // End of line
									y++;
									if (y > (U32)bih.biHeight) {
										goto doneBad;
									}
									for (; x < (U32)bih.biWidth; x++) {
										*pBmpMem++ = ARGB(255,0,0,0);
									}
									x = 0;
									pBmpMem -= pBmp->stride >> 1;
									break;
								case 1: // End of bitmap
									goto doneGood;
								case 2: // Delta - can only handle horizontal deltas
									u8 = *pFilePicMem++;
									if (*pFilePicMem++ != 0) {
										goto doneBad;
									}
									for (i=u8 - 1; i>0; i--) {
										*pBmpMem++ = ARGB(255,0,0,0);
										x++;
									}
									break;
								default: // absolute mode
									x += u8;
									if (x > (U32)bih.biWidth) {
										// Problem with bitmap file - too many pixels in row
										goto doneBad;
									}
									for (i=u8 >> 1; i>0; i--) {
										col2 = *pFilePicMem++;
										col = col2 >> 4;
										r = pColTable[col].red;
										g = pColTable[col].green;
										b = pColTable[col].blue;
										col = ARGB(255, r, b, g);
										*pBmpMem++ = col;
										col = col2 & 0x0f;
										r = pColTable[col].red;
										g = pColTable[col].green;
										b = pColTable[col].blue;
										col = ARGB(255, r, b, g);
										*pBmpMem++ = col;
									}
									if (u8 & 1) {
										col = *pFilePicMem++;
										col >>= 4;
										r = pColTable[col].red;
										g = pColTable[col].green;
										b = pColTable[col].blue;
										col = ARGB(255, r, b, g);
										*pBmpMem++ = col;
									}
									if ((u8 & 0x3) == 1 || (u8 & 0x3) == 2) {
										pFilePicMem++;
									}
									break;
							}
						} else {
							x += u8;
							if (x > (U32)bih.biWidth) {
								// Problem with bitmap file - too many pixels in row
								goto doneBad;
							}
							col2 = *pFilePicMem++;
							col = col2 >> 4;
							r = pColTable[col].red;
							g = pColTable[col].green;
							b = pColTable[col].blue;
							col = ARGB(255, r, g, b);
							col2 &= 0x0f;
							r = pColTable[col2].red;
							g = pColTable[col2].green;
							b = pColTable[col2].blue;
							col2 = ARGB(255, r, g, b);
							for (i=u8 >> 1; i>0; i--) {
								*pBmpMem++ = col;
								*pBmpMem++ = col2;
							}
							if (u8 & 1) {
								*pBmpMem++ = col;
							}
						}
					}
					break;
				default:
					goto doneBad;
			}
			break;
		case 8:
			switch (bih.biCompression) {
				case BI_RGB:
					for (y=0; y<height; y++) {
						for (x=0; x<width; x++) {
							col = *pFilePicMem++;
							r = pColTable[col].red;
							g = pColTable[col].green;
							b = pColTable[col].blue;
							col = ARGB(255, r, g, b);
							*pBmpMem++ = col;
						}
						pBmpMem -= pBmp->stride >> 1;
						pFilePicMem += linePadding;
					}
					break;
				case BI_RLE8:
					x = y = 0;
					for (;;) {
						u8 = *pFilePicMem++;
						if (u8 == 0) {
							u8 = *pFilePicMem++;
							switch (u8) {
								case 0: // End of line
									y++;
									if (y > (U32)bih.biHeight) {
										goto doneBad;
									}
									for (; x < (U32)bih.biWidth; x++) {
										*pBmpMem++ = ARGB(255,0,0,0);
									}
									x = 0;
									pBmpMem -= pBmp->stride >> 1;
									break;
								case 1: // End of bitmap
									goto doneGood;
								case 2: // Delta - can't handle these
									goto doneBad;
								default: // absolute mode
									x += u8;
									if (x > (U32)bih.biWidth) {
										// Problem with bitmap file - too many pixels in row
										goto doneBad;
									}
									for (i = u8; i>0; i--) {
										col = *pFilePicMem++;
										r = pColTable[col].red;
										g = pColTable[col].green;
										b = pColTable[col].blue;
										col = ARGB(255, r, b, g);
										*pBmpMem++ = col;
									}
									if (u8 & 1) {
										pFilePicMem++;
									}
									break;
							}
						} else {
							x += u8;
							if (x > (U32)bih.biWidth) {
								// Problem with bitmap file - too many pixels in row
								goto doneBad;
							}
							col = *pFilePicMem++;
							r = pColTable[col].red;
							g = pColTable[col].green;
							b = pColTable[col].blue;
							col = ARGB(255, r, b, g);
							for (i=u8; i>0; i--) {
								*pBmpMem++ = col;
							}
						}
					}
					break;
				default:
					goto doneBad;
			}
			break;
		case 24:
			for (y=0; y<height; y++) {
				for (x=0; x<width; x++) {
					r = *pFilePicMem++;
					g = *pFilePicMem++;
					b = *pFilePicMem++;
					col = ARGB(255, r, g, b);
					*pBmpMem++ = col;
				}
				pBmpMem -= pBmp->stride >> 1;
				pFilePicMem += linePadding;
			}
			break;
		case 32:
			for (y=0; y<height; y++) {
				for (x=0; x<width; x++) {
					r = *pFilePicMem++;
					g = *pFilePicMem++;
					b = *pFilePicMem++;
					pFilePicMem++;
					col = ARGB(255, r, g, b);
					*pBmpMem++ = col;
				}
				pBmpMem -= pBmp->stride >> 1;
				pFilePicMem += linePadding;
			}
			break;
		default:
doneBad:
			DisposeImage_((tImage*)pBmp);
			return NULL;
	}

doneGood:
	return pBmp;
}

typedef struct tGifHeader_ tGifHeader;
struct tGifHeader_ {
	unsigned char gif[3];
	unsigned char version[3];
};

// This always follows directly after the GifHeader
typedef struct tGifLogicalScreenDescriptor_ tGifLogicalScreenDescriptor;
struct tGifLogicalScreenDescriptor_ {
	U16 width;
	U16 height;
	U8 flags;
	U8 transparentIndex;
	U8 pixelAspectRatio;
};
#define GIF_LSD_HAS_GLOBALCOLOURTABLE 0x80
#define GIF_LSD_GLOBALCOLOURTABLE_SIZE_MASK 0x7

typedef struct tGifColourTableEntry_ tGifColourTableEntry;
struct tGifColourTableEntry_ {
	U8 red;
	U8 green;
	U8 blue;
};

typedef struct tGifImageDescriptor_ tGifImageDescriptor;
struct tGifImageDescriptor_ {
	U8 imageSeparator;
	U8 xPos1, xPos2;
	U8 yPos1, yPos2;
	U8 width1, width2;
	U8 height1, height2;
	U8 flags;
};
#define GIF_ID_HAS_LOCALCOLOURTABLE 0x80
#define GIF_ID_IS_INTERLACED 0x40

typedef struct tGifDictEntry_ tGifDictEntry;
struct tGifDictEntry_ {
	U16 prevIndex;
	U16 suffix;
};

tBitmap* LoadGIF(U8 *pFile, I32 *pWidth, I32 *pHeight, U32 *pPixelFormat) {
	tBitmap *pBmp;
	U32 width, height;
	U32 *pBmpMem;
	tGifLogicalScreenDescriptor *pLSD;
	U32 gctSize;
	tGifColourTableEntry *pColTable;
	tGifImageDescriptor *pImageDesc;
	U32 isInterlaced;
	U8 blockLen;
	U8 *pFileBlock;
	U32 bitsPerCode, codeMask;
	U32 clearCode, endCode;
	U32 curCode, curCodeBitCount;
	tGifDictEntry *pDict = NULL;
	U32 dictOfs;
	U32 codeSize;
	U32 i, w;
	U32 x, y;
	U16 *pLineRedirect = NULL;
	
	pLSD = (tGifLogicalScreenDescriptor*)(pFile + sizeof(tGifHeader));
	if (!(pLSD->flags & GIF_LSD_HAS_GLOBALCOLOURTABLE)) {
		// Can only decode GIF's with global colour table
		return NULL;
	}

	pBmp = CreateBitmap_(pLSD->width, pLSD->height, PixelFormat_Format32bppArgb);
	*pWidth = width = pBmp->image.width;
	*pHeight = height = pBmp->image.height;
	*pPixelFormat = pBmp->image.pixelFormat;
	pBmpMem = (U32*)(pBmp->pBmp);
	gctSize = 1 << ((pLSD->flags & GIF_LSD_GLOBALCOLOURTABLE_SIZE_MASK) + 1);
	pColTable = (tGifColourTableEntry*)(((U8*)pLSD) + 7);
	
	pImageDesc = (tGifImageDescriptor*)(pColTable + gctSize);
	if (pImageDesc->imageSeparator != 0x2c ||
		pImageDesc->xPos1 != 0 || pImageDesc->xPos2 != 0 ||
		pImageDesc->yPos1 != 0 || pImageDesc->yPos2 != 0 ||
		pImageDesc->width1 != (width & 0xff) || pImageDesc->width2 != (width >> 8) ||
		pImageDesc->height1 != (height & 0xff) || pImageDesc->height2 != (height >> 8) ||
		(pImageDesc->flags & GIF_ID_HAS_LOCALCOLOURTABLE))  {
		goto doneBad;
	}
	isInterlaced = pImageDesc->flags & GIF_ID_IS_INTERLACED;

	pLineRedirect = (U16*)malloc(height << 1);
	if (isInterlaced) {
		static U8 passes[] = {3,0, 3,4, 2,2, 1,1};
		U32 pass = 0, iOfs = 0;
		for (i=0; i<height; i++) {
			U32 target;
reTarget:
			target = ((i - iOfs) << passes[pass]) + passes[pass+1];
			if (target >= height) {
				pass += 2;
				iOfs = i;
				goto reTarget;
			}
			pLineRedirect[i] = target;
		}
	} else {
		for (i=0; i<height; i++) {
			pLineRedirect[i] = i;
		}
	}

	// Process all the image blocks
	pFileBlock = ((U8*)pImageDesc) + 10;
	codeSize = *pFileBlock++;
	bitsPerCode = codeSize + 1;
	codeMask = (1 << bitsPerCode) - 1;
	clearCode = 1 << (bitsPerCode - 1);
	endCode = clearCode + 1;
	curCode = 0;
	curCodeBitCount = 0;
	pDict = malloc(0x1000 * sizeof(tGifDictEntry));
	for (i=0; i<clearCode; i++) {
		pDict[i].prevIndex = 0xffff;
		pDict[i].suffix = i;
	}
	w = 0xffffffff;
	dictOfs = endCode + 1;
	x = y = 0;
	while ((blockLen = *pFileBlock++) != 0) {
		for (;;) {
			U32 code;
			U32 pixelCount, codeChain, codeChain0;
			while (curCodeBitCount < bitsPerCode) {
				if (blockLen == 0) {
					goto blockDone;
				}
				blockLen--;
				curCode |= (*pFileBlock++) << curCodeBitCount;
				curCodeBitCount += 8;
			}
			code = curCode & codeMask;
			curCode >>= bitsPerCode;
			curCodeBitCount -= bitsPerCode;
			if (code == clearCode) {
				dictOfs = endCode + 1;
				bitsPerCode = codeSize + 1;
				codeMask = (1 << bitsPerCode) - 1;
				w = 0xffffffff;
			} else if (code == endCode) {
				goto doneGood;
			} else {
				U32 dontAddToDict = 0;
				if (code > clearCode) {
					if (code == dictOfs) {
						// Special case - add to dictionary here, and don't later
						dontAddToDict = 1;
						pDict[dictOfs].prevIndex = w;
						codeChain = w;
						while (pDict[codeChain].prevIndex != 0xffff) {
							codeChain = pDict[codeChain].prevIndex;
						}
						pDict[dictOfs].suffix = codeChain;
						dictOfs++;
					} else if (code > dictOfs){
						// Invalid code
						goto doneGood;
					}
				}
				// Put the data identifed by entry into the final bitmap
				// Count how many pixels are about to be set
				codeChain = code;
				pixelCount=1;
				while (pDict[codeChain].prevIndex != 0xffff) {
					codeChain = pDict[codeChain].prevIndex;
					pixelCount++;
				}
				codeChain0 = codeChain;
				// Fill up the pixels
				codeChain = code;
				for (i = pixelCount; i > 0; i--) {
					// TODO: Optimize!!! This is horribly inefficient.
					tGifColourTableEntry *pCol = &pColTable[pDict[codeChain].suffix];
					U32 col = ARGB(255, pCol->red, pCol->green, pCol->blue);
					U32 xLocal = x + i - 1;
					U32 yLocal = y + xLocal / width;
					xLocal %= width;
					*(pBmpMem + pLineRedirect[yLocal] * width + xLocal) = col;
					codeChain = pDict[codeChain].prevIndex;
				}

				x += pixelCount;
				y += x / width;
				x %= width;
				if (y >= height && x > 0) {
					goto doneBad;
				}

				if (w != 0xffffffff && !dontAddToDict && dictOfs < 0x1000) {
					pDict[dictOfs].prevIndex = w;
					pDict[dictOfs].suffix = codeChain0;
					dictOfs++;
				}
				w = code;
				if (dictOfs > codeMask) {
					bitsPerCode++;
					codeMask = (1 << bitsPerCode) - 1;
				}
			}
		}
blockDone:;
	}

doneGood:
	free(pDict);
	return pBmp;

doneBad:
	free(pDict);
	DisposeImage_((tImage*)pBmp);
	return NULL;
}

tBitmap* LoadJPEG(U8 *pFile, U32 fileSize, I32 *pWidth, I32 *pHeight, U32 *pPixelFormat) {
	struct jdec_private *pJpegDecoder;
	int res;
	U32 width, height;
	U8 *pRGB;
	tBitmap *pBmp;
	U32 i;
	U32 *pBmpMem;

	pJpegDecoder = tinyjpeg_init();
	if (pJpegDecoder == NULL) {
		return NULL;
	}
	res = tinyjpeg_parse_header(pJpegDecoder, pFile, fileSize);
	if (res < 0) {
		tinyjpeg_free(pJpegDecoder);
		return NULL;
	}
	tinyjpeg_get_size(pJpegDecoder, (unsigned int*)&width, (unsigned int*)&height);
	*pWidth = (I32)width;
	*pHeight = (I32)height;
	*pPixelFormat = PixelFormat_Format32bppArgb;
	res = tinyjpeg_decode(pJpegDecoder, TINYJPEG_FMT_RGB24);
	if (res < 0) {
		tinyjpeg_free(pJpegDecoder);
		return NULL;
	}
	tinyjpeg_get_components(pJpegDecoder, &pRGB);

	pBmp = CreateBitmap_(width, height, PixelFormat_Format32bppArgb);
	pBmpMem = (U32*)(pBmp->pBmp);
	for (i = width*height; i>0; i--) {
		*pBmpMem++ = ARGB(255, pRGB[0], pRGB[1], pRGB[2]);
		pRGB += 3;
	}

	tinyjpeg_free(pJpegDecoder);

	return pBmp;
}

tBitmap* BitmapFromFile_(STRING filename, I32 *pWidth, I32 *pHeight, U32 *pPixelFormat) {

	U8 *pFile;
	U32 fileSize;
	tBitmap *pBmp;

	{
		U32 bytesRead;
		int f;
		f = open(filename, O_BINARY | O_RDONLY);
		if (f < 0) {
			return NULL;
		}
		fileSize = (U32)lseek(f, 0, SEEK_END);
		lseek(f, 0, SEEK_SET);
		pFile = (U8*)malloc(fileSize);
		bytesRead = read(f, pFile, fileSize);
		close(f);
		if (bytesRead != fileSize) {
			free(pFile);
			return NULL;
		}
	}

	pBmp = NULL;
	// See what kind of file this is
	if (pFile[0] == 'B' && pFile[1] == 'M') {
		// It's a bmp file
		printf("BMP\n");
		pBmp = LoadBMP(pFile, pWidth, pHeight, pPixelFormat);
	}
	else
	if (pFile[0] == 'G' && pFile[1] == 'I' && pFile[2] == 'F') {
		// It's a GIF file
		pBmp = LoadGIF(pFile, pWidth, pHeight, pPixelFormat);
	}
	else
	if (pFile[0] == 0xff && pFile[1] == 0xd8) {
		// It's a JPEG file
		pBmp = LoadJPEG(pFile, fileSize, pWidth, pHeight, pPixelFormat);
	}

	free(pFile);

	return pBmp;
}
