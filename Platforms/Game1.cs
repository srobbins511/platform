using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platforms
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;
        private Character character;
        private Floor floor;
        private Level level;
        private Level level2;
        private int charX;
        private int charY;
        private int screenWidth;
        private int screenHeight;
        private KeyboardState prevKeyboardState;
        private GameTime gameTime;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            // TODO: use this.Content to load your game content here
            gameContent = new GameContent(Content);
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            if(screenWidth >= 1080)
            {
                screenWidth = 1080;
            }
            if(screenHeight >= 720)
            {
                screenHeight = 720;
            }

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            charX = 300;
            charY = 300;
            character = new Character(gameContent, spriteBatch, charX, charY, screenWidth, screenHeight);
            floor = new Floor(gameContent, spriteBatch, screenWidth, screenHeight);
            level = new Level(gameContent, spriteBatch, screenWidth, screenHeight, floor);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(character.Y>screenHeight)
            {
                Exit();
            }
            if(character.WinLevel)
            {
                Exit();
            }
            
            KeyboardState currentKeyboardState = Keyboard.GetState();
            if(currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D))
                {
                    character.MovingRight();
                }
                else if (currentKeyboardState.IsKeyDown(Keys.A))
                {
                    character.MovingLeft();
                }
                
            }
            if (currentKeyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyDown(Keys.Space))
            {
                character.Jump();
            }
            if(!(currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.A)))
            {
                character.ResetVectorX();
            }
            character.Move(floor, level);
            if(!character.stable)
            {
                character.ResetVectorY();
            }

            foreach (Land l in floor.floor)
            {
                l.X = l.X - 1;
                l.rect = new Rectangle((int)l.X, (int)l.Y, (int)l.Width, (int)l.Height);
            }
            foreach (Tile t in level.level)
            {
                t.X = t.X - 1;
                t.rect = new Rectangle((int)t.X, (int)t.Y, (int)t.Width, (int)t.Height);
            }
            if(character.resetFloor)
            {
                foreach (Land l in floor.floor)
                {
                    l.X = level.level[level.platformNumber-1].X;
                    l.rect = new Rectangle((int)l.X, (int)l.Y, (int)l.Width, (int)l.Height);
                }
            }
            character.X -= 1;
            prevKeyboardState = currentKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            floor.Draw();
            level.Draw();
            character.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
