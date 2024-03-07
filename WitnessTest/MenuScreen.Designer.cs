namespace WitnessTest
{
    partial class MenuScreen
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
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 16;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // xInput
            // 
            this.xInput.AutoSize = true;
            this.xInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput.Location = new System.Drawing.Point(22, 22);
            this.xInput.Name = "xInput";
            this.xInput.Size = new System.Drawing.Size(31, 32);
            this.xInput.TabIndex = 0;
            this.xInput.Text = "0";
            // 
            // yInput
            // 
            this.yInput.AutoSize = true;
            this.yInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yInput.Location = new System.Drawing.Point(22, 73);
            this.yInput.Name = "yInput";
            this.yInput.Size = new System.Drawing.Size(31, 32);
            this.yInput.TabIndex = 1;
            this.yInput.Text = "0";
            // 
            // rectXPos
            // 
            this.rectXPos.AutoSize = true;
            this.rectXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectXPos.Location = new System.Drawing.Point(140, 22);
            this.rectXPos.Name = "rectXPos";
            this.rectXPos.Size = new System.Drawing.Size(31, 32);
            this.rectXPos.TabIndex = 2;
            this.rectXPos.Text = "0";
            // 
            // rectYPos
            // 
            this.rectYPos.AutoSize = true;
            this.rectYPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectYPos.Location = new System.Drawing.Point(140, 73);
            this.rectYPos.Name = "rectYPos";
            this.rectYPos.Size = new System.Drawing.Size(31, 32);
            this.rectYPos.TabIndex = 3;
            this.rectYPos.Text = "0";
            // 
            // MenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.Controls.Add(this.rectYPos);
            this.Controls.Add(this.rectXPos);
            this.Controls.Add(this.yInput);
            this.Controls.Add(this.xInput);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MenuScreen";
            this.Size = new System.Drawing.Size(860, 798);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuScreen_Paint);
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
    }
}
