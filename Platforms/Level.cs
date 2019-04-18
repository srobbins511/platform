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
        private Random randGen = new Random();
        public static int levelCount = 1;
        public int platformNumber { get; set; }
        public int specPlatformNum { get; set; }
        private int xOffset;
        private int yOffset;

        public Level(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight, Floor floor)
        {
            variation = 100;
            platformNumber = 20 * levelCount;
            specPlatformNum = 5 * levelCount;
            level = new Tile[platformNumber + specPlatformNum];
            Width = 0;
            Height = 0;
            Tile platform = new Tile(gameContent, spriteBatch, 0, 0);
            X = floor.X + floor.Width;
            Y = screenHeight- (screenHeight/4);
            prevHeight = 0;
            
            for (int i = 0; i < platformNumber ; i++)
            {
                level[i] = new Tile(gameContent, spriteBatch, X + (100*i+1),Y );
                int zerOne = randGen.Next(0, 2);
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

            for (int i = platformNumber; i < platformNumber + specPlatformNum; i++)
            {
                int type = randGen.Next(2, 4);
                int offsetShift = randGen.Next(-3, 3);
                int platformPlace = randGen.Next(0, platformNumber);
                int Y = 0;
                int x = 0;
                if(type == 2)
                {
                    xOffset = 50;
                    yOffset = (int)level[platformPlace].Y;
                    X = level[platformPlace].X - xOffset;
                    Y = yOffset;
                }
                else
                {
                    xOffset = -67;
                    yOffset = -50*offsetShift;
                    X = level[platformPlace].X - xOffset;
                    Y = (int)level[platformPlace].Y - yOffset;
                }
                level[i] = new Tile(gameContent, spriteBatch, X, Y, type);
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
