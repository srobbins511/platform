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
    class Level
    {
        public Tile[] level { get; }
        public float Width { get; }
        public float Height { get; }
        public float X { get; set; }
        public float Y { get; }
        private float prevHeight;
        private float variation;
        private Random posNeg = new Random();

        public Level(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight)
        {
            variation = 100;
            level = new Tile[100];
            Width = 0;
            Height = 0;
            Tile platform = new Tile(gameContent, spriteBatch, 0, 0);
            X = screenWidth/2;
            Y = screenHeight- (screenHeight/4);
            prevHeight = 0;
            
            for (int i = 0; i < 100; i++)
            {
                level[i] = new Tile(gameContent, spriteBatch, X + (100*i+1),Y );
                int zerOne = posNeg.Next(0, 2);
                prevHeight = Y;
                if(Y>100 && Y<=(screenHeight-200))
                {
                    if (zerOne == 1)
                    {
                        Y = (Y + variation);
                    }
                    else
                    {
                        Y = Y - variation;
                    }
                }
                else
                {
                    Y = screenHeight - 300;
                }
                
            }
        }

        public void Draw()
        {
            foreach (Tile t in level)
            {
                t.Draw();
            }
        }

    }
}
