using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaaaaace.Graphics
{
    public class Sprite
    {

        protected Texture2D texture;

        public Sprite(LonharGame game, String name)
        {
            texture = game.Textures.getTexture(name);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle rect, GameTime gameTime)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
        public virtual void DrawCenter(SpriteBatch spriteBatch, Vector2 Position, GameTime gameTime)
        {
            Draw(spriteBatch, Position - new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2), gameTime);
        }
    }
}
