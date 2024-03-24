namespace WitnessTest
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.xInputF1 = new System.Windows.Forms.Label();
            this.yInputF1 = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.updateScreen = new System.Windows.Forms.Timer(this.components);
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // xInputF1
            // 
            this.xInputF1.AutoSize = true;
            this.xInputF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInputF1.Location = new System.Drawing.Point(589, 667);
            this.xInputF1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.xInputF1.Name = "xInputF1";
            this.xInputF1.Size = new System.Drawing.Size(25, 26);
            this.xInputF1.TabIndex = 1;
            this.xInputF1.Text = "0";
            this.xInputF1.Visible = false;
            // 
            // yInputF1
            // 
            this.yInputF1.AutoSize = true;
            this.yInputF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yInputF1.Location = new System.Drawing.Point(660, 667);
            this.yInputF1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.yInputF1.Name = "yInputF1";
            this.yInputF1.Size = new System.Drawing.Size(25, 26);
            this.yInputF1.TabIndex = 2;
            this.yInputF1.Text = "0";
            this.yInputF1.Visible = false;
            // 
            // levelLabel
            // 
            this.levelLabel.BackColor = System.Drawing.Color.Transparent;
            this.levelLabel.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel.ForeColor = System.Drawing.Color.Black;
            this.levelLabel.Location = new System.Drawing.Point(280, 0);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(140, 23);
            this.levelLabel.TabIndex = 9;
            this.levelLabel.Text = "Level: 1";
            this.levelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateScreen
            // 
            this.updateScreen.Enabled = true;
            this.updateScreen.Interval = 16;
            this.updateScreen.Tick += new System.EventHandler(this.updateScreen_Tick);
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.BackColor = System.Drawing.Color.Transparent;
            this.copyrightLabel.Font = new System.Drawing.Font("Vladimir Script", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyrightLabel.ForeColor = System.Drawing.Color.Black;
            this.copyrightLabel.Location = new System.Drawing.Point(2, 683);
            this.copyrightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(150, 16);
            this.copyrightLabel.TabIndex = 13;
            this.copyrightLabel.Text = "LGMazeSoftware Ltd.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(700, 700);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.yInputF1);
            this.Controls.Add(this.xInputF1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label xInputF1;
        private System.Windows.Forms.Label yInputF1;
        public System.Windows.Forms.Label levelLabel;
        public System.Windows.Forms.Timer updateScreen;
        private System.Windows.Forms.Label copyrightLabel;
    }
}

