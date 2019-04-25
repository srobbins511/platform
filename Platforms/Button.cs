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
            button = new Rectangle((int)Position.X, (int)Position.Y, this.width *2, this.height *2);
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
                        if (text.Equals("Start")|| text.Equals("Continue"))
                        {
                            clicked = true;
                            return true;
                        }
                        else if(text.Equals("Exit"))
                        {
                            clicked = true;
                            return true;
                        }
                        else if (text.Equals("Controls"))
                        {
                            clicked = true;
                            return true;
                        }
                        else if (text.Equals("Return"))
                        {
                            clicked = true;
                            return true;
                        }
                    }
                }
                prevMouseState = curMouseState;
            }
            clicked = false;
            return false;
        }

        public void Draw()
        {
            if(text.Equals("Start"))
            {
                spriteBatch.Draw(this.Texture, new Vector2(Position.X - 10, Position.Y - 10), null, Color.Green, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            else if (text.Equals("Continue"))
            {
                spriteBatch.Draw(this.Texture, new Vector2( Position.X-10 , Position.Y - 10), null, Color.Green, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            else if(text.Equals("Exit"))
            {
                spriteBatch.Draw(this.Texture, new Vector2(Position.X - 10, Position.Y - 10), null, Color.Red, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            else if (text.Equals("Controls"))
            {
                spriteBatch.Draw(this.Texture, new Vector2(Position.X - 10, Position.Y - 10), null, Color.Green, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            else if (text.Equals("Return"))
            {
                spriteBatch.Draw(this.Texture, new Vector2(Position.X - 10, Position.Y - 10), null, Color.Red, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            spriteBatch.DrawString(spriteFont, text, Position, Color.Black);
            if(!Game1.died && !Game1.ControlScreen && !Game1.Win)
            {
                spriteBatch.DrawString(spriteFont, "Super Golem Bro's", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenWidth / 4), Color.Black);
            }
            else if (Game1.Win)
            {
                spriteBatch.DrawString(spriteFont, "You Win", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenWidth / 4), Color.Black);
            }
            else if(!Game1.ControlScreen)
            {
                spriteBatch.DrawString(spriteFont, "You Died", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenWidth / 4), Color.Black);
            }
            

        }
    }
}
