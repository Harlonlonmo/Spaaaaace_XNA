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

        private GameMenu gameMenu;

        public Menu(LonharGame game):base(game)
        {
            gameMenu = new GameMenu(GameMenu.Direction.Vertical);
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 50, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Play", game.ButtonFont));
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 110, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Editor", game.ButtonFont));
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 170, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Exit", game.ButtonFont));
        }

        public override void Update(GameTime gameTime)
        {

            gameMenu.Update(gameTime);

            int pressed = gameMenu.getPressed();
            if (pressed >= 0)
            {
                if (pressed == 0)
                {
                    game.ChangeState(new Game(game));
                }
                else if (pressed == 1)
                {
                    game.ChangeState(new Editor(game));
                }
                else if (pressed == 2)
                {
                    game.Exit();
                }
            }
            if (InputController.KeyWasPressed(Keys.Escape) ||
                InputController.ButtonWasPressed(Buttons.Start))
            {
                game.Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(game.Textures.getTexture("Background"), new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);
            gameMenu.Draw(spriteBatch, gameTime);
        }

        

    }
}
