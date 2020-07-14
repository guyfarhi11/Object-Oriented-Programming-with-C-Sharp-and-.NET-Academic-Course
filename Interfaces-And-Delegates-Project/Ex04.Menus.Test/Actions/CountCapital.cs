using System;
using System.Text;
using System.Text.RegularExpressions;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class CountCapital : IActionSelect
    {
        private const string k_OpeningSentence = "Hey, Please enter a sentence for which you want to count the capital letters";
        private const string k_InvalidInput = "Something went wrong, please try again";

        public void Action()
        {
            StringBuilder sentenceToPrint = new StringBuilder();
            Console.WriteLine(k_OpeningSentence);
            string sentence;
            while((sentence = Console.ReadLine()) == null)
            {
                Console.Write(k_InvalidInput);
            }

            sentenceToPrint.AppendFormat("The amount of capital letters is: {0}", countAmountOfCapitalLetters(sentence));
            Console.WriteLine(sentenceToPrint);
        }

        private int countAmountOfCapitalLetters(string i_String)
        {
            return Regex.Matches(i_String, "[A-Z]").Count;
        }
    }
}
