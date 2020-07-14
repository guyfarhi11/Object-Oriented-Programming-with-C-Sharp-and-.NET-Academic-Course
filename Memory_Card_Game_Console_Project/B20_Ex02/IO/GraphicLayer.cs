using System;
using System.Text;
using System.Collections.Generic;
using B20_Ex02.GameLogic;

namespace B20_Ex02.IO
{
    internal class GraphicLayer
    {
        private const int k_FirstAsciChar = 65;
        private const int k_OneAsciChar = 48;
        private const string k_Quit = "Q";
        private const int k_PlayerOneIndex = 1;
        private const int k_PlayerTwoIndex = 2;
        private const int k_NumberOfPlayers = 2;
        private readonly Board r_GameBoard;
        private readonly List<Player> r_GamePlayers;
        private readonly AiComputer r_ComputerPlayer;
        private readonly bool r_PlayWithComputer;

        internal GraphicLayer()
        {
            r_PlayWithComputer = false;
            askForTypeOfGame(out int oTypeOfGame, out int oWidthOfBoard, out int oHeightOfBoard);
            if (oTypeOfGame == 1)
            {
                r_PlayWithComputer = true;
            }

            r_GameBoard = new Board(oWidthOfBoard, oHeightOfBoard);
            r_GamePlayers = new List<Player>(k_NumberOfPlayers);
            r_GamePlayers.Add(new Player(false, k_PlayerOneIndex, r_GameBoard));
            r_GamePlayers.Add(new Player(r_PlayWithComputer, k_PlayerTwoIndex, r_GameBoard));
            r_ComputerPlayer = new AiComputer(r_GameBoard);
        }

        internal bool RunGame()
        {
            bool endGame = false;
            int currentPlayer = new Random().Next(0, k_NumberOfPlayers);
            SingleGame game = new SingleGame(r_GameBoard, r_GamePlayers, r_PlayWithComputer, r_ComputerPlayer);
            bool anotherGame;
            bool qFlag = false;
            bool isGoodCardMatch = false;
            while (!endGame)
            {
                Console.Clear();
                printBoard(r_GameBoard);
                string firstCard, secondCard;
                bool computerTurn = r_PlayWithComputer && (currentPlayer == k_PlayerOneIndex);
                if (computerTurn)
                {
                    computerChoice(out firstCard, out secondCard);
                }
                else
                {
                    playerChoice(currentPlayer, out firstCard, out secondCard);
                }

                if (firstCard.Equals(k_Quit) || secondCard.Equals(k_Quit))
                {
                    qFlag = true;
                    break;
                }

                endGame = game.RunTurnPlayer(currentPlayer, firstCard, secondCard, ref isGoodCardMatch);
                if (isGoodCardMatch)
                {
                    goodMatch(r_GamePlayers[currentPlayer].GetPlayerNumber());
                }
                else
                {
                    badMatch();
                }

                currentPlayer = 1 - currentPlayer;
            }

            if (qFlag)
            {
                anotherGame = false;
            }
            else
            {
                game.GetWinner(out int winner, out int winnerScore);
                announceWinner(winner, winnerScore, out anotherGame);
            }

            return anotherGame;
        }

        private static void askForTypeOfGame(out int i_OChoiceOfGame, out int i_OWidthOfBoard, out int i_OHeightOfBoard)
        {
            StringBuilder startSentence = new StringBuilder();
            StringBuilder inValidChoiceSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            StringBuilder optionSizeSentence = new StringBuilder();
            startSentence.AppendFormat("Welcome to the memory game, in each step after starting type Q to exit.{0}", Environment.NewLine);
            startSentence.AppendFormat("Please choose one of the following options:{0}", Environment.NewLine);
            inValidChoiceSentence.AppendFormat("Input is not valid please choose only one of the following options:{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 1 for a game against the AI computer{0}", Environment.NewLine);
            optionsSentence.AppendFormat("press 2 for a game against another player");
            startSentence.Append(optionsSentence);
            inValidChoiceSentence.Append(optionsSentence);
            Console.WriteLine(startSentence);
            while (!int.TryParse(Console.ReadLine(), out i_OChoiceOfGame) || ((i_OChoiceOfGame != 1) && (i_OChoiceOfGame != 2)))
            {
                Console.WriteLine(inValidChoiceSentence);
            }

            Console.Clear();
            optionSizeSentence.AppendFormat("Nice! Please enter requested size of board by the following instructions:{0}", Environment.NewLine);
            optionSizeSentence.AppendFormat("1. The requested width and the requested height have to be at most 6 each{0}", Environment.NewLine);
            optionSizeSentence.AppendFormat("2. The requested width and the requested height have to be at least 4 each{0}", Environment.NewLine);
            optionSizeSentence.AppendFormat("3. Number of cells cant be odd, i.e size 5X5 is not valid");
            Console.WriteLine(optionSizeSentence);
            while (true)
            {
                Console.WriteLine("Please type width according the instructions");
                while (!int.TryParse(Console.ReadLine(), out i_OWidthOfBoard) || (i_OWidthOfBoard < 4) || (i_OWidthOfBoard > 6))
                {
                    Console.WriteLine("Invalid width please type according the instructions");
                }

                Console.WriteLine("Please type height according the instructions");
                while (!int.TryParse(Console.ReadLine(), out i_OHeightOfBoard) || (i_OHeightOfBoard < 4)
                                                                           || (i_OHeightOfBoard > 6))
                {
                    Console.WriteLine("Invalid height please type according the instructions");
                }

                if (i_OWidthOfBoard == 5 && i_OHeightOfBoard == 5)
                {
                    Console.WriteLine(" Number of cells cant be odd, i.e size 5X5 is not valid");
                }
                else
                {
                    break;
                }
            }
        }

        private static void goodMatch(int i_PlayerNumber)
        {
            StringBuilder congratulationsSentence = new StringBuilder();
            congratulationsSentence.AppendFormat("Congratulations player {0}!", i_PlayerNumber);
            congratulationsSentence.AppendFormat("you've earned 2 points");
            Console.WriteLine(congratulationsSentence);
            System.Threading.Thread.Sleep(2000);
        }

        private static void badMatch()
        {
            StringBuilder congratulationsSentence = new StringBuilder();
            congratulationsSentence.AppendFormat("Almost done it! ");
            congratulationsSentence.AppendFormat("remember the cards!");
            Console.WriteLine(congratulationsSentence);
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        private static StringBuilder lineOfEqual(int i_LineLength)
        {
            StringBuilder lineToPrint = new StringBuilder();
            lineToPrint.Append("   ");
            lineToPrint.Append("=====");
            for (int i = 0; i < i_LineLength - 1; i++)
            {
                lineToPrint.Append("====");
            }

            return lineToPrint;
        }

        private static StringBuilder firstLineToPrint(int i_Size)
        {
            StringBuilder lineToPrint = new StringBuilder();
            lineToPrint.Append("    ");
            char charToAdd = 'A';
            for (int i = 0; i < i_Size; i++)
            {
                if (i == 0)
                {
                    lineToPrint.Append(string.Format(" {0}  ", (char)(charToAdd + i)));
                }
                else
                {
                    lineToPrint.Append(string.Format(" {0}  ", (char)(charToAdd + i)));
                }
            }

            return lineToPrint;
        }

        private void computerChoice(out string i_OFirstCard, out string i_OSecondCard)
        {
            askForChoice(r_ComputerPlayer.GetPlayerNumber(), "first", r_ComputerPlayer.AiChoice(string.Empty), string.Empty, out i_OFirstCard);
            askForChoice(r_ComputerPlayer.GetPlayerNumber(), "second", r_ComputerPlayer.AiChoice(i_OFirstCard), i_OFirstCard, out i_OSecondCard);
        }

        private void playerChoice(int i_CurrentPlayer, out string i_OFirstCard, out string i_OSecondCard)
        {
            askForChoice(r_GamePlayers[i_CurrentPlayer].GetPlayerNumber(), "first", string.Empty, string.Empty, out i_OFirstCard);
            if (i_OFirstCard.Equals(k_Quit))
            {
                i_OSecondCard = k_Quit;
                return;
            }

            askForChoice(r_GamePlayers[i_CurrentPlayer].GetPlayerNumber(), "second", string.Empty, i_OFirstCard, out i_OSecondCard);
        }

        private void askForChoice(int i_PlayerNumber, string i_TurnInfo, string i_ComputerChoice, string i_FirstChoice, out string i_OCardPosition)
        {
            Console.WriteLine("Player {0} turn.{1}Please choose {2} position of a card to flip", i_PlayerNumber, Environment.NewLine, i_TurnInfo);
            i_OCardPosition = i_ComputerChoice;
            if (i_ComputerChoice.Equals(string.Empty))
            {
                while ((!r_GameBoard.ValidateLocation(i_OCardPosition = Console.ReadLine()) || i_OCardPosition.Equals(i_FirstChoice)) && !i_OCardPosition.Equals(k_Quit))
                {
                    if (i_OCardPosition.Equals(i_FirstChoice))
                    {
                        Console.WriteLine(
                            "You can't choose {0} twice, please choose again", i_FirstChoice);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Please enter a valid location only! (capital letter and a number from range, for example A2)");
                    }
                }
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
            }

            if (!i_OCardPosition.Equals(k_Quit))
            {
                Console.Clear();
                if (!i_OCardPosition.Equals(k_Quit))
                {
                    r_GameBoard.FlipTemporaryCard(i_OCardPosition);
                    printBoard(r_GameBoard);
                }

                Console.WriteLine("Player {0}, {1} choice turned out to be {2}!", i_PlayerNumber, i_TurnInfo, r_GameBoard.GetCardValueByLocation(i_OCardPosition));
            }
        }

        private void announceWinner(int i_Winner, int i_WinningScore, out bool i_OAnotherGame)
        {
            StringBuilder closingSentence = new StringBuilder();
            StringBuilder inValidInputSentence = new StringBuilder();
            StringBuilder optionsSentence = new StringBuilder();
            if (i_Winner == 0)
            {
                closingSentence.AppendFormat("That was really close!!! Game ended with a tie!{0}", Environment.NewLine);
            }
            else
            {
                closingSentence.AppendFormat(
                    "Well done player {0}! You won with an amazing score of {1} points!{2}",
                    i_Winner,
                    i_WinningScore,
                    Environment.NewLine);
                closingSentence.AppendFormat(
                    "Player {0}! You lost with a score of {1} points!{2}",
                    3 - i_Winner,
                    (r_GameBoard.BoardHeight * r_GameBoard.BoardWidth) - i_WinningScore,
                    Environment.NewLine);
            }

            closingSentence.AppendFormat("Would you like to play again?{0}", Environment.NewLine);
            inValidInputSentence.AppendFormat("Input is not valid please choose only one of the following options:{0}", Environment.NewLine);
            optionsSentence.Append("please type yes or no");
            closingSentence.Append(optionsSentence);
            inValidInputSentence.Append(optionsSentence);
            Console.Clear();
            printBoard(r_GameBoard);
            Console.WriteLine(closingSentence);
            bool enteredLoop = false;
            string flag = string.Empty;
            while ((!flag.Equals("yes") && !flag.Equals("no")) || !enteredLoop)
            {
                if (!enteredLoop)
                {
                    enteredLoop = true;
                }
                else
                {
                    Console.WriteLine(inValidInputSentence);
                }

                flag = Console.ReadLine();
                while (flag == null)
                {
                    Console.WriteLine(inValidInputSentence);
                    flag = Console.ReadLine();
                }
            }

            i_OAnotherGame = flag.Equals("yes");
        }

        private void printBoard(Board i_Board)
        {
            StringBuilder allBoard = new StringBuilder();
            allBoard.Append(firstLineToPrint(i_Board.BoardWidth));
            allBoard.Append(Environment.NewLine);
            for (int i = 1; i <= i_Board.BoardHeight; i++)
            {
                allBoard.Append(lineOfEqual(i_Board.BoardWidth));
                allBoard.Append(Environment.NewLine);
                allBoard.Append(string.Format(" {0} ", (char)(i + k_OneAsciChar)));
                for (int j = k_FirstAsciChar; j < (i_Board.BoardWidth + k_FirstAsciChar); j++)
                {
                    string stringKeyToCheck = string.Format("{0}{1}", (char)j, (char)(i + k_OneAsciChar));
                    Card cardToCheck = i_Board.GetCardByLocation(stringKeyToCheck);
                    if (cardToCheck.IsShow == true)
                    {
                        allBoard.Append(string.Format("| {0} ", cardToCheck.CardValue));
                    }
                    else
                    {
                        allBoard.Append("|   ");
                    }
                }

                allBoard.Append("|");
                allBoard.Append(Environment.NewLine);
            }

            allBoard.Append(lineOfEqual(i_Board.BoardWidth));
            Console.WriteLine(allBoard);
        }
    }
}
