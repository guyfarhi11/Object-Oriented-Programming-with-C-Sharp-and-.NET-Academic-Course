using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EX05.GameLogic;

namespace EX05.FormsUI
{
    internal class GameForm : Form
    {
        private const string k_WindowName = "Memory Game";
        private const string k_Pairs = "Pair(s)";
        private const string k_CurrentPlayer = "Current Player";
        private const int k_ButtonSize = 90;
        private const int k_DistanceButton = 20;
        private const int k_FirstAsciChar = 65;
        private readonly string[] r_PlayersNames = new string[2];
        private readonly int r_Width;
        private readonly int r_Height;
        private readonly Dictionary<string, GameButton> r_ButtonMap;
        private readonly Label r_CurrentPlayerLabel;
        private readonly Label[] r_PlayerLabel = new Label[2];
        private readonly int r_TypeOfGame;
        private readonly Color[] r_PlayerColor = new Color[] { Color.FromArgb(192, 255, 192), Color.FromArgb(191, 191, 255) };
        private readonly EndOfGameForm r_EndOfGameForm;
        private readonly PictureMap r_PicturesMap;
        private readonly Timer r_ComputerTimer;
        private readonly Timer r_FlipTimer;
        private SingleGame m_CurrentGame;
        private string m_FirstChosen;
        private string m_SecondChosen;

        internal GameForm(int i_Width, int i_Height, int i_TypeOfGame, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            r_FlipTimer = new Timer() { Interval = 10 };
            r_FlipTimer.Tick += flipTimer_Tick;
            r_ComputerTimer = new Timer() { Interval = 1000 };
            r_ComputerTimer.Tick += computerTimer_Tick;
            m_FirstChosen = string.Empty;
            m_SecondChosen = string.Empty;
            Text = k_WindowName;
            r_PlayersNames[0] = i_FirstPlayerName;
            r_PlayersNames[1] = i_SecondPlayerName;
            m_CurrentGame = new SingleGame(i_Width, i_Height, i_TypeOfGame, i_FirstPlayerName, i_SecondPlayerName);
            r_TypeOfGame = i_TypeOfGame;
            AutoSize = true;
            r_Height = i_Height;
            r_Width = i_Width;
            r_ButtonMap = new Dictionary<string, GameButton>(i_Height * i_Width);
            r_PicturesMap = new PictureMap(i_Height * i_Width / 2);
            r_CurrentPlayerLabel = new Label();
            r_PlayerLabel[0] = new Label();
            r_PlayerLabel[1] = new Label();
            r_EndOfGameForm = new EndOfGameForm();
            m_CurrentGame.EndGame += endTheGame;
            m_CurrentGame.GoodMatch += updateLabel_GoodMatch;
            m_CurrentGame.WrongChoice += flip_WrongChoice;
            m_CurrentGame.AIChoice += buttonClick_AIChoice;
            initializeBoardComponent();
            this.Shown += gameForm_Shown;
        }

        private void initializeBoardComponent()
        {
            int posY = k_DistanceButton;
            for (int i = 0; i < r_Height; i++)
            {
                int posX = k_DistanceButton;
                for (int j = 0; j < r_Width; j++)
                {
                    string currentLocation = string.Format("{0}{1}", ((char)(j + k_FirstAsciChar)).ToString(), i + 1);
                    r_ButtonMap.Add(currentLocation, new GameButton(currentLocation));
                    GameButton currentButton = r_ButtonMap[currentLocation];
                    currentButton.Size = new Size(k_ButtonSize, k_ButtonSize);
                    currentButton.Left = posX;
                    currentButton.Top = posY;
                    posX += k_ButtonSize + k_DistanceButton;
                    currentButton.Click += currentButton_Click;
                    this.Controls.Add(currentButton);
                }

                posY += k_ButtonSize + k_DistanceButton;
            }

            r_CurrentPlayerLabel.Left = k_DistanceButton;
            r_CurrentPlayerLabel.Top = posY;
            r_CurrentPlayerLabel.AutoSize = true;
            setCurrentPlayerLabel(m_CurrentGame.CurrentPlayer);
            r_CurrentPlayerLabel.BackColor = r_PlayerColor[m_CurrentGame.CurrentPlayer];
            Controls.Add(r_CurrentPlayerLabel);
            posY += r_CurrentPlayerLabel.PreferredHeight + k_DistanceButton;
            r_PlayerLabel[0].Left = k_DistanceButton;
            r_PlayerLabel[0].Top = posY;
            setFormattedPlayerLabel(0);
            r_PlayerLabel[0].BackColor = r_PlayerColor[0];
            r_PlayerLabel[0].AutoSize = true;
            Controls.Add(r_PlayerLabel[0]);
            posY += r_PlayerLabel[0].PreferredHeight + k_DistanceButton;
            r_PlayerLabel[1].Left = k_DistanceButton;
            r_PlayerLabel[1].Top = posY;
            setFormattedPlayerLabel(1);
            r_PlayerLabel[1].BackColor = r_PlayerColor[1];
            r_PlayerLabel[1].AutoSize = true;
            Controls.Add(r_PlayerLabel[1]);
            MaximizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Padding = new Padding(0, 0, k_DistanceButton, k_DistanceButton);
        }

        internal void ResetForm()
        {
            m_CurrentGame = new SingleGame(r_Width, r_Height, r_TypeOfGame, r_PlayersNames);
            foreach (string position in r_ButtonMap.Keys)
            {
                resetButton(position);
            }

            setFormattedPlayerLabel(0);
            setFormattedPlayerLabel(1);
            setCurrentPlayerLabel(m_CurrentGame.CurrentPlayer);
            m_CurrentGame.GoodMatch += updateLabel_GoodMatch;
            m_CurrentGame.WrongChoice += flip_WrongChoice;
            m_CurrentGame.EndGame += endTheGame;
            m_CurrentGame.AIChoice += buttonClick_AIChoice;
            startComputerTurn();
        }

        private void flip_WrongChoice()
        {
            r_FlipTimer.Start();
        }

        private void updateLabel_GoodMatch()
        {
            setFormattedPlayerLabel(1 - m_CurrentGame.CurrentPlayer);
            this.Enabled = true;
            startComputerTurn();
            m_FirstChosen = string.Empty;
            m_SecondChosen = string.Empty;
        }

        private void flipTimer_Tick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            resetButton(m_SecondChosen);
            resetButton(m_FirstChosen);
            m_FirstChosen = string.Empty;
            m_SecondChosen = string.Empty;
            r_FlipTimer.Stop();
            this.Enabled = true;
            startComputerTurn();
        }

        private void gameForm_Shown(object sender, EventArgs e)
        {
            startComputerTurn();
        }

        private void startComputerTurn()
        {
            if (m_CurrentGame.CurrentPlayer == 1 && r_TypeOfGame == 0)
            {
                r_ComputerTimer.Start();
                this.Enabled = false;
            }
        }

        private void computerTimer_Tick(object sender, EventArgs e)
        {
            m_CurrentGame.RunAI();
        }

        internal EndOfGameForm EndOfGameForm
        {
            get
            {
                return r_EndOfGameForm;
            }
        }

        private void resetButton(string i_Position)
        {
            r_ButtonMap[i_Position].BackgroundImage = null;
            r_ButtonMap[i_Position].BackColor = default(Color);
            r_ButtonMap[i_Position].Enabled = true;
        }

        private void setFormattedPlayerLabel(int i_PlayerIndex)
        {
            r_PlayerLabel[i_PlayerIndex].Text = string.Format(
                "{0}: {1} {2}",
                r_PlayersNames[i_PlayerIndex],
                m_CurrentGame.GetPlayerPointsByIndex(i_PlayerIndex),
                k_Pairs);
        }

        private void setCurrentPlayerLabel(int i_PlayerIndex)
        {
            r_CurrentPlayerLabel.BackColor = r_PlayerColor[m_CurrentGame.CurrentPlayer];
            r_CurrentPlayerLabel.Text = string.Format("{0}: {1}", k_CurrentPlayer, r_PlayersNames[i_PlayerIndex]);
        }
        
        private void buttonClick_AIChoice(string i_ComputerChoice)
        {
            if (m_FirstChosen != string.Empty)
            {
                r_ComputerTimer.Stop();
                System.Threading.Thread.Sleep(1000);
            }

            currentButton_Click((object)r_ButtonMap[i_ComputerChoice], null);
        }

        private void currentButton_Click(object sender, EventArgs e)
        {
            GameButton button = sender as GameButton;
            button.BackColor = r_PlayerColor[m_CurrentGame.CurrentPlayer];
            button.BackgroundImage = r_PicturesMap.GetPathByKey(m_CurrentGame.GetKey(button.Location));
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Enabled = false;
            if (!m_FirstChosen.Equals(string.Empty))
            {
                if(m_CurrentGame.RunTurnPlayer(m_FirstChosen, button.Location))
                {
                    return;
                }

                m_SecondChosen = button.Location;
                setCurrentPlayerLabel(m_CurrentGame.CurrentPlayer);
            }
            else
            {
                m_FirstChosen = button.Location;
            }
        }

        private void endTheGame(string i_Winner, string i_Looser, int i_WinnerPoints, int i_LooserPoints, bool i_Tied)
        {
            this.Hide();
            r_ComputerTimer.Stop();
            this.Enabled = true;
            r_EndOfGameForm.EndTheGame(i_Winner, i_Looser, i_WinnerPoints, i_LooserPoints, i_Tied);
        }
    }
}
