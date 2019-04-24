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
    class Character
    {
        public int tileY;
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public double xVector { get; set; }
        public double yVector { get; set; }
        public float distance { get; set; }
        public float screenWidth;
        public float screenHeight;
        public bool stable { get; set; }
        public bool WinLevel { get; set; }

        private Texture2D body { get; set; }

        private Texture2D head { get; set; }
        private Texture2D face1 { get; set; }
        private Texture2D face2 { get; set; }
        private Texture2D face3 { get; set; }
        private Texture2D leftArm { get; set; }
        private Texture2D leftHand { get; set; }
        private Texture2D leftLeg { get; set; }
        private Texture2D rightArm { get; set; }
        private Texture2D rightLeg { get; set; }
        private Texture2D rightHand { get; set; }
        public bool resetFloor { get; set; }

        private SpriteBatch spriteBatch;
        private static float JumpHeight = -20;
        private static float Gravity = 10;
        private static float MovementSpeed = 5;
        private bool Jumped = false;
        public bool landOnSpike { get; set; }
        public bool landOnIce { get; set; }
        public static int score = 0;

        //initailize all the character data
        public Character(GameContent gameContent, SpriteBatch spriteBatch, float x, float y, float screenWidth, float screenHeight)
        {
            this.head = gameContent.head;
            this.body = gameContent.body;
            this.face1 = gameContent.face1;
            this.face2 = gameContent.face2;
            this.face3 = gameContent.face3;
            this.leftArm = gameContent.leftArm;
            this.leftHand = gameContent.leftHand;
            this.leftLeg = gameContent.leftLeg;
            this.rightArm = gameContent.rightArm;
            this.rightHand = gameContent.rightHand;
            this.rightLeg = gameContent.rightLeg;

            stable = false;
            Width = 30;
            Height = 55;
            landOnIce = false;
            landOnSpike = false;
            this.screenWidth= screenWidth;
            this.screenHeight = screenHeight;

            this.X = x;
            this.Y = y;
            this.xVector = 0;
            this.yVector = 0;
            this.spriteBatch = spriteBatch;
            distance = x;
            WinLevel = false;
        }


        //Draw the Character to the screen
        public void Draw()
        {
            spriteBatch.Draw(leftArm, new Vector2(X + 27, Y + 39), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(leftHand, new Vector2(X + 27, Y + 45), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(leftLeg, new Vector2(X + 23, Y + 51), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(rightLeg, new Vector2(X + 17, Y + 51), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(body, new Vector2(X + 8, Y + 32), null, Color.White, 0, new Vector2(0,0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(rightArm, new Vector2(X + 11, Y + 39), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(rightHand, new Vector2(X + 11, Y + 45), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(head, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
            spriteBatch.Draw(face1, new Vector2(X+13, Y+19), null, Color.White, 0, new Vector2(0, 0), .1f, SpriteEffects.None, 0);
        }

        //change the xVector value to initiate movement and determine speed for leftward movement
        public void MovingLeft()
        {
            if(xVector> -MovementSpeed)
            {
                xVector += -.2;
            } 
        }
        //Same as for left movement but for right
        public void MovingRight()
        {
            if(xVector<MovementSpeed)
            {
                xVector += .2;
            }
        }
        //Use the vectors to move the character on the x axis
        public void Move(Floor floor, Level[] map)
        {
            float prevX = X;
            Rectangle charRect = new Rectangle((int)X, (int)(Y + Height - 1), (int)Width, 1);
            if(X<=0)
            {
                xVector += 20;
                Jump();
            }
            if ( X < screenWidth - xVector)
            {
                X += (float)xVector;
                distance = (float)xVector;
            }
            if (colTest(charRect, map))
            {
                if(!stable && yVector>=0)
                {
                    Y = tileY - Height+1;
                    stable = true;
                }
            }
            else if (colTest(charRect, floor))
            {
                if(!stable && yVector>=0)
                {
                    Y = floor.Y - Height+1;
                    stable = true;
                }
                
            }
            else
            {
                stable = false;
            }
            if(!stable)
            {
                Y = Y + (float)yVector;
            }
            if (Jumped)
            {
                Y = Y + (float)yVector;
                Jumped = false;
            }
            if(prevX < X)
            {
                score+= 1 * (Level.curLevel + 1);
                foreach (Land l in floor.floor)
                {
                    l.X = l.X - distance;
                    l.rect = new Rectangle((int)l.X, (int)l.Y,(int) l.Width,(int)l.Height);
                }
                foreach (Level l in map)
                {
                    foreach (Tile t in l.level)
                    {
                        t.X = t.X - distance;
                        t.rect = new Rectangle((int)t.X, (int)t.Y, (int)t.Width, (int)t.Height);
                    }
                }
            }
           
        }

        //Check to see if the Character has landed on the floor
        public Boolean colTest(Rectangle r1, Floor floor)
        {
            foreach(Land l in floor.floor)
            {
                if (Rectangle.Intersect(r1, l.rect) != Rectangle.Empty)
                {
                    if(resetFloor)
                    {
                        WinLevel = true;
                        resetFloor = false;
                    }
                    return true;
                }
            }
            return false;
        }

        //chack to see if the character has landed on any tiles
        public Boolean colTest(Rectangle r1, Level[] map)
        {
            int count = 0;
            int levCount = 0;
            foreach (Level l in map)
            {
                if(l.visible)
                {
                    foreach (Tile t in l.level)
                    {
                        count++;
                        if (Rectangle.Intersect(r1, t.rect) != Rectangle.Empty)
                        {
                            if (t.platformType == 2 && yVector > 0)
                            {
                                landOnSpike = true;
                            }
                            if (t.platformType == 3 && yVector > 0)
                            {
                                landOnIce = true;
                            }
                            else
                            {
                                landOnIce = false;
                            }
                            if (count > 0)
                            {
                                WinLevel = false;
                                resetFloor = true;
                                if(levCount> Level.curLevel)
                                {
                                    Level.curLevel++;
                                }
                            }
                            tileY = (int)t.Y;
                            return true;
                        }
                    }
                }
                levCount++;
            }
            
            return false;
        }
        //Alter the vector values when they are not in use so they return to their default states
        public void ResetVectorX()
        {
            if(xVector > -.4 && xVector < .4)
            {
                xVector = 0;
            }
            else if(landOnIce)
            {
                return;
            }
            else 
            {
                xVector = xVector* .7;
            }
        }

        //Reset the Y vector of the character based on its current y Vector
        public void ResetVectorY()
        {
            if(stable)
            {
                yVector = 0;
            }
            else if (yVector < Gravity)
            {
                yVector += .5;
            }
            else if (yVector > Gravity)
            {
                yVector = Gravity;
            }
        }

        //set the yVector to create upward movement for the character
        public void Jump()
        {
            if(stable)
            {
                yVector = JumpHeight/2;
                Jumped = true;
            }
        }
    }
}
