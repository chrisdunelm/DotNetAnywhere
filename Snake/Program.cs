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
					snakePit.AddObsticle();
				}
			}
			snakePit.Msg(crashType.ToString());
			Thread.Sleep(5000);

			return true;
		}
	}
}
