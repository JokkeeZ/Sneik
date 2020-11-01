using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sneik.Utils
{
	static class SpriteBatchExtension
	{
		private static Vector2 GetCenterPosition(this SpriteFont font, string text, (int x, int y) offset)
		{
			var strSize = font.MeasureString(text);

			return new Vector2(
				Constants.WINDOW_SIZE / 2 - strSize.X / 2 + offset.x,
				Constants.WINDOW_SIZE / 2 - strSize.Y / 2 + offset.y
			);
		}

		public static void DrawCenteredString(this SpriteBatch spriteBatch, SpriteFont font, string text, Color color)
		{
			var textPosition = font.GetCenterPosition(text, (0, 0));
			spriteBatch.DrawString(font, text, textPosition, color);
		}

		public static void DrawCenteredStringWithOffset(this SpriteBatch spriteBatch, SpriteFont font, string text, Color color, (int x, int y) offset)
		{
			var textPosition = font.GetCenterPosition(text, offset);
			spriteBatch.DrawString(font, text, textPosition, color);
		}
	}
}