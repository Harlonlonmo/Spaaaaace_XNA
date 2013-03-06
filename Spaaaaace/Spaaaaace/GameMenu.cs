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
        public enum Direction { Vertical, Horizontal }

        public Boolean Open { get; set; }

        public List<Button> Buttons { get; protected set; }
        protected int buttonIndex;

        protected Rectangle oldMousePos;

        protected Boolean joysticChange;
        protected Boolean keyboardChange;
        protected Direction dir;

        public GameMenu(Direction dir)
        {
            this.dir = dir;
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
            if (dir == Direction.Vertical)
            {
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
            }
            else
            {
                if (Math.Abs(InputController.gamePadState.ThumbSticks.Left.X) > .5f)
                {
                    if (!joysticChange)
                    {
                        buttonIndex += Math.Sign(InputController.gamePadState.ThumbSticks.Left.X);
                        joysticChange = true;
                    }
                }
                else
                {
                    joysticChange = false;
                }

                if (InputController.KeyWasPressed(Keys.A) ||
                    InputController.KeyWasPressed(Keys.Left) ||
                    InputController.KeyWasPressed(Keys.D) ||
                    InputController.KeyWasPressed(Keys.Right))
                {
                    if (!keyboardChange)
                    {
                        if (InputController.KeyWasPressed(Keys.D) ||
                            InputController.KeyWasPressed(Keys.Right))
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
            }
            buttonIndex = (int)MathHelper.Clamp(buttonIndex, 0, Buttons.Count - 1);
            Buttons[buttonIndex].Hover = true;
        }

        public int getPressed()
        {
            if (InputController.KeyWasPressed(Keys.Enter) ||
                (InputController.MouseButtonWasPressed(InputController.MouseButton.Left) &&
                    Buttons[buttonIndex].CheckHover(InputController.getMouseRect()))
                || InputController.ButtonWasPressed(Microsoft.Xna.Framework.Input.Buttons.A))
            {
                return buttonIndex;
            }
            return -1;
        }
    }
}
