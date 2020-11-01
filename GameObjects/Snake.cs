using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sneik.Utils;

namespace Sneik.GameObjects
{
	class Snake : ISneikGameObject
	{
		const int INTERVAL_MILLISECONDS = 76;

		private int lastUpdate;

		private readonly List<Rectangle> tailParts;

		private (int X, int Y) position;
		private (int X, int Y) direction = (1, 0);

		private bool gridMode = true;

		public (int X, int Y) Position
		{
			get => position;
			set => position = value;
		}

		public int Size => Constants.RECT_SIZE;

		public bool IsAlive { get; set; } = true;

		public Texture2D Texture { get; set; }

		public Snake() => tailParts = new List<Rectangle>
		{
			new Rectangle(position.X, position.Y, Size, Size)
		};

		public void SetupTexture(SpriteBatch spriteBatch)
		{
			Texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			Texture.SetData(new[] { Color.White });
		}

		public bool CanMove(Directions direction) => direction switch
		{
			Directions.Up => tailParts.Count == 1 || (tailParts.Count > 1 && this.direction.Y != 1),
			Directions.Down => tailParts.Count == 1 || (tailParts.Count > 1 && this.direction.Y != -1),
			Directions.Left => tailParts.Count == 1 || (tailParts.Count > 1 && this.direction.X != 1),
			Directions.Right => tailParts.Count == 1 || (tailParts.Count > 1 && this.direction.X != -1),
			_ => false
		};

		public void ResetSnake()
		{
			tailParts.Clear();

			SetStartingPosition();

			ExtendTail();
			IsAlive = true;
		}

		public void HandleUserInput(KeyboardState state)
		{
			var pressedKeyCount = state.GetPressedKeyCount();
			if (pressedKeyCount > 1 || pressedKeyCount == 0)
			{
				return;
			}

			if (SneikKeyboard.IsKeyPress(Keys.T))
			{
				gridMode = !gridMode;
			}

			var (x, y) = direction;
			if (SneikKeyboard.IsKeyPress(Keys.Up) && CanMove(Directions.Up))
			{
				(x, y) = (0, -1);
			}
			else if (SneikKeyboard.IsKeyPress(Keys.Down) && CanMove(Directions.Down))
			{
				(x, y) = (0, 1);
			}
			else if (SneikKeyboard.IsKeyPress(Keys.Left) && CanMove(Directions.Left))
			{
				(x, y) = (-1, 0);
			}
			else if (SneikKeyboard.IsKeyPress(Keys.Right) && CanMove(Directions.Right))
			{
				(x, y) = (1, 0);
			}

			// Trying to move to X and Y directions at the same time
			if ((x, y) == (0, 0) || x == 1 && y == 1 || x == -1 && y == -1)
			{
				return;
			}

			direction = (x, y);
		}

		public bool CollisionWithApple(Apple apple) => Position == apple.Position;

		public void ExtendTail()
		{
			tailParts.Add(new Rectangle(position.X, position.Y, Size, Size));
		}

		public void SetStartingPosition()
		{
			position.X = Constants.WINDOW_SIZE / 2;
			position.Y = Constants.WINDOW_SIZE / 2;
		}

		private bool CollisionWithTailPart(Rectangle tailPart) => position.X == tailPart.X && position.Y == tailPart.Y;

		public void Update(GameTime gameTime)
		{
			lastUpdate += gameTime.ElapsedGameTime.Milliseconds;

			if (IsAlive && lastUpdate >= INTERVAL_MILLISECONDS)
			{
				lastUpdate = 0;

				position.Y += direction.Y * Size;
				position.X += direction.X * Size;

				position.Y = position.Y == -Size ? Constants.WINDOW_SIZE : position.Y == Constants.WINDOW_SIZE ? 0 : position.Y;
				position.X = position.X == -Size ? Constants.WINDOW_SIZE : position.X == Constants.WINDOW_SIZE ? 0 : position.X;

				if (tailParts.Count > 1)
				{
					for (var i = tailParts.Count - 1; i > 0; --i)
					{
						if (CollisionWithTailPart(tailParts[i]))
						{
							IsAlive = false;
						}

						tailParts[i] = new Rectangle(tailParts[i - 1].X, tailParts[i - 1].Y, Size, Size);
					}
				}

				tailParts[0] = new Rectangle(position.X, position.Y, Size, Size);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var part in tailParts)
			{
				if (gridMode)
				{
					spriteBatch.Draw(Texture, new Rectangle(part.X - 1, part.Y - 1, Size + 2, Size + 2), Color.Black);
				}

				spriteBatch.Draw(Texture, part, IsAlive ? Color.ForestGreen : Color.DarkRed);
			}
		}
	}
}