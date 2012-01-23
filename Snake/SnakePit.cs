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
using CustomDevice;
using System.Drawing.Drawing2D;

namespace Snake {
	class SnakePit {

		private const int cellSize = 8;
		private const int scoreBoardHeight = 48;

		private Graphics screen;
		private int cellOfsX, cellOfsY;
		private int numCellsX, numCellsY;
		private Random rnd;
		private Point[] food;
		private List<ASnake> snakes = new List<ASnake>();
		private int score, level;
		private List<Point[]> obstacles = new List<Point[]>();

		public SnakePit() {
			this.screen = DeviceGraphics.GetScreen();
			this.screen.Clear(Color.White);
			this.numCellsX = (DeviceGraphics.ScreenXSize / cellSize) - 2;
			this.numCellsY = ((DeviceGraphics.ScreenYSize - scoreBoardHeight) / cellSize) - 2;
			this.cellOfsX = cellSize;
			this.cellOfsY = cellSize;
			this.rnd = new Random();
			this.food = null;
			this.score = 0;
			this.level = 1;

			using (Brush brush = new HatchBrush(HatchStyle.DiagonalCross, Color.Black, Color.White)) {
				this.screen.FillRectangle(brush, 0, 0, DeviceGraphics.ScreenXSize, cellSize);
				this.screen.FillRectangle(brush, 0, cellSize, cellSize, this.numCellsY * cellSize);
				this.screen.FillRectangle(brush, (1 + this.numCellsX) * cellSize, cellSize, cellSize, this.numCellsY * cellSize);
				this.screen.FillRectangle(brush, 0, (1 + this.numCellsY) * cellSize, DeviceGraphics.ScreenXSize, cellSize);
			}
			this.screen.DrawRectangle(Pens.Black, cellSize - 1, cellSize - 1,
				this.numCellsX * cellSize + 1, this.numCellsY * cellSize + 1);

			using (Font f = new Font("tahoma", 15)) {
				using (StringFormat sf = new StringFormat()) {
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					this.screen.DrawString("<", f, Brushes.Black, new RectangleF(0, 220, 64, 20), sf);
					this.screen.DrawString("v", f, Brushes.Black, new RectangleF(64, 220, 64, 20), sf);
					this.screen.DrawString("^", f, Brushes.Black, new RectangleF(128, 220, 64, 20), sf);
					this.screen.DrawString(">", f, Brushes.Black, new RectangleF(192, 220, 64, 20), sf);
				}
			}

			this.ShowScore();
		}

		private void FillCell(Point cell, Color col) {
			using (Brush brush = new SolidBrush(col)) {
				screen.FillRectangle(brush,
					this.cellOfsX + cell.X * cellSize, this.cellOfsY + cell.Y * cellSize, cellSize, cellSize);
			}
		}

		public void FillAndOutlineCells(IEnumerable<Point> points, Color fillCol, Color outlineCol) {
			using (Brush brush = new SolidBrush(fillCol)) {
				this.FillAndOutlineCells(points, brush, outlineCol);
			}
		}

		public void FillAndOutlineCells(IEnumerable<Point> points, Brush fillBrush, Color outlineCol) {
			int minX = int.MaxValue, maxX = int.MinValue;
			int minY = int.MaxValue, maxY = int.MinValue;
			foreach (Point p in points) {
				minX = Math.Min(minX, p.X);
				maxX = Math.Max(maxX, p.X);
				minY = Math.Min(minY, p.Y);
				maxY = Math.Max(maxY, p.Y);
			}
			int x = this.cellOfsX + minX * cellSize;
			int y = this.cellOfsY + minY * cellSize;
			int width = (maxX - minX + 1) * cellSize;
			int height = (maxY - minY + 1) * cellSize;
			this.screen.FillRectangle(fillBrush, x, y, width, height);
			using (Pen pen = new Pen(outlineCol)) {
				this.screen.DrawRectangle(pen, x, y, width - 1, height - 1);
			}
		}

		public void AddSnake(ASnake snake) {
			this.snakes.Add(snake);
		}

		public Point Centre {
			get {
				return new Point(numCellsX >> 1, numCellsY >> 1);
			}
		}

		public Size Size {
			get {
				return new Size(numCellsX, numCellsY);
			}
		}

		public void SetCell(Point cell, Color col) {
			this.FillCell(cell, col);
		}

		public void ClearCell(Point cell) {
			this.FillCell(cell, Color.White);
		}

		public bool IsCrashObstacle(Point cell) {
			foreach (Point[] obs in this.obstacles) {
				foreach (Point pt in obs) {
					if (pt == cell) {
						return true;
					}
				}
			}
			return false;
		}

		public CrashType IsCrash(Point cell) {
			if (cell.X < 0 || cell.X >= this.numCellsX || cell.Y < 0 || cell.Y >= this.numCellsY) {
				return CrashType.Wall;
			}
			if (this.IsCrashObstacle(cell)) {
				return CrashType.Wall;
			}
			if (this.food != null) {
				for (int i = 0; i < this.food.Length; i++) {
					if (this.food[i] == cell) {
						return CrashType.Food;
					}
				}
			}
			return CrashType.None;
		}

		public void CreateFood(bool canMoveFood) {
			if (this.food != null && !canMoveFood) {
				// Don't move the food
				return;
			}
			this.RemoveFood();
			Point[] newFood;
			for (; ; ) {
				int x = this.rnd.Next(this.numCellsX - 1);
				int y = this.rnd.Next(this.numCellsY - 1);
				newFood = new Point[4];
				newFood[0] = new Point(x, y);
				newFood[1] = new Point(x + 1, y);
				newFood[2] = new Point(x, y + 1);
				newFood[3] = new Point(x + 1, y + 1);
				bool ok = true;
				foreach (ASnake snake in this.snakes) {
					if (snake.DoesIntersect(newFood)) {
						ok = false;
						break;
					}
				}
				if (ok) {
					foreach (Point pt in newFood) {
						if (IsCrashObstacle(pt)) {
							ok = false;
							break;
						}
					}
				}
				if (ok) {
					break;
				}
			}
			this.food = newFood;
			this.FillAndOutlineCells(this.food, Color.Gray, Color.Black);
		}

		public void RemoveFood() {
			if (this.food == null) {
				return;
			}
			for (int i = 0; i < this.food.Length; i++) {
				this.ClearCell(this.food[i]);
			}
			this.food = null;
		}

		public bool EatFood() {
			this.RemoveFood();
			this.score++;
			bool ret = false;
			if (this.score >= this.level * 4) {
				this.level++;
				ret = true;
			}
			this.ShowScore();
			return ret;
		}

		public void Msg(string msg) {
			using (Font f = new Font("tahoma", 40)) {
				this.screen.DrawString(msg, f, Brushes.Black, 30, 80);
			}
		}

		public void ShowScore() {
			this.screen.FillRectangle(Brushes.White, 0,
				DeviceGraphics.ScreenYSize - scoreBoardHeight, DeviceGraphics.ScreenXSize, scoreBoardHeight - 20);
			using (Font f = new Font("tahoma", 15)) {
				string s = string.Format("Level: {0}", this.level);
				this.screen.DrawString(s, f, Brushes.Black, 0, DeviceGraphics.ScreenYSize - scoreBoardHeight);
				s = string.Format("Score: {0}", this.score);
				this.screen.DrawString(s, f, Brushes.Black, DeviceGraphics.ScreenXSize >> 1, DeviceGraphics.ScreenYSize - scoreBoardHeight);
			}
		}

		public int Level {
			get {
				return this.level;
			}
		}

		private int Dist(Point a, Point b) {
			int dx = a.X - b.Y;
			int dy = a.Y - b.Y;
			return Math.Max(Math.Abs(dx), Math.Abs(dy));
		}

		public void AddObstacle() {
			Point[] obs;
			for (; ; ) {
				int x = this.rnd.Next(this.numCellsX - 1);
				int y = this.rnd.Next(this.numCellsY - 1);
				obs = new Point[4];
				obs[0] = new Point(x, y);
				obs[1] = new Point(x + 1, y);
				obs[2] = new Point(x, y + 1);
				obs[3] = new Point(x + 1, y + 1);
				bool ok = true;
				foreach (ASnake snake in this.snakes) {
					if (snake.DoesIntersect(obs) || this.Dist(snake.Head, obs[0]) < 10) {
						ok = false;
						break;
					}
				}
				if (ok) {
					break;
				}
			}
			using (HatchBrush brush = new HatchBrush(HatchStyle.DiagonalCross,Color.Black, Color.White)) {
				this.FillAndOutlineCells(obs, brush, Color.Black);
			}
			this.obstacles.Add(obs);
			if (this.obstacles.Count > this.Level * 2) {
				// Remove an obstacle
				obs = this.obstacles[0];
				foreach (Point pt in obs) {
					this.ClearCell(pt);
				}
				this.obstacles.RemoveAt(0);
			}
		}
	}
}
