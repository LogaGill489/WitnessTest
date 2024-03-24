namespace WitnessTest
{
    partial class GameScreen
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
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.xInput = new System.Windows.Forms.Label();
            this.yInput = new System.Windows.Forms.Label();
            this.rectXPos = new System.Windows.Forms.Label();
            this.rectYPos = new System.Windows.Forms.Label();
            this.listLabel = new System.Windows.Forms.Label();
            this.listLabel2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 2;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // xInput
            // 
            this.xInput.AutoSize = true;
            this.xInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput.Location = new System.Drawing.Point(16, 18);
            this.xInput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.xInput.Name = "xInput";
            this.xInput.Size = new System.Drawing.Size(25, 26);
            this.xInput.TabIndex = 0;
            this.xInput.Text = "0";
            this.xInput.Visible = false;
            // 
            // yInput
            // 
            this.yInput.AutoSize = true;
            this.yInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yInput.Location = new System.Drawing.Point(16, 59);
            this.yInput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.yInput.Name = "yInput";
            this.yInput.Size = new System.Drawing.Size(25, 26);
            this.yInput.TabIndex = 1;
            this.yInput.Text = "0";
            this.yInput.Visible = false;
            // 
            // rectXPos
            // 
            this.rectXPos.AutoSize = true;
            this.rectXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectXPos.Location = new System.Drawing.Point(105, 18);
            this.rectXPos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rectXPos.Name = "rectXPos";
            this.rectXPos.Size = new System.Drawing.Size(25, 26);
            this.rectXPos.TabIndex = 2;
            this.rectXPos.Text = "0";
            this.rectXPos.Visible = false;
            // 
            // rectYPos
            // 
            this.rectYPos.AutoSize = true;
            this.rectYPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectYPos.Location = new System.Drawing.Point(105, 59);
            this.rectYPos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rectYPos.Name = "rectYPos";
            this.rectYPos.Size = new System.Drawing.Size(25, 26);
            this.rectYPos.TabIndex = 3;
            this.rectYPos.Text = "0";
            this.rectYPos.Visible = false;
            // 
            // listLabel
            // 
            this.listLabel.AutoSize = true;
            this.listLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listLabel.Location = new System.Drawing.Point(192, 18);
            this.listLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.listLabel.Name = "listLabel";
            this.listLabel.Size = new System.Drawing.Size(25, 26);
            this.listLabel.TabIndex = 4;
            this.listLabel.Text = "0";
            this.listLabel.Visible = false;
            // 
            // listLabel2
            // 
            this.listLabel2.AutoSize = true;
            this.listLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listLabel2.Location = new System.Drawing.Point(192, 59);
            this.listLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.listLabel2.Name = "listLabel2";
            this.listLabel2.Size = new System.Drawing.Size(25, 26);
            this.listLabel2.TabIndex = 5;
            this.listLabel2.Text = "0";
            this.listLabel2.Visible = false;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.Controls.Add(this.listLabel2);
            this.Controls.Add(this.listLabel);
            this.Controls.Add(this.rectYPos);
            this.Controls.Add(this.rectXPos);
            this.Controls.Add(this.yInput);
            this.Controls.Add(this.xInput);
            this.DoubleBuffered = true;
            this.Name = "GameScreen";
            this.Size = new System.Drawing.Size(648, 648);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuScreen_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuScreen_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuScreen_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MenuScreen_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label xInput;
        private System.Windows.Forms.Label yInput;
        private System.Windows.Forms.Label rectXPos;
        private System.Windows.Forms.Label rectYPos;
        private System.Windows.Forms.Label listLabel;
        private System.Windows.Forms.Label listLabel2;
    }
}
