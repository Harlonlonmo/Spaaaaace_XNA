using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spaaaaace.Graphics
{
    public class AnimatedSprite : Sprite
    {

        private Rectangle[] sources;

        public float animCounter;

        public int FPS { get; protected set; }

        public AnimatedSprite(LonharGame game, String name, int width, int height, int fps)
            : base(game, name)
        {
            FPS = fps;
            animCounter = 0f;
            int numX = texture.Width / width;
            int numY = texture.Height / height;
            sources = new Rectangle[numX * numY];
            for (int y = 0; y < numY; y++)
            {
                for (int x = 0; x < numX; x++)
                {
                    sources[x + y * numX] = new Rectangle(x * width, y * height, width, height);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 target, GameTime gameTime)
        {
            animCounter += (float)gameTime.ElapsedGameTime.TotalSeconds * (float)FPS;
            animCounter %= sources.Length;
            spriteBatch.Draw(texture, target, sources[(int)(animCounter)], Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 target, int frame)
        {
            frame += sources.Length;
            frame %= sources.Length;
            spriteBatch.Draw(texture, target, sources[frame], Color.White);
        }
        public override void DrawCenter(SpriteBatch spriteBatch, Vector2 Position, GameTime gameTime)
        {
            Draw(spriteBatch, Position - new Vector2(sources[0].Width / 2, sources[0].Height / 2), gameTime);
        }
        public void DrawCenter(SpriteBatch spriteBatch, Vector2 Position, int frame)
        {
            Draw(spriteBatch, Position - new Vector2(sources[0].Width / 2, sources[0].Height / 2), frame);
        }
    }
}
