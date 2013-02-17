using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spaaaaace.Graphics;
using Microsoft.Xna.Framework;
using C3.XNA;

namespace Spaaaaace.GameObjects
{
    public class Planet : GameObject
    {
        private float density;
        public float Mass { get; protected set; }
        private float _radius;
        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                Mass = (float)(density * (Math.PI * 4 / 3) * value * value * value);
            }
        }

        public Boolean Selected { get; set; }

        public Planet(LonharGame game, float density, float radius, Vector2 position)
            : base(new Sprite(game, "Planet"), position)
        {
            this.density = density;
            Radius = radius;
        }

        public override void DrawCenter(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            float pixelRadius = Radius / 2E+5f;
            Rectangle drawRect = new Rectangle(
                    (int)(Position.X - pixelRadius),
                    (int)(Position.Y - pixelRadius),
                    (int)(pixelRadius * 2),
                    (int)(pixelRadius * 2));
            sprite.Draw(spriteBatch, drawRect, gameTime);
            if (Selected)
            {
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X, drawRect.Y), new Vector2(drawRect.X + drawRect.Width / 4, drawRect.Y), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X, drawRect.Y), new Vector2(drawRect.X, drawRect.Y + drawRect.Height / 4), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X + drawRect.Width * 3 / 4, drawRect.Y), new Vector2(drawRect.X + drawRect.Width, drawRect.Y), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X + drawRect.Width, drawRect.Y + drawRect.Height / 4), new Vector2(drawRect.X + drawRect.Width, drawRect.Y), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X, drawRect.Y + drawRect.Height), new Vector2(drawRect.X + drawRect.Width / 4, drawRect.Y + drawRect.Height), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X, drawRect.Y + drawRect.Height * 3 / 4), new Vector2(drawRect.X, drawRect.Y + drawRect.Height), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X + drawRect.Width, drawRect.Y + drawRect.Height), new Vector2(drawRect.X + drawRect.Width * 3 / 4, drawRect.Y + drawRect.Height), Color.White);
                Primitives2D.DrawLine(spriteBatch, new Vector2(drawRect.X + drawRect.Width, drawRect.Y + drawRect.Height * 3 / 4), new Vector2(drawRect.X + drawRect.Width, drawRect.Y + drawRect.Height), Color.White);
            }
        }
    }
}
