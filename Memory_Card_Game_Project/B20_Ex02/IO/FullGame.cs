using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B20_Ex02.IO;

namespace B20_Ex02.GameLogic
{
    internal class FullGame
    {
        internal static void RunFullGame()
        {
            GraphicLayer game = new GraphicLayer();
            while (game.RunGame())
            {
                Console.Clear();
                game = new GraphicLayer();
            }
        }
    }
}