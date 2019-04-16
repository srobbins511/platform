using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platforms
{
    class Land
    {
        private Texture2D land { get; set; }
        private float screenWidth;
        private float screenHeight;
        public float Height { get; }
        public float Width { get; }
        private SpriteBatch spriteBatch;
        public float X { get; set; }
        public float Y { get; set; }
        public Rectangle rect;

        public Land(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight, float x, float y)
        {
            land = gameContent.landTile;
            this.spriteBatch = spriteBatch;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            Height = land.Height;
            Width = land.Width;
            X = x;
            Y = y;
            rect = new Rectangle((int)X,(int) Y, land.Width, land.Height);
        }

        public void Draw()
        {
            spriteBatch.Draw(land, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
        }
    }
}
