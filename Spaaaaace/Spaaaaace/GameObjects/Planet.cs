using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spaaaaace.Graphics;
using Microsoft.Xna.Framework;

namespace Spaaaaace.GameObjects
{
    public class Planet: GameObject
    {
        public float Gravity { get; protected set; }

        public float Mass { get; protected set; }
        private float radius;
        private float density;

        private Boolean changedMass;

        public Planet(LonharGame game, float density, float radius, Vector2 position)
            :base(new Sprite(game, "Planet"), position)
        {
            changedMass = false;
            this.density = density;
            this.radius = radius;
            calculate();
        }

        public override void Update(GameTime gameTime)
        {
            if (changedMass)
            {
                calculate();
            }
            base.Update(gameTime);
        }

        public void calculate()
        {
            Mass = (float)(density * (Math.PI * 4 / 3) * radius * radius * radius);
        }
    }
}
