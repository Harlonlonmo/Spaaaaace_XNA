﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spaaaaace.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaaaaace.GameStates
{
    public class Game : GameState
    {

        private PlayerShip playerShip;
        private List<Planet> planets;

        public Game(LonharGame game)
            : base(game)
        {
            playerShip = new PlayerShip(game, new Vector2(300, 150));
            planets = new List<Planet>();
            planets.Add(new Planet(game, 5500f, 6380000, new Vector2(300, 300)));
            planets.Add(new Planet(game, 5500f, 6380000, new Vector2(600, 300)));
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(PlayerIndex.One);
            if (ks.IsKeyDown(Keys.Escape) || gs.IsButtonDown(Buttons.Start))
            {
                game.ChangeState(new Menu(game));
            }
            foreach (Planet p in planets)
            {
                p.Update(gameTime);
            }
            playerShip.Update(gameTime, planets);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            playerShip.Draw(spriteBatch, gameTime);
            foreach (Planet p in planets)
            {
                p.DrawCenter(spriteBatch, gameTime);
            }
        }
    }
}