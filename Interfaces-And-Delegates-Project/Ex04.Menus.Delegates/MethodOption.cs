using System;

namespace Ex04.Menus.Delegates
{
    public class MethodOption : MenuItem
    {
        private const int k_TimeOutPeriod = 2000;

        public event Action OptionChosen;

        public MethodOption(string i_Name)
            : base(i_Name)
        {
        }

        internal override void OnOptionChosen()
        {
            if (OptionChosen != null)
            {
                Console.Clear();
                OptionChosen.Invoke();
            }

            System.Threading.Thread.Sleep(k_TimeOutPeriod);
            Father.OnOptionChosen();
        }
    }
}
