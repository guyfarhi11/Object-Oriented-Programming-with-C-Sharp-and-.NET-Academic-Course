namespace EX05.FormsUI
{
    internal partial class EndOfGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_ExitBtn = new System.Windows.Forms.Button();
            this.m_PlayAgainBtn = new System.Windows.Forms.Button();
            this.m_ResultSentenceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_ExitBtn
            // 
            this.m_ExitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ExitBtn.Location = new System.Drawing.Point(56, 124);
            this.m_ExitBtn.Name = "m_ExitBtn";
            this.m_ExitBtn.Size = new System.Drawing.Size(124, 51);
            this.m_ExitBtn.TabIndex = 0;
            this.m_ExitBtn.Text = "Exit";
            this.m_ExitBtn.UseVisualStyleBackColor = true;
            // 
            // m_PlayAgainBtn
            // 
            this.m_PlayAgainBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_PlayAgainBtn.Location = new System.Drawing.Point(246, 125);
            this.m_PlayAgainBtn.Name = "m_PlayAgainBtn";
            this.m_PlayAgainBtn.Size = new System.Drawing.Size(124, 49);
            this.m_PlayAgainBtn.TabIndex = 1;
            this.m_PlayAgainBtn.Text = "Play again";
            this.m_PlayAgainBtn.UseVisualStyleBackColor = true;
            // 
            // m_ResultSentenceLabel
            // 
            this.m_ResultSentenceLabel.AutoSize = true;
            this.m_ResultSentenceLabel.Location = new System.Drawing.Point(63, 23);
            this.m_ResultSentenceLabel.Name = "m_ResultSentenceLabel";
            this.m_ResultSentenceLabel.Size = new System.Drawing.Size(0, 20);
            this.m_ResultSentenceLabel.TabIndex = 2;
            // 
            // EndOfGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.m_ExitBtn;
            this.ClientSize = new System.Drawing.Size(406, 213);
            this.Controls.Add(this.m_ResultSentenceLabel);
            this.Controls.Add(this.m_PlayAgainBtn);
            this.Controls.Add(this.m_ExitBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndOfGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndOfGameForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_ExitBtn;
        private System.Windows.Forms.Button m_PlayAgainBtn;
        private System.Windows.Forms.Label m_ResultSentenceLabel;
    }
}