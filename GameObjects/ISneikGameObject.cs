using Microsoft.Xna.Framework.Graphics;

namespace Sneik.GameObjects
{
	interface ISneikGameObject
	{
		bool IsAlive { get; set; }

		(int X, int Y) Position { get; set; }

		int Size { get; }

		void Draw(SpriteBatch spriteBatch);
	}
}