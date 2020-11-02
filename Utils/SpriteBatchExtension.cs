using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sneik.Utils
{
	static class SpriteBatchExtension
	{
		private static Texture2D texture;

		private static Vector2 GetWindowCenter(this SpriteFont font, string text, (int x, int y) textOffset)
		{
			var strSize = font.MeasureString(text);

			return new Vector2(
				Constants.WINDOW_SIZE / 2 - strSize.X / 2 + textOffset.x,
				Constants.WINDOW_SIZE / 2 - strSize.Y / 2 + textOffset.y
			);
		}

		public static void DrawCenteredString(this SpriteBatch spriteBatch, SpriteFont font, string text, Color color)
		{
			var textPosition = font.GetWindowCenter(text, (0, 0));
			spriteBatch.DrawString(font, text, textPosition, color);
		}

		public static void DrawCenteredStringWithOffset(this SpriteBatch spriteBatch, SpriteFont font, string text, Color color, (int x, int y) offset)
		{
			var textPosition = font.GetWindowCenter(text, offset);
			spriteBatch.DrawString(font, text, textPosition, color);
		}

		private static Texture2D GetTexture(GraphicsDevice graphics)
		{
			if (texture == null)
			{
				texture = new Texture2D(graphics, 1, 1);
				texture.SetData(new[] { Color.White });
			}

			return texture;
		}

		private static void DrawLine(this SpriteBatch spriteBatch, Vector2 p1, Vector2 p2)
		{
			var angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
			var dis = Vector2.Distance(p1, p2);

			var texture = GetTexture(spriteBatch.GraphicsDevice);
			spriteBatch.Draw(texture, p1, null, Color.DarkSlateGray, angle, new Vector2(0f, 0.5f), new Vector2(dis, 1f), SpriteEffects.None, 0);
		}

		public static void DrawGrid(this SpriteBatch spriteBatch)
		{
			for (var i = 0; i < Constants.WINDOW_SIZE / Constants.RECT_SIZE; ++i)
			{
				spriteBatch.DrawLine(new Vector2(0f, i * Constants.RECT_SIZE), new Vector2(Constants.WINDOW_SIZE, i * Constants.RECT_SIZE));
				spriteBatch.DrawLine(new Vector2(i * Constants.RECT_SIZE, 0f), new Vector2(i * Constants.RECT_SIZE, Constants.WINDOW_SIZE));
			}
		}
	}
}