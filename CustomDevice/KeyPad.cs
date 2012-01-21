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
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

namespace CustomDevice {
	public static class KeyPad {

		[DllImport("libIGraph")]
		extern private static bool IsKeyDown_Internal(KeyPadKey key);

		[DllImport("libIGraph")]
		extern private static int LatestKeyUp_Internal();

		[DllImport("libIGraph")]
		extern private static int LatestKeyDown_Internal();

		public static bool IsKeyDown(KeyPadKey key) {
			if (Utils.IsInterNet2) {
				return IsKeyDown_Internal(key);
			} else {
				return WindowsScreen.WinScreen.IsKeyDown(key);
			}
		}

		private static bool WindowsLatestKeyUp(out KeyPadKey key) {
			return WindowsScreen.WinScreen.LatestKeyUp(out key);
		}

		private static bool WindowsLatestKeyDown(out KeyPadKey key) {
			return WindowsScreen.WinScreen.LatestKeyDown(out key);
		}

		public static bool LatestKeyUp(out KeyPadKey key) {
			if (Utils.IsInterNet2) {
				int iKey = LatestKeyUp_Internal();
				if (iKey >= 0) {
					key = (KeyPadKey)iKey;
					return true;
				} else {
					key = KeyPadKey.B0;
					return false;
				}
			} else {
				return WindowsLatestKeyUp(out key);
			}
		}

		public static bool LatestKeyDown(out KeyPadKey key) {
			if (Utils.IsInterNet2) {
				int iKey = LatestKeyDown_Internal();
				if (iKey >= 0) {
					key = (KeyPadKey)iKey;
					return true;
				} else {
					key = KeyPadKey.B0;
					return false;
				}
			} else {
				return WindowsLatestKeyDown(out key);
			}
		}

		public static KeyPadKey? ReadKey(int timeoutMs) {
			DateTime end = DateTime.UtcNow.AddMilliseconds(timeoutMs);
			do {
				KeyPadKey key;
				if (LatestKeyDown(out key)) {
					return key;
				}
				Thread.Sleep(1);
			} while (DateTime.UtcNow < end && timeoutMs >= 0);
			return null;
		}

		public static KeyPadKey? ReadKey() {
			return ReadKey(-1);
		}
	}
}
