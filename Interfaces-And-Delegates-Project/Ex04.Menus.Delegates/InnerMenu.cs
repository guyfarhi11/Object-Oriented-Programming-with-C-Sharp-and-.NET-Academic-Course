using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class InnerMenu : MenuItem
    {
        private const string k_ChoiceSentence = "Menu, please choose one of the following:";
        private const string k_Option = "Press {0} for {1}{2}";
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";
        private const string k_InvalidInput = "Input wasn't valid please choose only one of the following:";
        private const int k_Zero = 0;
        private readonly List<MenuItem> r_ItemList;

        public InnerMenu(string i_Name)
            : base(i_Name)
        {
            r_ItemList = new List<MenuItem>();
        }

        public void AddToMenu(MenuItem i_NewItem)
        {
            i_NewItem.Father = this;
            r_ItemList.Add(i_NewItem);
        }

        private void show()
        {
            Console.Clear();
            StringBuilder toShow = new StringBuilder();
            StringBuilder error = new StringBuilder();
            StringBuilder options = new StringBuilder();
            toShow.AppendFormat("{0} {1}{2}", r_ItemName, k_ChoiceSentence, Environment.NewLine);
            if (Father == null)
            {
                options.AppendFormat(k_Option, k_Zero, k_Exit, Environment.NewLine);
            }
            else
            {
                options.AppendFormat(k_Option, k_Zero, k_Back, Environment.NewLine);
            }

            error.AppendLine(k_InvalidInput);
            int i = 1;
            foreach (MenuItem item in r_ItemList)
            {
                options.AppendFormat(k_Option, i++, item.ItemName, Environment.NewLine);
            }

            toShow.Append(options);
            error.Append(options);
            Console.Write(toShow);
            askForChoice(error);
        }

        private void askForChoice(StringBuilder i_Error)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice > r_ItemList.Count || choice < 0)
            {
                Console.Clear();
                Console.Write(i_Error);
            }

            if (choice == 0)
            {
                if (Father != null)
                {
                    Father.OnOptionChosen();
                }
                else
                {
                    return;
                }
            }
            else
            {
                r_ItemList[choice - 1].OnOptionChosen();
            }
        }

        internal override void OnOptionChosen()
        {
            if (r_ItemList.Count > 0)
            {
                show();
            }
        }
    }
}
