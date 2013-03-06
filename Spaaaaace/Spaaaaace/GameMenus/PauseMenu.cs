using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Spaaaaace.GameMenus
{
    class PauseMenu : GameMenu
    {
        public PauseMenu(LonharGame game):base(Direction.Horizontal)
        {
            Buttons.Add(new Button(new Rectangle(50, 50, 150, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Exit", game.ButtonFont));
            Buttons.Add(new Button(new Rectangle(210, 50, 150, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Continue", game.ButtonFont));
        }
    }
}
