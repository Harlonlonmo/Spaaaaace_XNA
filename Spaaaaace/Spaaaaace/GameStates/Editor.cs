using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;
using Microsoft.Xna.Framework.Input;

namespace Spaaaaace.GameStates
{
    class Editor : GameState
    {

        private Rectangle camera;
        private MouseState oldMs;
        private GamePadState oldGs;

        private GameMenu editorMenu;

        private Vector2 cursorPos;

        public Editor(LonharGame game)
            : base(game)
        {
            camera = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            cursorPos = new Vector2();
            editorMenu = new GameMenu();
            editorMenu.Buttons.Add(new Button(new Rectangle(650, 50, 125, 50), game, Color.Thistle));
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            Boolean mouseChanged = ms != oldMs;
            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(PlayerIndex.One);
            Boolean gamePadChanged = gs != oldGs;
            if (editorMenu.Open)
            {
                if (gs.Buttons.B == ButtonState.Pressed && oldGs.Buttons.B == ButtonState.Released)
                {
                    editorMenu.Open = true;
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Escape) || gs.IsButtonDown(Buttons.Start))
                {
                    game.ChangeState(new Menu(game));
                }
                if (mouseChanged)
                {
                    cursorPos = new Vector2(ms.X, ms.Y);
                    cursorPos.X = MathHelper.Clamp(ms.X, 0, camera.Width);
                    cursorPos.Y = MathHelper.Clamp(ms.Y, 0, camera.Height);
                    if (ms.RightButton == ButtonState.Pressed)
                    {
                        camera.Offset(new Point(oldMs.X - ms.X, oldMs.Y - ms.Y));

                    }
                    oldMs = ms;
                }
                if (gamePadChanged)
                {

                    float cursormove = (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
                    cursorPos.X = MathHelper.Clamp(cursorPos.X + gs.ThumbSticks.Left.X * cursormove, 0, camera.Width);
                    cursorPos.Y = MathHelper.Clamp(cursorPos.Y - gs.ThumbSticks.Left.Y * cursormove, 0, camera.Height);

                    camera.Offset((int)(gs.ThumbSticks.Right.X * cursormove), (int)(-gs.ThumbSticks.Right.Y * cursormove));

                    if (gs.Buttons.B == ButtonState.Pressed && oldGs.Buttons.B == ButtonState.Released)
                    {
                        editorMenu.Open = true;
                    }
                }
            }
            oldMs = ms;
            oldGs = gs;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 cursorGridPos = gridToPoint(pointToGrid(cursorPos, 32), 32);
            Primitives2D.FillRectangle(spriteBatch, new Rectangle((int)cursorGridPos.X, (int)cursorGridPos.Y, 32, 32), Color.White);
            //Primitives2D.FillRectangle(spriteBatch, new Rectangle((int)pointToGrid(cursorPos, 32).X*32, (int)pointToGrid(cursorPos, 32).Y*32, 32, 32), Color.Tomato);
            DrawGrid(spriteBatch, 32);
            if (editorMenu.Open)
            {
                foreach (Button b in editorMenu.Buttons)
                {
                    b.Draw(spriteBatch, gameTime);
                }
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
