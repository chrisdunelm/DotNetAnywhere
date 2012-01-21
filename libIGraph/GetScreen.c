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

#include "Graphics.h"

#define XSIZE 320
#define YSIZE 240

#define KEYPAD_0 0
#define KEYPAD_1 1
#define KEYPAD_2 2
#define KEYPAD_3 3
#define KEYPAD_4 4
#define KEYPAD_5 5
#define KEYPAD_6 6
#define KEYPAD_7 7
#define KEYPAD_8 8
#define KEYPAD_9 9
#define KEYPAD_C 10
#define KEYPAD_OK 11

static tGraphics screen;
static U32 windowCreated = 0;

static U8 keyState[12];
static U32 latestKeyDown = 0xffffffff, latestKeyUp = 0xffffffff;

U32 GetScreenPixel(I32 x, I32 y) {
	if (windowCreated) {
		U8* pMem = (U8*)screen.pScan0 + screen.stride*y + (x >> 1);
		U8 b = *pMem;
		b = (x&1)?b&0xf:b>>4;
#ifdef INVERT_4BPP_GRAY
		b = 15 - b;
#endif
		b = (b << 4) | b;
		return ARGB(0xff, b, b, b);
	} else {
		return 0xff000000;
	}
}

#ifdef WIN32

static HBITMAP hBackBuffer;
static int doUpdates = 0; // Should be do periodic updates of the window

static void UpdateScreenWindow() {
	PAINTSTRUCT ps;
	HDC memHdc;
	HGDIOBJ orgObj;

	BeginPaint((HWND)screen.screenPtr, &ps);

	memHdc = CreateCompatibleDC(ps.hdc);
	orgObj = SelectObject(memHdc, hBackBuffer);
	BitBlt(ps.hdc, 0, 0, screen.xSize, screen.ySize, memHdc, 0, 0, SRCCOPY);
	SelectObject(memHdc, orgObj);
	DeleteObject(memHdc);
	EndPaint((HWND)screen.screenPtr, &ps);
}

U32 IsKeyDown_Internal_(U32 key) {
	return keyState[key];
}

U32 LatestKeyUp_Internal_() {
	U32 ret = latestKeyUp;
	latestKeyUp = 0xffffffff;
	return ret;
}

U32 LatestKeyDown_Internal_() {
	U32 ret = latestKeyDown;
	latestKeyDown = 0xffffffff;
	return ret;
}

static U32 MapKey(U32 keyCode) {
	switch (keyCode) {
		case 48:
		case 96:
			return KEYPAD_0;
		case 49:
		case 97:
			return KEYPAD_1;
		case 50:
		case 98:
			return KEYPAD_2;
		case 51:
		case 99:
			return KEYPAD_3;
		case 52:
		case 100:
			return KEYPAD_4;
		case 53:
		case 101:
			return KEYPAD_5;
		case 54:
		case 102:
			return KEYPAD_6;
		case 55:
		case 103:
			return KEYPAD_7;
		case 56:
		case 104:
			return KEYPAD_8;
		case 57:
		case 105:
			return KEYPAD_9;
		case 27:
			return KEYPAD_C;
		case 13:
			return KEYPAD_OK;
		default:
			return 0xffffffff;
	}
}

static LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	U32 key;
	switch (uMsg) {
		case WM_CLOSE:
			// Tell the program to end, and dispatch WM_DESTROY
			DestroyWindow(hWnd);
			break;
		case WM_DESTROY:
			// End the program.
			PostQuitMessage(0);
			exit(0);
			break;
		case WM_TIMER:
			// Update window contents if doUpdates is 1
			if (doUpdates) {
				InvalidateRect((HWND)screen.screenPtr, NULL, 0);
			}
			break;
		case WM_PAINT:
			if (windowCreated) {
				UpdateScreenWindow();
			} else {
				return DefWindowProc(hWnd, uMsg, wParam, lParam);
			}
			break;
		case WM_ERASEBKGND:
			break;
		case WM_KEYDOWN:
			key = MapKey((U32)wParam);
			if (key <= 12) {
				keyState[key] = 1;
			}
			latestKeyDown = key;
			break;
		case WM_KEYUP:
			key = MapKey((U32)wParam);
			if (key <= 12) {
				keyState[key] = 0;
			}
			latestKeyUp = key;
			break;
		default:
			return DefWindowProc(hWnd, uMsg, wParam, lParam);
	}
	return 0;
}

static const WCHAR className[] = L"DotNetAnywhere";

static DWORD WINAPI WindowMessagePump(void *arg) {

	WNDCLASSEX wc;
	HWND hWnd;
	HINSTANCE hInstance;
	MSG msg;
	BITMAPINFO *pBmpInfo;
	U32 i;
	RECT r;
	DWORD winStyle, winExStyle;

	hInstance = GetModuleHandle(NULL);

	wc.cbSize = sizeof(WNDCLASSEX);
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW);
	wc.hCursor = LoadCursor(hInstance, IDC_ARROW);
	wc.hIcon = LoadIcon(hInstance, IDI_APPLICATION);
	wc.hIconSm = LoadIcon(hInstance, IDI_APPLICATION);
	wc.hInstance = hInstance;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = className;
	wc.lpszMenuName = NULL;
	wc.style = 0;

	if (!RegisterClassEx(&wc)) {
		// Error
		printf("[libIGraph] ERROR: Cannot register window class. error=0x%08x\n", GetLastError());
		windowCreated = 2;
		return 0;
	}

	winStyle = WS_OVERLAPPEDWINDOW;
	winExStyle = WS_EX_WINDOWEDGE;

	r.left = 100;
	r.top = 100;
	r.right = 100 + XSIZE;
	r.bottom = 100 + YSIZE;
	AdjustWindowRectEx(&r, winStyle, 0, winExStyle);

	hWnd = CreateWindowEx(winExStyle, className, L"Dot Net Anywhere", winStyle,
		CW_USEDEFAULT, CW_USEDEFAULT, r.right - r.left, r.bottom - r.top, NULL, NULL, hInstance, NULL);
	if (hWnd == NULL) {
		printf("[libIGraph] ERROR: Cannot create window. error=0x%08x\n", GetLastError());
		windowCreated = 2;
		return 0;
	}

	ShowWindow(hWnd, SW_SHOWNORMAL);
	UpdateWindow(hWnd);

	memset(&screen, 0, sizeof(tGraphics));
	screen.xSize = XSIZE;
	screen.ySize = YSIZE;
	screen.pixelFormat = PixelFormat_Gdi;
	screen.pixelFormatIndex = FMT_4BPP_GRAY;
	screen.stride = XSIZE / 2;
	screen.textRenderingHint = TextRenderingHint_SystemDefault;
	screen.pImage = NULL;
	// Create a memory back-buffer for the screen/window

	pBmpInfo = (BITMAPINFO*)malloc(sizeof(BITMAPINFOHEADER) + 16*4);
	pBmpInfo->bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	pBmpInfo->bmiHeader.biWidth = XSIZE;
	pBmpInfo->bmiHeader.biHeight = -YSIZE;
	pBmpInfo->bmiHeader.biPlanes = 1;
	pBmpInfo->bmiHeader.biBitCount = 4;
	pBmpInfo->bmiHeader.biCompression = BI_RGB;
	pBmpInfo->bmiHeader.biSizeImage = 0;
	pBmpInfo->bmiHeader.biXPelsPerMeter = 0;
	pBmpInfo->bmiHeader.biYPelsPerMeter = 0;
	pBmpInfo->bmiHeader.biClrUsed = 0;
	pBmpInfo->bmiHeader.biClrImportant = 0;
	for (i=0; i<16; i++) {
		U32 c = i | (i << 4);
		pBmpInfo->bmiColors[i].rgbRed = c;
		pBmpInfo->bmiColors[i].rgbGreen = c;
		pBmpInfo->bmiColors[i].rgbBlue = c;
	}
	hBackBuffer = CreateDIBSection(NULL, pBmpInfo, DIB_RGB_COLORS, &screen.pScan0, NULL, 0);

	screen.memSize = XSIZE * YSIZE / 2;
	screen.screenPtr = hWnd;

	windowCreated = 1;

	// Create a timer to update the window from the back-buffer
	SetTimer(hWnd, 1, 40, NULL);

	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}

tGraphics* GetScreen_(I32 *pXSize, I32 *pYSize, U32 *pPixelFormat) {
	
	// Create a new thread to handle all the window messages
	DWORD threadID;
	HANDLE hThread;
	int i;

	if (windowCreated == 0) {
		// Only need to create the window if it hasn't already been done.

		// Initialise the keyState
		memset(keyState, 0, 12);

		hThread = CreateThread(NULL, 0, WindowMessagePump, NULL, 0, &threadID); 
		if (hThread == NULL) {
			// Failed to create thread
			printf("[libIGraph] ERROR: Failed to create window thread. error=0x%08x\n", GetLastError());
			return NULL;
		}

		// Wait for the window to be created
		i = 0;
		while (windowCreated == 0) {
			Sleep(1);
			i++;
			if (i>1000) {
				printf("[libIGraph] ERROR: Window creation timed-out\n");
				return NULL;
			}
		}

		if (windowCreated == 2) {
			windowCreated = 0;
			// Error
			return NULL;
		}
	}

	doUpdates++;

	*pXSize = screen.xSize;
	*pYSize = screen.ySize;
	*pPixelFormat = screen.pixelFormat;
	return &screen;
}

void ScreenDispose() {
	doUpdates--;
	if (doUpdates == 0) {
		// If this is the last screen Graphics to be disposed of, then do one last window update
		InvalidateRect((HWND)screen.screenPtr, NULL, 0);
	}
}

#else

#include <dev/wscons/wsconsio.h>

tGraphics* GetScreen_(I32 *pXSize, I32 *pYSize, U32 *pPixelFormat) {
	if (windowCreated == 0) {
		int s;
		
		// Initialise the keyState
		memset(keyState, 0, 12);

		memset(&screen, 0, sizeof(tGraphics));
		screen.xSize = XSIZE;
		screen.ySize = YSIZE;
		screen.pixelFormat = PixelFormat_Gdi;
		screen.pixelFormatIndex = FMT_4BPP_GRAY;
		screen.stride = XSIZE / 2;
		screen.screenPtr = NULL;
		screen.memSize = XSIZE * YSIZE / 2;
		screen.textRenderingHint = TextRenderingHint_SystemDefault;
		screen.pImage = NULL;
		
		s = open("/dev/wsdisplay0", O_RDWR, 0);
		if (s <= 0) {
			printf("[libIGraph] ERROR: Cannot open /dev/wsdisplay0\n");
			return NULL;
		}
		
		screen.pScan0 = mmap(0, screen.memSize, PROT_READ|PROT_WRITE, MAP_SHARED, s, 0);
		if (screen.pScan0 == MAP_FAILED) {
			printf("[libIGraph] ERROR: Failed to mmap() frame-buffer\n");
			return NULL;
		}
		
		windowCreated = 1;
	}
	
	*pXSize = XSIZE;
	*pYSize = YSIZE;
	*pPixelFormat = screen.pixelFormat;

	return &screen;
}

void ScreenDispose(){
	// Nothing needs to be done.
}

static int keyb = -1;
static void ProcessKeyPad() {

	struct wscons_event e;
	fd_set readfds;
	int res;
	struct timeval tv_timeout;

	if (keyb == -1) {
		// Open keyboard device
		keyb = open("/dev/wskbd0",O_RDWR,0);
		if (keyb < 0) {
			printf("[libIGraph] Error: Cannot open keyboard device: /dev/wskbd0");
			exit(2);
		}
	}
	
	tv_timeout.tv_sec = 0;
	tv_timeout.tv_usec = 0;
	FD_ZERO(&readfds);
	FD_SET(keyb,&readfds);

	for (;;) {
		U32 key;
		res = select(FD_SETSIZE,&readfds,NULL,NULL,&tv_timeout);
		if (res <= 0) {
			// Timeout
			break;
		}
		res = read(keyb,&e,sizeof(e));
		if (res != sizeof(struct wscons_event)) {
			break;
		}
		switch (e.value) {
		case '0':
			key = KEYPAD_0;
			break;
		case '1':
			key = KEYPAD_1;
			break;
		case '2':
			key = KEYPAD_2;
			break;
		case '3':
			key = KEYPAD_3;
			break;
		case '4':
			key = KEYPAD_4;
			break;
		case '5':
			key = KEYPAD_5;
			break;
		case '6':
			key = KEYPAD_6;
			break;
		case '7':
			key = KEYPAD_7;
			break;
		case '8':
			key = KEYPAD_8;
			break;
		case '9':
			key = KEYPAD_9;
			break;
		case '.':
			key = KEYPAD_OK;
			break;
		case 'x':
			key = KEYPAD_C;
			break;
		default:
			continue;
		}
		switch (e.type) {
		case WSCONS_EVENT_KEY_DOWN:
			keyState[key] = 1;
			latestKeyDown = key;
			break;
		case WSCONS_EVENT_KEY_UP:
			keyState[key] = 0;
			latestKeyUp = key;
			break;
		}
	}
	
}

U32 IsKeyDown_Internal_(U32 key) {
	ProcessKeyPad();
	return keyState[key];
}

U32 LatestKeyUp_Internal_() {
	U32 ret;
	ProcessKeyPad();
	ret = latestKeyUp;
	latestKeyUp = 0xffffffff;
	return ret;
}

U32 LatestKeyDown_Internal_() {
	U32 ret;
	ProcessKeyPad();
	ret = latestKeyDown;
	latestKeyDown = 0xffffffff;
	return ret;
}

#endif
