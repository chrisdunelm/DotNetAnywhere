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
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace CustomDevice {
	class WindowsScreen : Form {

		internal static WindowsScreen winScreen = null;

		public static WindowsScreen WinScreen {
			get {
				if (winScreen == null) {
					using (Graphics g = DeviceGraphics.GetScreen()) {
					}
				}
				return winScreen;
			}
		}

		public WindowsScreen(int width, int height) {
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.FixedWidth, true);
			this.SetStyle(ControlStyles.FixedHeight, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.UserPaint, true);

			this.formSurface = new Bitmap(width, height);

			this.ClientSize = new Size(width, height);

			this.Show();
			this.Activate();

		}

		private Bitmap formSurface;

		public Bitmap FormSurface {
			get {
				return this.formSurface;
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			lock (this) {
				e.Graphics.DrawImageUnscaled(this.formSurface, new Point());
			}
		}

		internal static void WindowsMessagePump(object state) {
			WindowsScreen.winScreen = new WindowsScreen(DeviceGraphics.ScreenXSize, DeviceGraphics.ScreenYSize);
			for (; ; ) {
				Application.DoEvents();
				Thread.Sleep(40);
				WindowsScreen.winScreen.Refresh();
			}
		}

		private bool[] keyState = new bool[12];

		private KeyPadKey MapKey(Keys pcKey, out bool isValid) {
			isValid = true;
			switch (pcKey) {
				case Keys.D0:
				case Keys.NumPad0:
					return KeyPadKey.B0;
				case Keys.D1:
				case Keys.NumPad1:
					return KeyPadKey.B1;
				case Keys.D2:
				case Keys.NumPad2:
					return KeyPadKey.B2;
				case Keys.D3:
				case Keys.NumPad3:
					return KeyPadKey.B3;
				case Keys.D4:
				case Keys.NumPad4:
					return KeyPadKey.B4;
				case Keys.D5:
				case Keys.NumPad5:
					return KeyPadKey.B5;
				case Keys.D6:
				case Keys.NumPad6:
					return KeyPadKey.B6;
				case Keys.D7:
				case Keys.NumPad7:
					return KeyPadKey.B7;
				case Keys.D8:
				case Keys.NumPad8:
					return KeyPadKey.B8;
				case Keys.D9:
				case Keys.NumPad9:
					return KeyPadKey.B9;
				case Keys.Enter:
					return KeyPadKey.OK;
				case Keys.Escape:
					return KeyPadKey.C;
				default:
					isValid = false;
					return KeyPadKey.B0;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			bool isValid;
			KeyPadKey key = this.MapKey(e.KeyCode, out isValid);
			if (isValid) {
				e.Handled = true;
				this.keyState[(int)key] = true;
				this.latestKeyDown = key;
				return;
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyUp(KeyEventArgs e) {
			bool isValid;
			KeyPadKey key = this.MapKey(e.KeyCode, out isValid);
			if (isValid) {
				e.Handled = true;
				this.keyState[(int)key] = false;
				this.latestKeyUp = key;
				return;
			}
			base.OnKeyUp(e);
		}

		public bool IsKeyDown(KeyPadKey key) {
			return this.keyState[(int)key];
		}

		private KeyPadKey? latestKeyUp = null;
		public bool LatestKeyUp(out KeyPadKey key) {
			if (this.latestKeyUp.HasValue) {
				key = this.latestKeyUp.Value;
				this.latestKeyUp = null;
				return true;
			} else {
				key = KeyPadKey.B0;
				return false;
			}
		}

		private KeyPadKey? latestKeyDown = null;
		public bool LatestKeyDown(out KeyPadKey key) {
			if (this.latestKeyDown.HasValue) {
				key = this.latestKeyDown.Value;
				this.latestKeyDown = null;
				return true;
			} else {
				key = KeyPadKey.B0;
				return false;
			}
		}
	}
}
