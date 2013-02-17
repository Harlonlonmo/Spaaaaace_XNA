using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spaaaaace.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaaaaace.GameMenus;

namespace Spaaaaace.GameStates
{
    public class Game : GameState
    {

        private PlayerShip playerShip;
        private List<Planet> planets;

        private int index;
        private int planetSelectedIndex
        {
            get { return index; }
            set
            {
                planets[index].Selected = false;
                index = value % planets.Count;
                planets[index].Selected = true;
            }
        }

        private GameMenu pauseMenu;


        public Game(LonharGame game)
            : base(game)
        {
            playerShip = new PlayerShip(game, new Vector2(300, 150));
            planets = new List<Planet>();
            planets.Add(new Planet(game, 5500f, 6380000, new Vector2(300, 300)));
            planets.Add(new Planet(game, 5500f, 6380000, new Vector2(600, 300)));
            planets[0].Selected = true;
            pauseMenu = new PauseMenu(game);
        }

        public override void Update(GameTime gameTime)
        {
            if (pauseMenu.Open)
            {
                pauseMenu.Update(gameTime);
                if (InputController.KeyWasPressed(Keys.Escape) ||
                    InputController.ButtonWasPressed(Buttons.Start))
                {
                    pauseMenu.Open = false;
                }
                int pressed = pauseMenu.getPressed();
                if (pressed >= 0)
                {
                    if (pressed == 0)
                    {
                        game.ChangeState(new Menu(game));
                    }
                    else if (pressed == 1)
                    {
                        pauseMenu.Open = false;
                    }
                }
            }
            else
            {
                if (InputController.KeyWasPressed(Keys.Escape) ||
                    InputController.ButtonWasPressed(Buttons.Start))
                {
                    pauseMenu.Open = true;
                }
                if (InputController.KeyWasPressed(Keys.A) ||
                    InputController.ButtonWasPressed(Buttons.LeftShoulder))
                {
                    planetSelectedIndex++;
                }
                if (InputController.KeyWasPressed(Keys.D) ||
                    InputController.ButtonWasPressed(Buttons.RightShoulder))
                {
                    planetSelectedIndex++;
                }

                if (Math.Abs(InputController.gamePadState.ThumbSticks.Left.Y) > 0)
                {
                    planets[planetSelectedIndex].Radius += InputController.gamePadState.ThumbSticks.Left.Y * 10000;
                }

                if (InputController.keyboardState.IsKeyDown(Keys.W))
                {
                    planets[planetSelectedIndex].Radius += 10000;
                }
                if (InputController.keyboardState.IsKeyDown(Keys.S))
                {
                    planets[planetSelectedIndex].Radius -= 10000;
                }
                playerShip.Update(gameTime, planets);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(game.Textures.getTexture("Background"), new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            playerShip.Draw(spriteBatch, gameTime);
            foreach (Planet p in planets)
            {
                p.DrawCenter(spriteBatch, gameTime);
            }
            if (pauseMenu.Open)
            {
                pauseMenu.Draw(spriteBatch, gameTime);
            }
        }
    }
}
