using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EX05.FormsUI
{
    internal class GameButton : Button
    {
        private readonly string r_Location;

        internal GameButton(string i_Location) : base()
        {
            r_Location = i_Location;
        }

        internal string Location
        {
            get
            {
                return r_Location;
            }
        }
    }
}