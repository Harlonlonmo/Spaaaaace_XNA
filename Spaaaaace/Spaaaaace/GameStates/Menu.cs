using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaaaaace.GameStates
{
    public class Menu : GameState
    {

        private Button[] buttons;
        private int buttonIndex;
        private Boolean joysticChange;
        private Boolean keyboardChange;

        private Rectangle oldMousePos;

        public Menu(LonharGame game):base(game)
        {
            buttons = new Button[3]{
                new Button(new Rectangle(50, 50, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Test 1"),
                new Button(new Rectangle(50, 110, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Test 2"),
                new Button(new Rectangle(50, 170, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Test 3")
            };
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(PlayerIndex.One);
            CheckButtons(ms, ks, gs);

            if (ks.IsKeyDown(Keys.Enter) || 
                (ms.LeftButton == ButtonState.Pressed &&
                    buttons[buttonIndex].CheckHover(new Rectangle(ms.X, ms.Y, 1, 1)))
                || gs.IsButtonDown(Buttons.A))
            {
                if (buttonIndex == 0)
                {
                    game.ChangeState(new Game(game));
                }
                else if (buttonIndex == 1)
                {
                    game.ChangeState(new Editor(game));
                }
                else if (buttonIndex == 2)
                {
                    game.Exit();
                }
            }
            if (ks.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.Draw(spriteBatch, gameTime);
            }
        }

        private void CheckButtons(MouseState ms, KeyboardState ks, GamePadState gs)
        {
            Rectangle mousePos = new Rectangle(ms.X, ms.Y, 1, 1);
            buttons[buttonIndex].Hover = false;
            if (mousePos != oldMousePos)
            {
                oldMousePos = mousePos;
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].CheckHover(mousePos))
                    {
                        buttonIndex = i;
                    }
                }
            }
            if (Math.Abs(gs.ThumbSticks.Left.Y) > .5f)
            {
                if (!joysticChange)
                {
                    buttonIndex -= Math.Sign(gs.ThumbSticks.Left.Y);
                    joysticChange = true;
                }
            }
            else
            {
                joysticChange = false;
            }
            
            if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                if (!keyboardChange)
                {
                    if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
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
            buttonIndex = (int)MathHelper.Clamp(buttonIndex, 0, buttons.Length - 1);
            buttons[buttonIndex].Hover = true;
        }

    }
}
