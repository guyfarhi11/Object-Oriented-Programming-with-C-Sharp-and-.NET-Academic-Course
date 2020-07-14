using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class ShowVersion : IActionSelect
    {
        private const string k_ReturnSentence = "Version: 20.2.4.30620";

        public void Action()
        {
            Console.WriteLine(k_ReturnSentence);
        }
    }
}
