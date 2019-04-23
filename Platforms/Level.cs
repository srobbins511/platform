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
        public int levelCount;
        public int platformNumber { get; set; }
        public int specPlatformNum { get; set; }
        private int xOffset;
        private int yOffset;
        public bool visible;

        //Generate the level randomly based on several parameters
        public Level(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight, Floor floor, int levelNum)
        {
            levelCount = levelNum;
            variation = 100;        //How far up or down the tile with be
            platformNumber = 20 * levelCount;       // the number of platforms in the level
            specPlatformNum = 5 * levelCount;       //the number of special platforms in the level
            level = new Tile[platformNumber + specPlatformNum];     //create a tile array to hold all platforms needed for level
            Width = 0;       
            Height = 0;
            Tile platform = new Tile(gameContent, spriteBatch, 0, 0);
            X = floor.X + floor.Width;      // the base X coordinate used to determine first tile location
            Y = screenHeight- (screenHeight/4);     //the base Y coordinate used to determine first tile location
            prevHeight = 0;
            visible = true;
            
            //loop to create all the basic tiles in the game
            for (int i = 0; i < platformNumber ; i++)
            {
                level[i] = new Tile(gameContent, spriteBatch, X + (100*i+1),Y );
                Width += level[i].Width;
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
                int offsetShift = randGen.Next(1, 4);
                int platformPlace = randGen.Next(0, platformNumber);
                int Y = 0;
                int x = 0;
                if(type == 2)
                {
                    //Spike platform creation
                    xOffset = 50;
                    yOffset = (int)level[platformPlace].Y;
                    X = level[platformPlace].X - xOffset;
                    Y = yOffset;
                }
                else
                {
                    //Icy Platform creation
                    xOffset = 0;
                    yOffset = -70*offsetShift;
                    X = level[platformPlace].X - xOffset;
                    if(level[platformPlace].Y< screenHeight/2)
                    {
                        Y = (int)level[platformPlace].Y - yOffset;
                    }
                    else
                    {
                        Y = (int)level[platformPlace].Y + yOffset;
                    }

                }
                level[i] = new Tile(gameContent, spriteBatch, X, Y, type);
            }
            X = level[0].X;

        }



        public Level(GameContent gameContent, SpriteBatch spriteBatch, float screenWidth, float screenHeight,Level start, int levelNum)
        {
            levelCount = levelNum;
            variation = 100;        //How far up or down the tile with be
            platformNumber = 20 * levelCount;       // the number of platforms in the level
            specPlatformNum = 5 * levelCount;       //the number of special platforms in the level
            level = new Tile[platformNumber + specPlatformNum];     //create a tile array to hold all platforms needed for level
            Width = 0;
            Height = 0;
            Tile platform = new Tile(gameContent, spriteBatch, 0, 0);
            int beginPos =(int) start.platformNumber;
            X = start.level[start.platformNumber-1].X;     // the base X coordinate used to determine first tile location
            Y = screenHeight - (screenHeight / 4);     //the base Y coordinate used to determine first tile location
            prevHeight = 0;
            visible = false;

            //loop to create all the basic tiles in the game
            for (int i = 0; i < platformNumber; i++)
            {
                level[i] = new Tile(gameContent, spriteBatch, X + (100 * i + 1), Y);
                Width += level[i].Width;
                int zerOne = randGen.Next(0, 2);
                prevHeight = Y;
                if (Y > 100 && Y <= (screenHeight - 200))
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
                int offsetShift = randGen.Next(1, 4);
                int platformPlace = randGen.Next(0, platformNumber);
                int Y = 0;
                int x = 0;
                if (type == 2)
                {
                    //Spike platform creation
                    xOffset = 50;
                    yOffset = (int)level[platformPlace].Y;
                    X = level[platformPlace].X - xOffset;
                    Y = yOffset;
                }
                else
                {
                    //Icy Platform creation
                    xOffset = 0;
                    yOffset = -70 * offsetShift;
                    X = level[platformPlace].X - xOffset;
                    if (level[platformPlace].Y < screenHeight / 2)
                    {
                        Y = (int)level[platformPlace].Y - yOffset;
                    }
                    else
                    {
                        Y = (int)level[platformPlace].Y + yOffset;
                    }

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
