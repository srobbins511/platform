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
    class DrawMouse
    {
        private Texture2D MouseTexture;
        public MouseState curMouseState;
        public MouseState prevMouseState;
        public Point mousePosition;
        SpriteBatch spriteBatch;
        GameContent gameContent;


        public DrawMouse(SpriteBatch spriteBatch, GameContent gameContent)
        {
            
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;
            MouseTexture = gameContent.MouseTexture;
        }

        public void Draw()
        {
            MouseState curMouseState = Mouse.GetState();
            Point mousePosition = curMouseState.Position;
            Vector2 Position = new Vector2(mousePosition.X, mousePosition.Y);
            spriteBatch.Draw(MouseTexture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
