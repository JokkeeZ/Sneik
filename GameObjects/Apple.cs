using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sneik.Utils;

namespace Sneik.GameObjects
{
	class Apple : ISneikGameObject
	{
		private readonly Random random;

		public bool IsAlive { get; set; }

		public (int X, int Y) Position { get; set; }

		public int Size => Constants.RECT_SIZE;

		public Texture2D Texture { get; set; }

		public Apple() => random = new Random();

		public void SetupTexture(SpriteBatch spriteBatch)
		{
			Texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			Texture.SetData(new[] { Color.White });
		}

		public void ResetToRandomPosition()
		{
			Position = (
				random.Next(0, Constants.WINDOW_SIZE / Size) * Size,
				random.Next(0, Constants.WINDOW_SIZE / Size) * Size);

			IsAlive = true;
		}

		public void Update()
		{
			if (!IsAlive)
			{
				ResetToRandomPosition();
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!IsAlive)
			{
				return;
			}

			spriteBatch.Draw(Texture, new Rectangle(Position.X, Position.Y, Size, Size), Color.Red);
		}
	}
}