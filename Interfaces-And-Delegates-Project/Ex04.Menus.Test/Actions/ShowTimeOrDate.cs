using System;
using System.Text;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class ShowTimeOrDate : IActionSelect
    {
        private readonly TimeTypes.eTypeOfTime r_TypeOfTime;

        public ShowTimeOrDate(TimeTypes.eTypeOfTime i_TypeOfTime)
        {
            r_TypeOfTime = i_TypeOfTime;
        }

        public void Action()
        {
            StringBuilder stringToPrint = new StringBuilder();
            switch (r_TypeOfTime)
            {
                case TimeTypes.eTypeOfTime.Date:
                    stringToPrint.Append(DateTime.Today.ToString("yyyy-MM-dd"));
                    break;
                case TimeTypes.eTypeOfTime.Time:
                    stringToPrint.Append(DateTime.Now.ToString("HH-mm-ss"));
                    break;
            }

            Console.WriteLine(stringToPrint.ToString());
        }
    }
}
