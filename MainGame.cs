using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sneik.GameObjects;
using Sneik.Utils;

namespace Sneik
{
	class MainGame : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private readonly Snake snake;
		private readonly Apple apple;

		private SpriteFont scoreFont;
		private SpriteFont gameOverFont;

		private bool started;
		private int score;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferHeight = Constants.WINDOW_SIZE;
			graphics.PreferredBackBufferWidth = Constants.WINDOW_SIZE;

			snake = new Snake();
			apple = new Apple();
		}

		protected override void Initialize()
		{
			snake.SetStartingPosition();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			scoreFont = Content.Load<SpriteFont>("ScoreFont");
			gameOverFont = Content.Load<SpriteFont>("GameOverFont");

			snake.SetupTexture(spriteBatch);
			apple.SetupTexture(spriteBatch);
		}

		protected override void Update(GameTime gameTime)
		{
			var state = SneikKeyboard.GetState();

			if (SneikKeyboard.IsKeyPress(Keys.Escape))
			{
				Exit();
				return;
			}

			if (!started && SneikKeyboard.IsKeyPress(Keys.Enter))
			{
				started = true;
			}

			snake.Update(gameTime);

			if (!snake.IsAlive && SneikKeyboard.IsKeyPress(Keys.Enter))
			{
				snake.ResetSnake();
				score = 0;
			}

			apple.Update();

			snake.HandleUserInput(state);

			if (snake.CollisionWithApple(apple))
			{
				apple.ResetToRandomPosition();
				snake.ExtendTail();

				score++;
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			if (started)
			{
				snake.Draw(spriteBatch);
				apple.Draw(spriteBatch);

				spriteBatch.DrawString(scoreFont, $"Score: {score}", new Vector2(10, 10), Color.White);

				if (!snake.IsAlive)
				{
					spriteBatch.DrawCenteredString(gameOverFont, $" ---> Game over! <---\nPress Enter to restart.", Color.White);
				}
			}
			else
			{
				spriteBatch.DrawCenteredStringWithOffset(gameOverFont, "Press Enter to start the game.", Color.Green, (0, -100));
				spriteBatch.DrawCenteredStringWithOffset(scoreFont, string.Join("\n", new[]
				{
					"           Keybindings",
					"-------------------------------------",
					">  T           : Toggle Snake Grid Mode",
					">  Arrow Up    : Move up",
					">  Arrow Down  : Move down",
					">  Arrow Left  : Move left",
					">  Arrow Right : Move right",
					">  Enter       : Start the game"
				}), Color.White, (0, 70));
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}