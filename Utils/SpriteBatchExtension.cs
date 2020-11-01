using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sneik.Utils
{
	static class SpriteBatchExtension
	{
		private static Vector2 GetCenterPosition(this SpriteFont font, string text, int offsetX, int offsetY)
		{
			var strSize = font.MeasureString(text);

			return new Vector2(
				Constants.WINDOW_SIZE / 2 - strSize.X / 2 + offsetX,
				Constants.WINDOW_SIZE / 2 - strSize.Y / 2 + offsetY
			);
		}

		public static void DrawCenteredString(this SpriteBatch spriteBatch, SpriteFont font, string text, Color color, int offsetX = 0, int offsetY = 0)
		{
			var textPosition = font.GetCenterPosition(text, offsetX, offsetY);
			spriteBatch.DrawString(font, text, textPosition, color);
		}

		public static void DrawCenteredStringWithOffset(this SpriteBatch spriteBatch, SpriteFont font, string text, Color color, int offsetX, int offsetY)
		{
			spriteBatch.DrawCenteredString(font, text, color, offsetX, offsetY);
		}
	}
}