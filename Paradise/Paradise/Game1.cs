using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Paradise
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		#region Globals

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private int x = 600;
		private int y = 400;
		private Texture2D goldblock, blueblock, grenblock, grayblock, pinkblock;
		private KeyboardState currentKeyboardState;
		private KeyboardState lastKeyboardState;
		private GamePadState currentGamePadState;
		private GamePadState lastGamePadState;
		private SpriteFont font;
		public Dictionary<Block, int> blockposition = new Dictionary<Block, int>();
		private string DrawText;
		private bool BlockPickedUp = false;
		private Block PickedUpBlock = null;
		private const int ScreenHeight = 800, ScreenWidth = 1200;

		#endregion

		#region Properties

		private List<Block> blockList = new List<Block>();

		public List<Block> BlockList
		{
			get { return blockList; }
			set { blockList = value; }
		}

		private bool IsPressed(Keys key, Buttons button)
		{
			return (currentKeyboardState.IsKeyDown(key) &&
			        lastKeyboardState.IsKeyUp(key)) ||
			       (currentGamePadState.IsButtonDown(button) &&
			        lastGamePadState.IsButtonUp(button));
		}

		#endregion


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.IsMouseVisible = true;

			// Screensize
			graphics.PreferredBackBufferHeight = ScreenHeight;
			graphics.PreferredBackBufferWidth = ScreenWidth;

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("SpriteFont1");
			grayblock = Content.Load<Texture2D>("greyblock");
			pinkblock = Content.Load<Texture2D>("pinkblock");
			goldblock = Content.Load<Texture2D>("goldblock");
			blueblock = Content.Load<Texture2D>("blueblock");
			grenblock = Content.Load<Texture2D>("grenblock");

			// TODO: use this.Content to load your game content here

			//Draw the background

			for (int ii = 690; ii < 700; ii = ii + 10)
			{
				for (int i = 40; i < 700; i = i + 21)
				{
					Block myobj = new Block();
					myobj.blcolor = 1;
					myobj.x = i;
					myobj.y = ii;
					myobj.exists = true;
					BlockList.Add(myobj);
				}
			}

			for (int ii = 10; ii < 700; ii = ii + 10)
			{
				for (int i = 800; i < 810; i = i + 21)
				{
					Block myobj = new Block();
					myobj.blcolor = 3;
					myobj.x = i;
					myobj.y = ii;
					myobj.exists = true;
					BlockList.Add(myobj);
				}
			}
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			grayblock.Dispose();
			pinkblock.Dispose();
			goldblock.Dispose();
			blueblock.Dispose();
			grenblock.Dispose();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here







			//Gravity
			foreach (Block myblock in BlockList)
			{

				if ((myblock.y + 8) < ScreenHeight - 100)
				{
					bool touching = false;

					foreach (Block gblock in BlockList)
					{
						for (int i = 1; i < 19; i++)
						{
							if ((myblock.y + 10 < (gblock.y + 8)) && ((myblock.y + 10 > (gblock.y))) &&
									((myblock.x + i) >= gblock.x && (myblock.x + i) < (gblock.x + 20)))
							{
								touching = true;
								break;
							}
						}
					}

					if (!touching)
						myblock.y++;
				}
			}



      MouseState mouse = Mouse.GetState();
			currentKeyboardState = Keyboard.GetState();

	currentKeyboardState = Keyboard.GetState();

			if (mouse.LeftButton == ButtonState.Pressed)
			{
				//pick up any brick
				if (!BlockPickedUp)
					PickupBrick(mouse.X, mouse.Y);
			}

			if (mouse.LeftButton == ButtonState.Released)
			{
				//drop up any brick
				BlockPickedUp = false;
				PickedUpBlock = null;
			}

			if (mouse.RightButton == ButtonState.Pressed)
			{
				Block myobj = new Block();
				myobj.blcolor = 4;
				myobj.x = mouse.X;
				myobj.y = mouse.Y;
				myobj.exists = true;
				BlockList.Add(myobj);
			}

			if (IsPressed(Keys.Left, Buttons.A))
			{
			}

			if (BlockPickedUp)
			{
				PickedUpBlock.x = mouse.X - 10;
				PickedUpBlock.y = mouse.Y - 3;
			}


			if (IsPressed(Keys.Right, Buttons.A))
			{
			}

			if (IsPressed(Keys.Up, Buttons.A))
			{
			}

			if (IsPressed(Keys.Down, Buttons.A))
			{
			}

			if (IsPressed(Keys.Escape, Buttons.Back))
			{
				Exit();
			}


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			graphics.GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			if (BlockList != null)
			{
				foreach (Block myblock in BlockList)
				{

					if (!myblock.exists)
						continue;

					Vector2 pos = new Vector2(myblock.x, myblock.y);

					if (myblock.blcolor == 1)
						spriteBatch.Draw(grayblock, pos, Color.White);

					if (myblock.blcolor == 2)
						spriteBatch.Draw(pinkblock, pos, Color.White);

					if (myblock.blcolor == 3)
						spriteBatch.Draw(goldblock, pos, Color.White);

					if (myblock.blcolor == 4)
						spriteBatch.Draw(blueblock, pos, Color.White);

					if (myblock.blcolor == 5)
						spriteBatch.Draw(grenblock, pos, Color.White);
				}
			}

			spriteBatch.End();
			base.Draw(gameTime);
		}

		private void PickupBrick(int xpos, int ypos)
		{
			//// checks if block exists at click position.
			foreach (Block myblock in BlockList)
			{
				if (!myblock.exists)
					continue;

				if ((ypos < (myblock.y + 8)) && ((ypos > (myblock.y))) && ((xpos + 1) >= myblock.x && (xpos + 1) < (myblock.x + 21)))
				{
					BlockPickedUp = true;
					PickedUpBlock = myblock;
				}
			}
		}
}
	

#region Other Classes
	public class Missle
	{
		public int x;
		public int y = 500;
		public bool isNew = true;
		public bool used = false;
	}

	public class Block
	{
		public int x;
		public int y;
		public int blcolor = 1;
		public bool exists = false;
	}
	#endregion
}

