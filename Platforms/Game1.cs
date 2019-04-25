using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        private Button ContinueButton;
        private Button ControlsButton;
        private Button ReturnButton;
        private Button exitButton;
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private DrawMouse mouse;
        private ControlsList controlsList;
        private bool isPlaying = false;
        private bool isStarted = false;
        public bool died = false;
        public bool Win = false;
        public bool ControlScreen = false;
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
            //create all the buttons needed for the game and place them
            startButtonPosition = new Vector2((screenWidth / 2 - 20), (screenHeight / 10) * 5);
            exitButtonPosition = new Vector2((screenWidth / 2 - 20) , (screenHeight / 10) * 6);
            startButton = new Button(this, spriteBatch, gameContent, "Start", screenHeight / 10, screenWidth / 5, startButtonPosition);
            ContinueButton = new Button(this, spriteBatch, gameContent, "Continue", screenHeight / 10, screenWidth / 5, startButtonPosition);
            ControlsButton = new Button(this, spriteBatch, gameContent, "Controls", screenHeight / 10, screenWidth / 5, exitButtonPosition);
            exitButtonPosition.Y += 100;
            exitButton = new Button(this, spriteBatch, gameContent, "Exit", screenHeight / 10, screenWidth / 5, exitButtonPosition);
            exitButtonPosition.Y -= 50;
            ReturnButton = new Button(this, spriteBatch, gameContent, "Return", screenHeight / 10, screenWidth / 5, exitButtonPosition);
            //create the bool for if the game has started and is not paused
            isPlaying = false;
            //load in all the levels
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
            //create the level counter
            levCounter = new LevelCounter(gameContent, spriteBatch);
            //create the mouse object so player can see a mouse in the menus
            mouse = new DrawMouse(spriteBatch, gameContent);
            //create a controls list so the control menu can be seen
            controlsList= new ControlsList(this, spriteBatch, gameContent);
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
            if (isPlaying)//Check to see if game should be running or not
            {
                //check to see if player pauses the game
                if (currentKeyboardState.IsKeyDown(Keys.Enter)&&prevKeyboardState.IsKeyUp(Keys.Enter))
                {
                    isPlaying = false;
                }
                if (character.Y > screenHeight)//check to see if character fell off the botton of the screen
                {
                    died = true;
                }
                if (character.landOnSpike)//check to see if character landed on a spike
                {
                    died = true;
                }
                if (character.WinLevel)//check to see if character has completed the current level
                {
                    levels[Level.curLevel].visible = false;
                    if(Level.curLevel<5)
                    {
                        levels[Level.curLevel + 1].visible = true;
                    }
                    //Exit();
                }
                if (died)//check to see if character has died in some way
                {
                    PlaySound(GameContent.DeathSound);
                    isPlaying = false;
                }
                if (character.WinCounter >= 5)//check to see if character has completed all levels and won the game
                {
                    PlaySound(GameContent.WinSound);
                    isPlaying = false;
                    Win = true;
                }

                //check to see if character should move
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
                //check to see if character should jump
                if (currentKeyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyDown(Keys.Space))
                {
                    character.Jump();
                }
                //check to see if character is not moving
                if (!(currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.A)))
                {
                    character.ResetVectorX();
                }
                //move the character
                character.Move(floor, levels);
                //check to see if character is not on top of an object
                if (!character.stable)
                {
                    character.ResetVectorY();
                }

                //the logic to cause the screen to passively scroll
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
                //the logic to cause the floor to move to its next position when character starts the next level
                if (character.resetFloor)
                {
                    foreach (Land l in floor.floor)
                    {
                        l.X = levels[Level.curLevel].level[levels[Level.curLevel].platformNumber - 1].X;
                        l.rect = new Rectangle((int)l.X, (int)l.Y, (int)l.Width, (int)l.Height);
                    }
                    
                }
                //scroll the character with the landscape
                character.X -= 1;
                
            }
            else
            {
                //check to see if player clicked start
                if(startButton.Update(gameTime)&&!isStarted)
                {
                    isPlaying = true;
                    isStarted = true;
                }
                //check to see if character clicked continue when paused
                else if(ContinueButton.Update(gameTime)&&!died)
                {
                    isPlaying = true;
                }
                //check to see if player clicked controls
                else if(ControlsButton.Update(gameTime))
                {
                    ControlScreen = true;
                }
                //check to see if player wants to return to menu from looking at controls
                else if (ReturnButton.Update(gameTime))
                {
                    ControlScreen = false;
                }
                //check to see if character hit exit
                else if(exitButton.Update(gameTime))
                {
                    died = false;
                    Exit();
                }
                
            }
            //save the current keyboard state for next update call
            prevKeyboardState = currentKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //draw in the sky
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin();
            //check to see if character has started playing or is on start menu
            if (isStarted)
            {
                //check to see if after starting player has paused or not
                //if not draw the normal game screen
                if(isPlaying)
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
                }
                //check to see if character has paused
                //if so draw the pause screen
                else if(!died&&!ControlScreen&&!Win)
                {
                    ContinueButton.Draw();
                    ControlsButton.Draw();
                    exitButton.Draw();
                    mouse.Draw();
                }
                //check to see if the player waants to view the controls
                else if(ControlScreen)
                {
                    //if yes draw out the controls screen
                    controlsList.Draw();
                    ReturnButton.Draw();
                    mouse.Draw();
                }
                //will draw the exit screen if either character has died or won
                else
                {
                    exitButton.Draw();
                    mouse.Draw();
                }
                //draw the level counter and score keeping
                levCounter.Draw();
            }
            else if (ControlScreen)
            {
                //if they have not started but want to view controls show the controls screen
                controlsList.Draw();
                ReturnButton.Draw();
                mouse.Draw();
            }
            else
            {
                //draw the start menu if player has not clicked start
                startButton.Draw();
                ControlsButton.Draw();
                exitButton.Draw();
                mouse.Draw();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void PlaySound(SoundEffect sound)
        {
            float volume = 1;
            float pitch = 0.0f;
            float pan = 0.0f;
            sound.Play(volume, pitch, pan);
        }
    }
}
