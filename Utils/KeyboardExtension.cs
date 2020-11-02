using Microsoft.Xna.Framework.Input;

namespace Sneik.Utils
{
	static class KeyboardExtension
	{
		static KeyboardState previousState;
		static KeyboardState currentState;

		public static KeyboardState GetState()
		{
			previousState = currentState;
			currentState = Keyboard.GetState();

			return currentState;
		}

		public static bool PreviouslyKeyUp(Keys key) => previousState.IsKeyUp(key);

		public static bool IsKeyPress(Keys key) => currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
	}
}