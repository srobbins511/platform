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
    class Floor
    {
        public Land[] floor { get; }
        public float Width { get; }
        public float Height { get; }
        public float X { get; }
        public float Y { get; }
        public Rectangle rect;

        public Floor(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight)
        {
            Width = 0;
            Height = 0;
            Land land = new Land(gameContent, spriteBatch, screenWidth, screenHeight, 0, 0);
            X = 0;
            Y = screenHeight - (land.Height / 2);
            float spaces = screenWidth / land.Width;
            floor = new Land[(int) spaces + 1];
            for(int i = 0; i < spaces; i++)
            {
                floor[i] = new Land(gameContent, spriteBatch, screenWidth, screenHeight, land.Width*i, screenHeight - (land.Height/2));
                Width += floor[i].Width;
                Height += floor[i].Height;
            }
        }

        public void Draw()
        {
            foreach(Land l in floor)
            {
                l.Draw();
            }
        }
    }
}
