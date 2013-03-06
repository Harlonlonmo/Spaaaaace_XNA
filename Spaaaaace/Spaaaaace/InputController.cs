using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Spaaaaace
{
    public class InputController
    {
        public enum MouseButton { Left, Midle, Right }

        public static MouseState mouseState { get; protected set; }
        public static KeyboardState keyboardState { get; protected set; }
        public static GamePadState gamePadState { get; protected set; }

        private static MouseState oldMs;
        private static GamePadState oldGs;
        private static KeyboardState oldKs;

        public static Boolean mouseChanged { get; protected set; }
        public static Boolean keyboardChanged { get; protected set; }
        public static Boolean gamePadChanged { get; protected set; }

        public static void Update(GameTime gameTime)
        {
            oldGs = gamePadState;
            oldKs = keyboardState;
            oldMs = mouseState;

            mouseState = Mouse.GetState();
            mouseChanged = mouseState != oldMs;
            keyboardState = Keyboard.GetState();
            keyboardChanged = keyboardState != oldKs;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            gamePadChanged = gamePadState != oldGs;
        }

        public static Boolean KeyWasPressed(Keys key)
        {
            if (!keyboardChanged) return false;
            return keyboardState.IsKeyDown(key) && oldKs.IsKeyUp(key);
        }

        public static Boolean ButtonWasPressed(Buttons button)
        {
            if (!gamePadChanged) return false;
            return gamePadState.IsButtonDown(button) && oldGs.IsButtonUp(button);
        }
        public static Boolean MouseButtonWasPressed(MouseButton button)
        {
            if (!mouseChanged) return false;
            if (button == MouseButton.Left) return mouseState.LeftButton == ButtonState.Pressed &&
                oldMs.LeftButton == ButtonState.Released;
            if (button == MouseButton.Midle) return mouseState.MiddleButton == ButtonState.Pressed &&
                oldMs.MiddleButton == ButtonState.Released;
            if (button == MouseButton.Right) return mouseState.RightButton == ButtonState.Pressed &&
                oldMs.RightButton == ButtonState.Released;
            return false;
        }

        public static Rectangle getMouseRect()
        {
            return new Rectangle(mouseState.X, mouseState.Y, 1, 1);
        }

        public static Point getMouseChange()
        {
            return new Point(oldMs.X - mouseState.X, oldMs.Y - mouseState.Y);
        }
    }
}
