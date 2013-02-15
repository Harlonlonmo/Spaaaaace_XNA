using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaaaaace
{
    public class Button
    {
        public const int TEXT_ALIGN_LEFT = 0;
        public const int TEXT_ALIGN_MID = 1;
        public const int TEXT_ALIGN_RIGHT = 2;

        protected int textAlign;
        public String Text { get; protected set; }
        public Rectangle DrawRect { get; protected set; }
        public Boolean Hover { get; set; }
        protected Texture2D texture;
        protected Color color;
        protected Color hoverColor;

        protected Boolean drawText;

        public Button(Rectangle drawRect, LonharGame game, Color color, Color hoverColor, int textAlignment, String text)
        {
            textAlign = textAlignment;
            Text = text;
            this.color = color;
            this.hoverColor = hoverColor;
            DrawRect = drawRect;
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }
        public Button(Rectangle drawRect, LonharGame game, Color color)
        {
            DrawRect = drawRect;
            this.color = color;
            hoverColor = color;
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }
        /*public Button(Rectangle drawRect, LonharGame game, Color color, Color hoverColor, int textAlignment, String text)
        {
            textAlign = textAlignment;
            Text = text;
            this.color = color;
            this.hoverColor = hoverColor;
            DrawRect = drawRect;
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }*/
        public Button(Rectangle drawRect, Texture2D texture, int textAlignment, String text)
        {
            textAlign = textAlignment;
            Text = text;
        }

        public bool CheckHover(Rectangle mousePos)
        {
            return DrawRect.Contains(mousePos);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, DrawRect, (Hover) ? hoverColor : color);
            if (drawText)
            {
                //TODO Draw text
            }
        }
    }
}
