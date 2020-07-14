using B20_Ex02.GameLogic;

namespace B20_Ex02.IO
{
    internal class Player
    {
        private readonly int r_PlayerNumber;
        private int m_PlayerPoints;

        internal Player(bool i_IsComputer, int i_PlayerNumber, Board i_GameBoard)
        {
            r_PlayerNumber = i_PlayerNumber;
            m_PlayerPoints = 0;
        }

        internal void AddPointsToPlayer(Board i_GameBoard)
        {
            m_PlayerPoints += 2;
            i_GameBoard.AmountOfUnOpenCard -= 2;
        }

        internal int GetPlayerPoints()
        {
            return m_PlayerPoints;
        }

        internal int GetPlayerNumber()
        {
            return r_PlayerNumber;
        }
    }
}