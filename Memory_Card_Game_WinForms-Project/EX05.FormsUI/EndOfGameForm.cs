using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EX05.GameLogic;

namespace EX05.FormsUI
{
    internal partial class EndOfGameForm : Form
    {
        private const string k_TiedGame = "The game ended with a tie! Each player ended with {0} pairs";
        private const string k_resultSentence = "Winner was {0} with {1} pairs and looser was {2} with {3} pair(s)";
        private const string k_LooserSentence = "{0}, you lost. better luck next time! You lost with a score of {1} points.";

        internal EndOfGameForm()
        { 
            InitializeComponent();
            this.Hide();
        }

        internal void EndTheGame(string i_Winner, string i_Looser, int i_WinnerPoints, int i_LooserPoints, bool i_Tied)
        {
            if (i_Tied)
            {
                m_ResultSentenceLabel.Text = string.Format(k_TiedGame, i_WinnerPoints);
            }
            else
            {
                m_ResultSentenceLabel.Text = string.Format(k_resultSentence, i_Winner, i_WinnerPoints, i_Looser, i_LooserPoints);
            }

            ShowDialog();
        }

        internal Button ExitButton
        {
            get
            {
                return m_ExitBtn;
            }
        }

        internal Button PlayAgainButtonn
        {
            get
            {
                return m_PlayAgainBtn;
            }
        }
    }
}
