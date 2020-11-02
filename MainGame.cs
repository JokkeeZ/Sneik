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
		private bool drawGrid;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferHeight = Constants.WINDOW_SIZE;
			graphics.PreferredBackBufferWidth = Constants.WINDOW_SIZE;

			IsMouseVisible = true;

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

			snake.SetupTexture(GraphicsDevice);
			apple.SetupTexture(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			var state = KeyboardExtension.GetState();

			if (KeyboardExtension.IsKeyPress(Keys.Escape))
			{
				Exit();
				return;
			}

			if (!started && KeyboardExtension.IsKeyPress(Keys.Enter))
			{
				started = true;
			}

			if (KeyboardExtension.IsKeyPress(Keys.G))
			{
				drawGrid = !drawGrid;
			}

			snake.Update(gameTime);

			if (!snake.IsAlive && KeyboardExtension.IsKeyPress(Keys.Enter))
			{
				snake.ResetSnake();
				score = 0;
			}

			snake.HandleUserInput(state);

			if (snake.CollisionWithApple(apple))
			{
				apple.ResetToRandomPosition(snake.Tail);
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
				else
				{
					if (drawGrid)
					{
						spriteBatch.DrawGrid();
					}
				}
			}
			else
			{
				spriteBatch.DrawCenteredStringWithOffset(gameOverFont, "Press Enter to start the game.", Color.Green, (0, -100));
				spriteBatch.DrawCenteredStringWithOffset(scoreFont, string.Join("\n", new[]
				{
					"           Keybindings",
					"-------------------------------------",
					">  T           : Toggle Snake Grid",
					">  G           : Toggle Area Grid",
					">  Arrow Up    : Move Up",
					">  Arrow Down  : Move Down",
					">  Arrow Left  : Move Left",
					">  Arrow Right : Move Right",
					">  Enter       : Start The Game"
				}), Color.White, (0, 70));
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}