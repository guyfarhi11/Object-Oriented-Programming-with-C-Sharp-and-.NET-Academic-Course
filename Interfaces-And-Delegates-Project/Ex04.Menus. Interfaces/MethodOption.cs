using System;

namespace Ex04.Menus.Interfaces
{
    public class MethodOption : MenuItem
    {
        private const int k_TimeOutPeriod = 2000;
        private IActionSelect m_Action;

        public MethodOption(string i_Name)
            : base(i_Name)
        {
        }

        public IActionSelect Action
        {
            get
            {
                return m_Action;
            }

            set
            {
                m_Action = value;
            }
        }

        internal override void DoAction()
        {
            if (m_Action != null)
            {
                Console.Clear();
                m_Action.Action();
            }

            System.Threading.Thread.Sleep(k_TimeOutPeriod);
            Father.DoAction();
        }
    }
}
