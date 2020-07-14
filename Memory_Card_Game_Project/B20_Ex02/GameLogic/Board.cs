using System;
using System.Collections.Generic;

namespace B20_Ex02.GameLogic
{
    internal class Board
    {
        private const int k_FirstAsciChar = 65;
        private const int k_OneAsciChar = 48;
        private readonly int r_BoardWidth;
        private readonly int r_BoardHeight;
        private readonly Dictionary<string, Card> r_DictionaryBoard;
        private readonly List<string> r_UnUsedCardLocations;
        private int m_AmountOfUnOpenCard;

        internal Board(int i_Width, int i_Height)
        {
            r_DictionaryBoard = createCardValue(i_Width, i_Height);
            r_UnUsedCardLocations = new List<string>(this.r_DictionaryBoard.Keys);
            r_BoardWidth = i_Width;
            r_BoardHeight = i_Height;
            this.m_AmountOfUnOpenCard = i_Height * i_Width;
        }

        internal int AmountOfUnOpenCard
        {
            get { return m_AmountOfUnOpenCard; }
            set { m_AmountOfUnOpenCard = value; }
        }

        private static Dictionary<string, Card> createCardValue(int i_Width, int i_Height)
        {
            Dictionary<string, Card> dictionaryToReaturn = new Dictionary<string, Card>();
            int amoutOfUniqueCards = i_Width * i_Height / 2;
            List<char> arrayCardValue = new List<char>(amoutOfUniqueCards);
            for (int i = 0; i < amoutOfUniqueCards; i++)
            {
                int asciChar = i + k_FirstAsciChar;
                char toAdd = (char)asciChar;
                arrayCardValue.Add(toAdd);
                arrayCardValue.Add(toAdd);
            }

            Random rnd = new Random();
            for (int i = k_FirstAsciChar; i < (i_Width + k_FirstAsciChar); i++)
            {
                for (int j = 1; j <= i_Height; j++)
                {
                    int index = rnd.Next(arrayCardValue.Count);
                    char charAddToDictionary = arrayCardValue[index];
                    Card cardAddToDictionary = new Card(charAddToDictionary);
                    string stringAddToDictionary = string.Format("{0}{1}", (char)i, (char)(j + k_OneAsciChar));
                    dictionaryToReaturn.Add(stringAddToDictionary, cardAddToDictionary);
                    arrayCardValue.Remove(charAddToDictionary);
                }
            }

            return dictionaryToReaturn;
        }

        internal string ChooseCardRandomly()
        {
            string returnCard = string.Empty;
            if (this.r_UnUsedCardLocations.Count != 0)
            {
                Random random = new Random();
                int index = random.Next(0, this.r_UnUsedCardLocations.Count);
                string location = this.r_UnUsedCardLocations[index];
                this.RemoveFromUnUsedCardLocationsList(location);
                returnCard = location;
            }

            return returnCard;
        }

        internal void AddToUnUsedCardLocationsList(string i_Location)
        {
            if (!r_UnUsedCardLocations.Contains(i_Location))
            {
                r_UnUsedCardLocations.Add(i_Location);
            }
        }

        internal void RemoveFromUnUsedCardLocationsList(string i_Location)
        {
            if (this.r_UnUsedCardLocations.Contains(i_Location))
            {
                this.r_UnUsedCardLocations.Remove(i_Location);
            }
        }

        internal bool CheckTheCards(string i_FirstCardPosition, string i_SecondCardPosition, int i_PlayerNumber)
        {
            bool result = r_DictionaryBoard[i_FirstCardPosition].CardValue == r_DictionaryBoard[i_SecondCardPosition].CardValue;
            if (result)
            {
                this.RemoveFromUnUsedCardLocationsList(i_FirstCardPosition);
                this.RemoveFromUnUsedCardLocationsList(i_SecondCardPosition);
            }
            else
            {
                r_DictionaryBoard[i_FirstCardPosition].FlipCard();
                r_DictionaryBoard[i_SecondCardPosition].FlipCard();
            }

            return result;
        }

        internal void FlipTemporaryCard(string i_Card)
        {
            r_DictionaryBoard[i_Card].FlipCard();
        }

        internal Card GetCardByLocation(string i_CardPosition)
        {
            return r_DictionaryBoard[i_CardPosition];
        }

        internal char GetCardValueByLocation(string i_CardPosition)
        {
            return r_DictionaryBoard[i_CardPosition].CardValue;
        }

        internal bool ValidateLocation(string i_CardPosition)
        {
            return r_DictionaryBoard.ContainsKey(i_CardPosition) && !r_DictionaryBoard[i_CardPosition].IsShow;
        }

        internal bool CheckIfBoardIsClear()
        {
            return m_AmountOfUnOpenCard == 0;
        }

        internal int BoardWidth
        {
            get { return r_BoardWidth; }
        }

        internal int BoardHeight
        {
            get { return r_BoardHeight; }
        }
    }
}
