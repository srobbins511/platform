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
    class Tile
    {
        private Texture2D platform { get; set; }
        public float Height { get; }
        public float Width { get; }
        private SpriteBatch spriteBatch;
        public float X { get; set; }
        public float Y { get; }
        public Rectangle rect;
        public int platformType { get; set; }

        //Constructor to create a basic tile
        public Tile(GameContent gameContent, SpriteBatch spriteBatch, float x, float y)
        {
            platform = gameContent.JumpTile;
            this.spriteBatch = spriteBatch;
            Height = 20;
            Width = 50;
            X = x;
            Y = y;
            rect = new Rectangle((int)X, (int)Y,(int) Width, (int)Height);
        }

        //Constructor to create a special tile, the last param is used to determine the type of special tiel
        public Tile(GameContent gameContent, SpriteBatch spriteBatch, float x, float y, int type)
        {
            platformType = type;
            if(platformType == 2)
            {
                platform = gameContent.SpikyPad;
                Width = 50;
            }
            else if (platformType == 3)
            {
                platform = gameContent.IcyPad;
                Width = 67;
            }
            this.spriteBatch = spriteBatch;
            Height = 20;
            X = x;
            Y = y;
            rect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        public void Draw()
        {
            spriteBatch.Draw(platform, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
