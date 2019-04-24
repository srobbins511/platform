using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Platforms
{
    class ControlsList
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Game1 Game1;

        public ControlsList(Game1 game, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = gameContent.labelFont;
            Game1 = game;
        }

        public void Draw()
        {
            spriteBatch.DrawString(spriteFont, "Controls", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenWidth / 4), Color.Black);
            spriteBatch.DrawString(spriteFont, "Jump: <SPACE>", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenHeight / 4 + 120), Color.Black);
            spriteBatch.DrawString(spriteFont, "Move Left: <A>", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenHeight / 4 + 150), Color.Black);
            spriteBatch.DrawString(spriteFont, "Move Right <D>", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenHeight / 4 + 180), Color.Black);
            spriteBatch.DrawString(spriteFont, "Pause <ENTER>", new Vector2(Game1.screenWidth / 2 - 60, Game1.screenHeight / 4 + 210), Color.Black);
        }
    }
}
