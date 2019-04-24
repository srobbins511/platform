using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Platforms
{
    class Button
    {
        Game1 Game1;
        GraphicsDevice GraphicsDevice;
        GameContent gameContent;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D Texture;
        Vector2 Position;
        Color Color;
        int height;
        int width;
        string text;
        MouseState prevMouseState;
        Rectangle button;

        public bool clicked = false;
        public bool start = false;
        public bool exit = false;

        /*public Button(Game1 game, string text, int height, int width, Vector2 position)
        {
            Game1 = game;
            GraphicsDevice = game.GraphicsDevice;
            Color = Color.Black;
            this.text = text;
            this.height = height;
            this.width = width;
            Texture = new Texture2D(GraphicsDevice, width, height);
            Position = position;
            prevMouseState = Mouse.GetState();
            button = new Rectangle((int)Position.X, (int)Position.Y, this.width, this.height);
        }*/

        public Button(Game1 game, SpriteBatch spriteBatch,GameContent gameContent, string text, int height, int width, Vector2 position)
        {
            Game1 = game;
            this.spriteBatch = spriteBatch;
            GraphicsDevice = game.GraphicsDevice;
            spriteFont = gameContent.labelFont;
            this.Color = Color.Black;
            this.text = text;
            Texture = gameContent.ButtonTexture;
            this.height = Texture.Height;
            this.width = Texture.Width;
            Position = position;
            prevMouseState = Mouse.GetState();
            button = new Rectangle((int)Position.X, (int)Position.Y, this.width, this.height);
        }

        /*public void Update(GameTime gameTime)
        {
            if (!clicked)
            {
                MouseState curMouseState = Mouse.GetState();
                Point mousePosition = curMouseState.Position;
                if (button.Contains(mousePosition))
                {
                    if (curMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        if (text.Equals("Start"))
                        {

                        }
                        else
                        {
                            Game1.Exit();
                        }
                        clicked = true;
                    }
                }
                prevMouseState = curMouseState;
            }
        }*/

        public bool Update(GameTime gameTime)
        {
            if (!clicked)
            {
                MouseState curMouseState = Mouse.GetState();
                Point mousePosition = curMouseState.Position;
                if (button.Contains(mousePosition))
                {
                    if (curMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        if (text.Equals("Start"))
                        {
                            clicked = true;
                            return true;
                        }
                        else if(text.Equals("Exit"))
                        {
                            clicked = true;
                            return true;
                        }
                    }
                }
                prevMouseState = curMouseState;
            }
            return false;
        }

        public void Draw()
        {
            spriteBatch.Draw(this.Texture, Position, null, Color.Green, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, text, Position, Color.Black);
            spriteBatch.DrawString(spriteFont, "Golem Jump", new Vector2(Game1.screenWidth/2 - 60, Game1.screenWidth/4), Color.Black);

        }
    }
}
