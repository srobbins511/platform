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

        public Character()
        {

        }
    }
}
