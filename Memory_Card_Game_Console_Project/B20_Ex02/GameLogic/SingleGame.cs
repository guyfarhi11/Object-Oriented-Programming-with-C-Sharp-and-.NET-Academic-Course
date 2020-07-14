using System.Collections.Generic;
using B20_Ex02.IO;

namespace B20_Ex02.GameLogic
{
    internal class SingleGame
    {
        private readonly Board r_GameBoard;
        private readonly List<Player> r_GamePlayers;
        private readonly AiComputer r_ComputerPlayer;
        private readonly bool r_PlayWithComputer;

        internal SingleGame(Board i_GameBoard, List<Player> i_GamePlayers, bool i_PlayWithComputer, AiComputer i_ComputerPlayer)
        {
            r_GameBoard = i_GameBoard;
            r_GamePlayers = i_GamePlayers;
            r_PlayWithComputer = i_PlayWithComputer;
            r_ComputerPlayer = i_ComputerPlayer;
        }

        internal bool RunTurnPlayer(int i_PlayerIndex, string i_FirstCard, string i_SecondCard, ref bool i_IsGoodMatch)
        {
            bool returnValue = false;
            bool computerTurn = r_PlayWithComputer && (i_PlayerIndex == 1);
            i_IsGoodMatch = computerTurn ?
                                r_GameBoard.CheckTheCards(i_FirstCard, i_SecondCard, r_ComputerPlayer.GetPlayerNumber()) :
                                r_GameBoard.CheckTheCards(i_FirstCard, i_SecondCard, r_GamePlayers[i_PlayerIndex].GetPlayerNumber());
            if (i_IsGoodMatch)
            {
                if (r_PlayWithComputer)
                {
                    r_ComputerPlayer.RemoveFromAiMemory(i_FirstCard, i_SecondCard);
                }

                r_GamePlayers[i_PlayerIndex].AddPointsToPlayer(r_GameBoard);
                if (r_GameBoard.CheckIfBoardIsClear())
                {
                    returnValue = true;
                }
            }
            else
            {
                if (r_PlayWithComputer)
                {
                    r_ComputerPlayer.AddToAiMemory(i_FirstCard, i_SecondCard);
                }
            }

            return returnValue;
        }

        internal void GetWinner(out int i_OWinner, out int i_OWinnerScore)
        {
            if (r_GamePlayers[0].GetPlayerPoints() > r_GamePlayers[1].GetPlayerPoints())
            {
                i_OWinner = r_GamePlayers[0].GetPlayerNumber();
                i_OWinnerScore = r_GamePlayers[0].GetPlayerPoints();
            }
            else
            {
                if (r_GamePlayers[0].GetPlayerPoints() < r_GamePlayers[1].GetPlayerPoints())
                {
                    i_OWinner = r_GamePlayers[1].GetPlayerNumber();
                    i_OWinnerScore = r_GamePlayers[1].GetPlayerPoints();
                }
                else
                {
                    i_OWinner = 0;
                    i_OWinnerScore = 0;
                }
            }
        }
    }
}
