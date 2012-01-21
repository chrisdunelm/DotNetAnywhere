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

namespace Snake {
	class ASnake {

		private SnakePit snakePit;
		private Queue<Point> bodyParts = new Queue<Point>();
		private Point head;
		private int xSpeed, ySpeed;
		private Color col;
		private int increaseBy;

		public ASnake(SnakePit snakePit, int startLength, Point startPosition, Color col) {
			this.snakePit = snakePit;
			for (int i = 0; i < startLength; i++) {
				Point cell = new Point(startPosition.X - startLength + i + 1, startPosition.Y);
				snakePit.SetCell(cell, col);
				this.bodyParts.Enqueue(cell);
				this.head = cell;
			}
			this.col = col;
			this.xSpeed = 1;
			this.ySpeed = 0;
			this.increaseBy = 0;
			snakePit.AddSnake(this);
		}

		public CrashType Move() {
			if (this.increaseBy == 0) {
				Point tail = this.bodyParts.Dequeue();
				snakePit.ClearCell(tail);
			} else {
				this.increaseBy--;
			}
			this.head.X += this.xSpeed;
			this.head.Y += this.ySpeed;
			if (this.DoesIntersect(this.head)) {
				Console.WriteLine("!!!SELF CRASH!!!");
				return CrashType.Self;
			}
			this.bodyParts.Enqueue(this.head);
			snakePit.SetCell(this.head, this.col);
			CrashType crash = this.snakePit.IsCrash(head);
			return crash;
		}

		public void DrawHead() {
			snakePit.SetCell(this.head, this.col);
		}

		public void SetDirection(int x, int y) {
			// Do nothing if trying to set current or opposite direction
			if (x != 0 && this.xSpeed != 0) {
				return;
			}
			if (y != 0 && this.ySpeed != 0) {
				return;
			}
			// Set new speeds
			this.xSpeed = x;
			this.ySpeed = y;
		}

		public bool DoesIntersect(IEnumerable<Point> cells) {
			foreach (Point cell in cells) {
				if (this.DoesIntersect(cell)) {
					return true;
				}
			}
			return false;
		}

		public bool DoesIntersect(Point cell) {
			foreach (Point p in this.bodyParts) {
				if (p == cell) {
					return true;
				}
			}
			return false;
		}

		public void IncreaseLength(int amount) {
			this.increaseBy += amount;
		}

		public Point Head {
			get {
				return this.head;
			}
		}

	}
}
