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
        private Land[] floor;

        public Floor(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight)
        {
            Land land = new Land(gameContent, spriteBatch, screenWidth, screenHeight, 0, 0);
            float spaces = screenWidth / land.Width;
            floor = new Land[(int) spaces + 1];
            for(int i = 0; i < spaces; i++)
            {
                floor[i] = new Land(gameContent, spriteBatch, screenWidth, screenHeight, land.Width*i, screenWidth + 100 - land.Height * 2);
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
