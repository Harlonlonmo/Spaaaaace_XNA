using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spaaaaace.Graphics;
using Microsoft.Xna.Framework;

namespace Spaaaaace.GameObjects
{
    public class PlayerShip:GameObject
    {
        private float mass;

        public PlayerShip(LonharGame game, Vector2 position)
            :base(new AnimatedSprite(game, "Player ship", 50, 50, 30), position)
        {
            DeltaS = new Vector2(150, 0);
            mass = 10000;
        }

        public void Update(GameTime gameTime, List<Planet> planets)
        {
            DeltaV = new Vector2(0, 0);
            foreach (Planet p in planets)
            {
                float r = (p.Position - Position).Length()*1E+6f;
                if (r > 0)
                {
                    float F = (6.67E-11f * mass * p.Mass) / (r * r);
                    DeltaV += Vector2.Normalize(p.Position - Position) * F;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            ((AnimatedSprite)sprite).DrawCenter(spriteBatch, Position, (int)Math.Round(MathHelper.ToDegrees((float)Math.Atan2(DeltaS.Y, DeltaS.X))/-5));
        }
    }
}
