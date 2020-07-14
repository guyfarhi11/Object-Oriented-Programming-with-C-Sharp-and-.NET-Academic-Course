namespace B20_Ex02.GameLogic
{
    internal class Card
    {
        private readonly char r_CardValue;
        private bool m_Shown;

        internal Card(char i_CardValue)
        {
            m_Shown = false;
            r_CardValue = i_CardValue;
        }

        internal char CardValue
        {
            get { return r_CardValue; }
        }

        internal void FlipCard()
        {
            m_Shown = !m_Shown;
        }

        internal bool IsShow
        {
            get { return m_Shown; }
            set { m_Shown = value; }
        }
    }
}