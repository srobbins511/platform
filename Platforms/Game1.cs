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
        private Button startButton;
        private Button exitButton;
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private DrawMouse mouse;
        private bool isPlaying = false;
        private bool isStarted = false;
        private Level level;
        private Level level2;
        private Level level3;
        private Level level4;
        private Level level5;
        private Level[] levels;
        private int charX;
        private int charY;
        public int screenWidth;
        public int screenHeight;
        private KeyboardState prevKeyboardState;
        private GameTime gameTime;
        private int curLevel = 0;
        private LevelCounter levCounter;

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
            startButtonPosition = new Vector2((screenWidth / 2), (screenHeight / 10) * 5);
            exitButtonPosition = new Vector2((screenWidth / 2) , (screenHeight / 10) * 6);
            startButton = new Button(this, spriteBatch, gameContent, "Start", screenHeight / 10, screenWidth / 5, startButtonPosition);
            exitButton = new Button(this, spriteBatch, gameContent, "Exit", screenHeight / 10, screenWidth / 5, exitButtonPosition);
            isPlaying = false;
            level = new Level(gameContent, spriteBatch, screenWidth, screenHeight, floor, 1);
            level2 = new Level(gameContent, spriteBatch, screenWidth, screenHeight, level, 2);
            level3 = new Level(gameContent, spriteBatch, screenWidth, screenHeight, level2, 3);
            level4 = new Level(gameContent, spriteBatch, screenWidth, screenHeight, level3, 4);
            level5 = new Level(gameContent, spriteBatch, screenWidth, screenHeight, level4, 5);
            levels = new Level[5];
            levels[0] = level;
            levels[1] = level2;
            levels[2] = level3;
            levels[3] = level4;
            levels[4] = level5;
            levCounter = new LevelCounter(gameContent, spriteBatch);
            mouse = new DrawMouse(spriteBatch, gameContent);
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
            KeyboardState currentKeyboardState = Keyboard.GetState();
            LevelCounter.updateCount();
            if (isPlaying)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Enter)&&prevKeyboardState.IsKeyUp(Keys.Enter))
                {
                    isPlaying = false;
                }
                if (character.Y > screenHeight)
                {
                    Exit();
                }
                if (character.landOnSpike)
                {
                    Exit();
                }
                if (character.WinLevel)
                {
                    levels[Level.curLevel].visible = false;
                    levels[Level.curLevel + 1].visible = true;
                    //Exit();
                }

                
                if (currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.A))
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
                if (!(currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.A)))
                {
                    character.ResetVectorX();
                }
                character.Move(floor, levels);
                if (!character.stable)
                {
                    character.ResetVectorY();
                }

                foreach (Land l in floor.floor)
                {
                    l.X = l.X - 1;
                    l.rect = new Rectangle((int)l.X, (int)l.Y, (int)l.Width, (int)l.Height);
                }
                foreach (Level l in levels)
                {
                    foreach (Tile t in l.level)
                    {
                        t.X = t.X - 1;
                        t.rect = new Rectangle((int)t.X, (int)t.Y, (int)t.Width, (int)t.Height);
                    }
                }
                if (character.resetFloor)
                {
                    foreach (Land l in floor.floor)
                    {
                        l.X = levels[Level.curLevel].level[levels[Level.curLevel].platformNumber - 1].X;
                        l.rect = new Rectangle((int)l.X, (int)l.Y, (int)l.Width, (int)l.Height);
                    }
                    
                }
                character.X -= 1;
                
            }
            else
            {
                if(startButton.Update(gameTime))
                {
                    isPlaying = true;
                    isStarted = true;
                }
                else if(exitButton.Update(gameTime))
                {
                    Exit();
                }
                
            }
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
            if (isStarted)
            {
                floor.Draw();
                foreach (Level l in levels)
                {
                    if (l.visible)
                    {
                        l.Draw();
                    }
                }
                character.Draw();
                levCounter.Draw();
            }
            else
            {
                startButton.Draw();
                exitButton.Draw();
                mouse.Draw();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
