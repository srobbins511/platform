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
    class LevelCounter
    {
        public SpriteFont levCounterDisplay;
        public SpriteBatch spriteBatch;
        static string mesgLev;
        static string mesgScore;
        public int X;
        public int Y;


        public LevelCounter(GameContent gameContent, SpriteBatch spriteBatch)
        {
            levCounterDisplay = gameContent.labelFont;
            this.spriteBatch = spriteBatch;
            X = 20;
            Y = 20;
        }

        public void Draw()
        {
            spriteBatch.DrawString(levCounterDisplay, mesgLev, new Vector2(X,Y), Color.White);
            spriteBatch.DrawString(levCounterDisplay, mesgScore, new Vector2(X,Y + 30), Color.White);
        }

        public static void updateCount()
        {
            mesgLev = "Level: " + (Level.curLevel + 1);
            mesgScore = "Score: " + Character.score;
        }


    }
}
