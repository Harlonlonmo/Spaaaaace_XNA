using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;
using Microsoft.Xna.Framework.Input;
using Spaaaaace.GameMenus;

namespace Spaaaaace.GameStates
{
    class Editor : GameState
    {

        private Rectangle camera;

        private GameMenu editorMenu;

        private Vector2 cursorPos;

        private GameMenu pauseMenu;

        public Editor(LonharGame game)
            : base(game)
        {
            camera = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            cursorPos = new Vector2();
            editorMenu = new GameMenu();
            editorMenu.Buttons.Add(new Button(new Rectangle(650, 50, 125, 50), game, Color.Thistle, Color.White));
            editorMenu.Buttons.Add(new Button(new Rectangle(650, 110, 125, 50), game, Color.Thistle, Color.White));

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
            else if (editorMenu.Open)
            {
                editorMenu.Update(gameTime);
                if (InputController.ButtonWasPressed(Buttons.B) ||
                    InputController.KeyWasPressed(Keys.Escape) ||
                    InputController.KeyWasPressed(Keys.M))
                {
                    editorMenu.Open = false;
                }
                int pressed = editorMenu.getPressed();
                if (pressed >= 0)
                {
                    if (pressed == 0)
                    {
                        // button 0
                    }
                    else if (pressed == 1)
                    {
                        // button 1
                    }
                    else if (pressed == 2)
                    {
                        // button 2
                    }
                    editorMenu.Open = false;
                }
            }
            else
            {
                if (InputController.KeyWasPressed(Keys.Escape) ||
                    InputController.ButtonWasPressed(Buttons.Start))
                {
                    pauseMenu.Open = true;
                }
                if (InputController.KeyWasPressed(Keys.M))
                {
                    editorMenu.Open = true;
                }
                if (InputController.mouseChanged)
                {
                    cursorPos = new Vector2(InputController.getMouseRect().X, InputController.getMouseRect().Y);
                    cursorPos.X = MathHelper.Clamp(cursorPos.X, 0, camera.Width);
                    cursorPos.Y = MathHelper.Clamp(cursorPos.Y, 0, camera.Height);
                    if (InputController.mouseState.RightButton == ButtonState.Pressed)
                    {
                        camera.Offset(InputController.getMouseChange());
                    }
                }
                float cursormove = (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
                cursorPos.X = MathHelper.Clamp(cursorPos.X + InputController.gamePadState.ThumbSticks.Left.X * cursormove, 0, camera.Width);
                cursorPos.Y = MathHelper.Clamp(cursorPos.Y - InputController.gamePadState.ThumbSticks.Left.Y * cursormove, 0, camera.Height);

                camera.Offset(
                    (int)(InputController.gamePadState.ThumbSticks.Right.X * cursormove),
                    (int)(-InputController.gamePadState.ThumbSticks.Right.Y * cursormove));

                if (InputController.ButtonWasPressed(Buttons.B))
                {
                    editorMenu.Open = true;
                }

            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(game.Textures.getTexture("Background"), new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            Vector2 cursorGridPos = gridToPoint(pointToGrid(cursorPos, 32), 32);
            Primitives2D.FillRectangle(spriteBatch, new Rectangle((int)cursorGridPos.X, (int)cursorGridPos.Y, 32, 32), Color.White);
            //Primitives2D.FillRectangle(spriteBatch, new Rectangle((int)pointToGrid(cursorPos, 32).X*32, (int)pointToGrid(cursorPos, 32).Y*32, 32, 32), Color.Tomato);
            DrawGrid(spriteBatch, 32);
            if (editorMenu.Open)
            {
                editorMenu.Draw(spriteBatch, gameTime);
            }
            if (pauseMenu.Open)
            {
                pauseMenu.Draw(spriteBatch, gameTime);
            }
        }

        private void DrawGrid(SpriteBatch spriteBatch, int size)
        {
            for (int i = 0; i < camera.Width / size + 1; i++)
            {
                Primitives2D.DrawLine(
                    spriteBatch,
                    new Vector2(i * size - ((camera.X + size) % size), 0),
                    new Vector2(i * size - ((camera.X + size) % size), camera.Height),
                    Color.White);
            }
            for (int i = 0; i < camera.Height / size + 1; i++)
            {
                Primitives2D.DrawLine(
                    spriteBatch,
                    new Vector2(0, i * size - ((camera.Y + size) % size)),
                    new Vector2(camera.Width, i * size - ((camera.Y + size) % size)),
                    Color.White);
            }
        }

        private Point pointToGrid(Vector2 pos, int gridSize)
        {
            return new Point(
                (int)Math.Floor((pos.X + camera.X) / gridSize),
                (int)Math.Floor((pos.Y + camera.Y) / gridSize));
        }

        private Vector2 gridToPoint(Point pos, int gridSize)
        {
            return new Vector2(
                pos.X * gridSize - camera.X,
                pos.Y * gridSize - camera.Y);
        }
    }
}
