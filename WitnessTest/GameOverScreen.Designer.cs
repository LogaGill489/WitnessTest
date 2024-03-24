namespace WitnessTest
{
    partial class GameOverScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.restartButton = new System.Windows.Forms.Button();
            this.winLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // restartButton
            // 
            this.restartButton.BackColor = System.Drawing.Color.Wheat;
            this.restartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartButton.ForeColor = System.Drawing.Color.Black;
            this.restartButton.Location = new System.Drawing.Point(174, 316);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(300, 67);
            this.restartButton.TabIndex = 0;
            this.restartButton.Text = "Return To Start";
            this.restartButton.UseVisualStyleBackColor = false;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // winLabel
            // 
            this.winLabel.BackColor = System.Drawing.Color.Transparent;
            this.winLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.winLabel.Font = new System.Drawing.Font("Palatino Linotype", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winLabel.ForeColor = System.Drawing.Color.White;
            this.winLabel.Location = new System.Drawing.Point(124, 42);
            this.winLabel.Name = "winLabel";
            this.winLabel.Size = new System.Drawing.Size(400, 63);
            this.winLabel.TabIndex = 1;
            this.winLabel.Text = "Congrats!";
            this.winLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreLabel
            // 
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scoreLabel.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.White;
            this.scoreLabel.Location = new System.Drawing.Point(149, 130);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(350, 41);
            this.scoreLabel.TabIndex = 2;
            this.scoreLabel.Text = "You Beat The Game In 000 Seconds!";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameOverScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.winLabel);
            this.Controls.Add(this.restartButton);
            this.DoubleBuffered = true;
            this.Name = "GameOverScreen";
            this.Size = new System.Drawing.Size(648, 648);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Label winLabel;
        private System.Windows.Forms.Label scoreLabel;
    }
}
