using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platforms
{
    class Button
    {
        Game1 Game1;
        GraphicsDevice GraphicsDevice;
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

        public Button(Game1 game, string text, int height, int width, Vector2 position)
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
        }

        public void Update(GameTime gameTime)
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
        }
    }
}
