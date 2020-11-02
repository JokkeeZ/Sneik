using Microsoft.Xna.Framework.Graphics;

namespace Sneik.GameObjects
{
	interface IGameObject
	{
		int Size { get; }

		void Draw(SpriteBatch spriteBatch);
	}
}