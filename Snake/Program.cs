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
using CustomDevice;

namespace Snake {
	class Program {
		static void Main(string[] args) {
			for (; ; ) {
				bool res = Play();
				if (!res) {
					break;
				}
			}
		}

		static bool Play() {

			Font f = new Font("tahoma", 25);
			SnakePit snakePit = new SnakePit();
			Random rnd = new Random();
			int delay = 300;

			ASnake snake = new ASnake(snakePit, 5, snakePit.Centre, Color.Black);

			CrashType crashType;
			for (; ; ) {
				Thread.Sleep(delay);
				KeyPadKey key;
				if (KeyPad.LatestKeyDown(out key)) {
					switch (key) {
					case KeyPadKey.B6: // Left
						snake.SetDirection(-1, 0);
						break;
					case KeyPadKey.B7: // Down
						snake.SetDirection(0, 1);
						break;
					case KeyPadKey.B8: // Up
						snake.SetDirection(0, -1);
						break;
					case KeyPadKey.B9: // Right
						snake.SetDirection(1, 0);
						break;
					case KeyPadKey.C:
						return false;
					}
				}
				crashType = snake.Move();
				bool finish = false;
				switch (crashType) {
				case CrashType.None:
					break;
				case CrashType.Self:
				case CrashType.Wall:
				default:
					finish = true;
					break;
				case CrashType.Food:
					bool levelUp = snakePit.EatFood();
					if (levelUp) {
						delay = (delay * 3) / 4;
					}
					snake.IncreaseLength(4);
					break;

				}
				snake.DrawHead();
				if (finish) {
					break;
				}

				int r = rnd.Next(1000);
				if (r < 50) {
					snakePit.CreateFood(r < 5);
				}
				r = rnd.Next(1000);
				if (r < 20) {
					snakePit.AddObstacle();
				}
			}
			snakePit.Msg(crashType.ToString());
			Thread.Sleep(5000);

			return true;
		}
	}
}
