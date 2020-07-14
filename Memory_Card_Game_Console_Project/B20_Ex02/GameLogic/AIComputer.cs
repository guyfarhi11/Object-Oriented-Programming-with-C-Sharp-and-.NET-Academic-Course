using System.Collections.Generic;

namespace B20_Ex02.GameLogic
{
    internal class AiComputer
    {
        private const int k_AiLevel = 6;
        private const int k_PlayerNumber = 2;
        private readonly Queue<string> r_AiMemory;
        private readonly Board r_GameBoard;

        internal AiComputer(Board i_GameBoard)
        {
            r_AiMemory = new Queue<string>();
            r_GameBoard = i_GameBoard;
        }

        internal string AiChoice(string i_FirstCard)
        {
            string choice = string.Empty;
            if (!i_FirstCard.Equals(string.Empty))
            {
                bool potentialFlag = false;
                foreach (string potentialCardPosition in r_AiMemory)
                {
                    if (r_GameBoard.GetCardValueByLocation(potentialCardPosition) == r_GameBoard.GetCardValueByLocation(i_FirstCard))
                    {
                        choice = potentialCardPosition;
                        potentialFlag = true;
                        break;
                    }
                }

                if (!potentialFlag)
                {
                    chooseRandomly(out choice);
                }
            }
            else
            {
                if (!lookForMatchInMemory(out choice))
                {
                    chooseRandomly(out choice);
                }
            }

            return choice;
        }

        private void chooseRandomly(out string i_OChoice)
        {
            i_OChoice = r_GameBoard.ChooseCardRandomly();
            if (i_OChoice == string.Empty)
            {
                i_OChoice = r_AiMemory.Dequeue();
            }
        }

        private bool lookForMatchInMemory(out string i_Choice)
        {
            i_Choice = string.Empty;
            int counter = 0;
            bool returnValue = false;
            if (r_AiMemory.Count > 1)
            {
                while (r_AiMemory.Count > counter)
                {
                    i_Choice = r_AiMemory.Dequeue();
                    foreach (string potentialCardPosition in r_AiMemory)
                    {
                        if (r_GameBoard.GetCardValueByLocation(potentialCardPosition) == r_GameBoard.GetCardValueByLocation(i_Choice))
                        {
                            returnValue = true;
                            break;
                        }
                    }

                    if (returnValue)
                    {
                        break;
                    }

                    r_AiMemory.Enqueue(i_Choice);
                    counter++;
                }

                while (r_AiMemory.Count - counter > 0)
                {
                    r_AiMemory.Enqueue(r_AiMemory.Dequeue());
                    counter++;
                }
            }

            return returnValue;
        }

        internal int GetPlayerNumber()
        {
            return k_PlayerNumber;
        }

        internal void AddToAiMemory(string i_PositionOne, string i_PositionTwo)
        {
            addToAiMemory(i_PositionOne);
            addToAiMemory(i_PositionTwo);
        }

        private void addToAiMemory(string i_Position)
        {
            if (!r_AiMemory.Contains(i_Position))
            {
                if (r_AiMemory.Count >= k_AiLevel)
                {
                    r_GameBoard.AddToUnUsedCardLocationsList(r_AiMemory.Dequeue());
                }

                r_AiMemory.Enqueue(i_Position);
                r_GameBoard.RemoveFromUnUsedCardLocationsList(i_Position);
            }
        }

        internal void RemoveFromAiMemory(string i_FirstCard, string i_SecondCard)
        {
            int counter = 0;
            if (r_AiMemory.Contains(i_FirstCard) || r_AiMemory.Contains(i_SecondCard))
            {
                while (r_AiMemory.Count > counter)
                {
                    if (r_AiMemory.Peek().Equals(i_FirstCard) || r_AiMemory.Peek().Equals(i_SecondCard))
                    {
                        r_AiMemory.Dequeue();
                        if (r_AiMemory.Count <= 0)
                        {
                            break;
                        }

                        continue;
                    }

                    r_AiMemory.Enqueue(r_AiMemory.Dequeue());
                    counter++;
                }
            }
        }
    }
}
