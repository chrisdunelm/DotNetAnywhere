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

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

namespace CustomDevice {

	public static class DeviceGraphics {

		private const int ScreenX = 320;
		private const int ScreenY = 240;

		private static int whichScreen = 0;

		public static int ScreenXSize {
			get {
				return ScreenX;
			}
		}

		public static int ScreenYSize {
			get {
				return ScreenY;
			}
		}

		public static Graphics GetScreen() {
			if (whichScreen == 0) {
				if (Utils.IsInterNet2) {
					whichScreen = 1;
				} else {
					whichScreen = 2;
				}
			}
			if (whichScreen == 1) {
				return GetScreen_InterNet2();
			} else {
				return GetScreen_Windows();
			}
		}

		private static Graphics GetScreen_InterNet2() {
			// In the InterNet2 System.Drawing dll Graphics.FromHdc() will always
			// return the Graphics object for the screen.
			return Graphics.FromHdc(IntPtr.Zero);
		}

		private static Graphics GetScreen_Windows() {

			if (WindowsScreen.winScreen == null) {
				Thread t = new Thread(WindowsScreen.WindowsMessagePump);
				t.IsBackground = true;
				t.Start();
				while (WindowsScreen.winScreen == null) {
					Thread.Sleep(2);
				}
			}

			lock (WindowsScreen.winScreen) {
				Graphics g = Graphics.FromImage(WindowsScreen.winScreen.FormSurface);
				return g;
			}
		}

	}

}
