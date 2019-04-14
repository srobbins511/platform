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
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public double xVector { get; set; }
        public double yVector { get; set; }
        public static float distance { get; set; }
        public float screenWidth;
        public float screenHeight;

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

        private SpriteBatch spriteBatch;
        private static float JumpHeight = -200;
        private static float Gravity = 10;
        private static float MovementSpeed = 5;
        private bool Jumped = false;
        private bool canJump = false;

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
            

            Width = body.Width + leftArm.Width + rightArm.Width;
            Height = body.Height + leftLeg.Height + head.Height;
            this.screenWidth= screenWidth;
            this.screenHeight = screenHeight;

            this.X = x;
            this.Y = y;
            this.xVector = 0;
            this.yVector = 0;
            this.spriteBatch = spriteBatch;
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
        public void Move(Floor floor)
        {
            Rectangle charRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            if (X>=0 && X<screenWidth)
            {
                X += (float)xVector;
            }
            if (colTest(charRect, floor))
            {
               Y = floor.Y - 45;
                canJump = true;
            }
            else
            {
                Y = Y + (float)yVector;
            }
            if (Jumped)
            {
                Y = Y + (float)yVector;
                Jumped = false;
            }
            
            
        }

        public Boolean colTest(Rectangle r1, Floor floor)
        {
            foreach(Land l in floor.floor)
            {
                if (Rectangle.Intersect(r1, l.rect) != Rectangle.Empty)
                {
                    return true;
                }
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
            else 
            {
                xVector = xVector* .7;
            }
        }

        public void ResetVectorY()
        {
            if (yVector < Gravity)
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
            if(canJump)
            {
                yVector = JumpHeight/2;
                Jumped = true;
                canJump = false;
            }
        }
    }
}
