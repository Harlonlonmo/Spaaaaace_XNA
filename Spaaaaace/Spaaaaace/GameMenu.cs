using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Spaaaaace
{
    public class GameMenu
    {
        public Boolean Open { get; set; }

        public List<Button> Buttons { get; protected set; }
        protected int buttonIndex;

        protected Rectangle oldMousePos;

        protected Boolean joysticChange;
        protected Boolean keyboardChange;

        public GameMenu()
        {
            Buttons = new List<Button>();
            buttonIndex = 0;
        }

        public void Update(GameTime gameTime)
        {
            CheckButtons();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Button b in Buttons)
            {
                b.Draw(spriteBatch, gameTime);
            }
        }

        protected void CheckButtons()
        {
            Rectangle mousePos = new Rectangle(InputController.mouseState.X, InputController.mouseState.Y, 1, 1);
            Buttons[buttonIndex].Hover = false;
            if (mousePos != oldMousePos)
            {
                oldMousePos = mousePos;
                for (int i = 0; i < Buttons.Count; i++)
                {
                    if (Buttons[i].CheckHover(mousePos))
                    {
                        buttonIndex = i;
                    }
                }
            }
            if (Math.Abs(InputController.gamePadState.ThumbSticks.Left.Y) > .5f)
            {
                if (!joysticChange)
                {
                    buttonIndex -= Math.Sign(InputController.gamePadState.ThumbSticks.Left.Y);
                    joysticChange = true;
                }
            }
            else
            {
                joysticChange = false;
            }

            if (InputController.KeyWasPressed(Keys.W) ||
                InputController.KeyWasPressed(Keys.Up) ||
                InputController.KeyWasPressed(Keys.S) ||
                InputController.KeyWasPressed(Keys.Down))
            {
                if (!keyboardChange)
                {
                    if (InputController.KeyWasPressed(Keys.S) ||
                        InputController.KeyWasPressed(Keys.Down))
                    {
                        buttonIndex++;
                    }
                    else
                    {
                        buttonIndex--;
                    }
                    keyboardChange = true;
                }
            }
            else
            {
                keyboardChange = false;
            }
            buttonIndex = (int)MathHelper.Clamp(buttonIndex, 0, Buttons.Count - 1);
            Buttons[buttonIndex].Hover = true;
        }

        public int getPressed()
        {
            if (InputController.KeyWasPressed(Keys.Enter) ||
                (InputController.MouseButtonWasPressed(InputController.MOUSE_LEFT) &&
                    Buttons[buttonIndex].CheckHover(InputController.getMouseRect()))
                || InputController.ButtonWasPressed(Microsoft.Xna.Framework.Input.Buttons.A))
            {
                return buttonIndex;
            }
            return -1;
        }
    }
}
