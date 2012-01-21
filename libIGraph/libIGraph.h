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

#ifndef __LIBIGRAPH_H
#define __LIBIGRAPH_H

#include "Config.h"

// Bitmap format indexes used in the function look-ups.
#define FMT_NUM 2
#define FMT_4BPP_GRAY 0
#define FMT_32BPP_ARGB 1

#define TMALLOC(type) (type*)malloc(sizeof(type))

#define PT_TO_PIXEL_X(pt) ((I32)(((pt) * 84) / 57))
#define PT_TO_PIXEL_Y(pt) ((I32)(((pt) * 84) / 57))

#define ABS(x) (((x)<0)?(-(x)):(x))
#define SWAP_I32(a,b) {I32 __tmp = a;a=b;b=__tmp;}

#define A(col) ((col) >> 24)
#define R(col) (((col) >> 16) & 0xff)
#define G(col) (((col) >> 8) & 0xff)
#define B(col) ((col) & 0xff)
#define ARGB(a,r,g,b) ((U32)(((a) << 24) | (((r) & 0xff) << 16) | (((g) & 0xff) << 8) | ((b) & 0xff)))

// A crude approximation of R*0.3 + G*0.59 + B*0.11
#define COL2GRAY(col) ((R(col) >> 2) + (G(col) >> 1) + (B(col) >> 2))

typedef unsigned char* STRING;
typedef unsigned short* STRING2;

#ifdef WIN32

#pragma warning(disable:4996)

typedef int I32;
typedef unsigned int U32;
typedef short I16;
typedef unsigned short U16;
typedef char I8;
typedef unsigned char U8;

typedef U16 uint16_t;
typedef I16 int16_t;
typedef U8 uint8_t;
typedef I8 int8_t;

#define EXPORT(returnType) __declspec(dllexport) returnType __stdcall

#undef INVERT_4BPP_GRAY

#define DIR_SEPARATOR "\\"

#include <windows.h>
#include <io.h>

#define snprintf _snprintf

#else

#include <sys/types.h>
#include <sys/mman.h>
#include <unistd.h>

#define EXPORT(returnType) returnType

#define INVERT_4BPP_GRAY

#define DIR_SEPARATOR "/"

#define O_BINARY 0

typedef U16 uint16_t;
typedef U8 uint8_t;

#endif

#define inline

#define FONT_DIR "." DIR_SEPARATOR "Fonts" DIR_SEPARATOR

#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>

#endif
