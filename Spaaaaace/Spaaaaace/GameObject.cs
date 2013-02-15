using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spaaaaace.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spaaaaace
{
    public class GameObject
    {
        protected Sprite sprite;

        public Vector2 Position { get; protected set; }

        public Vector2 DeltaS { get; protected set; }
        public Vector2 DeltaV { get; protected set; }

        public GameObject(Sprite sprite, Vector2 position)
        {
            this.sprite = sprite;
            Position = position;
            DeltaV = new Vector2(0,0);
            DeltaS = new Vector2(0,0);
        }

        public virtual void Update(GameTime gameTime)
        {
            DeltaS += DeltaV * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += DeltaS * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, Position, gameTime);
        }
        public virtual void DrawCenter(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.DrawCenter(spriteBatch, Position, gameTime);
        }

    }
}
