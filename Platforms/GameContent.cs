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
    class GameContent
    {
        public Texture2D body { get; set; }
        public Texture2D head { get; set; }
        public Texture2D face1 { get; set; }
        public Texture2D face2 { get; set; }
        public Texture2D face3 { get; set; }
        public Texture2D leftArm { get; set; }
        public Texture2D leftHand { get; set; }
        public Texture2D leftLeg { get; set; }
        public Texture2D rightArm { get; set; }
        public Texture2D rightLeg { get; set; }
        public Texture2D rightHand { get; set; }
        public Texture2D landTile { get; set; }
        public Texture2D JumpTile { get; set; }
        public Texture2D SpikyPad { get; set; }
        public Texture2D IcyPad { get; set; }
        public Texture2D BramblePad { get; set; }
        public Texture2D ButtonTexture { get; set; }
        public Texture2D MouseTexture { get; set; }
        public SpriteFont labelFont { get; set; }
        public SoundEffect jumpSound { get; set; }
        public SoundEffect wallBounce { get; set; }
        public SoundEffect deathSound { get; set; }

        public GameContent(ContentManager Content)
        {
            body = Content.Load<Texture2D>("Body");
            head = Content.Load<Texture2D>("Head");
            face1 = Content.Load<Texture2D>("Face1");
            face2 = Content.Load<Texture2D>("Face2");
            face3 = Content.Load<Texture2D>("Face3");
            leftArm = Content.Load<Texture2D>("LeftArm");
            leftHand = Content.Load<Texture2D>("LeftArm");
            leftLeg = Content.Load<Texture2D>("LeftLeg");
            rightArm = Content.Load<Texture2D>("RightArm");
            rightLeg = Content.Load<Texture2D>("RightLeg");
            rightHand = Content.Load<Texture2D>("RightHand");
            labelFont = Content.Load<SpriteFont>("Arial20");
            landTile = Content.Load<Texture2D>("bg");
            JumpTile = Content.Load<Texture2D>("Pad1Sized");
            SpikyPad = Content.Load<Texture2D>("SpikyPad");
            IcyPad = Content.Load<Texture2D>("IcyPad");
            BramblePad = Content.Load<Texture2D>("BramblePad");
            ButtonTexture = Content.Load<Texture2D>("ButtonTexture");
            MouseTexture = Content.Load<Texture2D>("MouseTexture");
        }
    }
}
