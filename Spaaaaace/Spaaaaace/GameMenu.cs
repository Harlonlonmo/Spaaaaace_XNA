using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spaaaaace
{
    public class GameMenu
    {
        public Boolean Open { get; set; }

        public List<Button> Buttons { get; protected set; }

        public GameMenu()
        {
            Buttons = new List<Button>();
        }

    }
}
