using Microsoft.Xna.Framework.Graphics;

namespace Sneik.GameObjects
{
	interface IGameObject
	{
		bool IsAlive { get; set; }

		(int X, int Y) GetPosition();

		int Size { get; }

		void Draw(SpriteBatch spriteBatch);
	}
}