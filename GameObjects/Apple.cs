using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sneik.Utils;

namespace Sneik.GameObjects
{
	class Apple : IGameObject
	{
		private readonly Random random;

		public bool IsAlive { get; set; }

		private (int X, int Y) position;

		public int Size => Constants.RECT_SIZE;

		public Texture2D Texture { get; set; }

		public Apple() => random = new Random();

		public void SetupTexture(SpriteBatch spriteBatch)
		{
			Texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			Texture.SetData(new[] { Color.White });
		}

		public (int X, int Y) GetPosition() => position;

		public void ResetToRandomPosition()
		{
			position = (
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

			spriteBatch.Draw(Texture, new Rectangle(position.X, position.Y, Size, Size), Color.Red);
		}
	}
}