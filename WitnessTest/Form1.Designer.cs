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
            this.xInputF1 = new System.Windows.Forms.Label();
            this.yInputF1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // xInputF1
            // 
            this.xInputF1.AutoSize = true;
            this.xInputF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInputF1.Location = new System.Drawing.Point(785, 821);
            this.xInputF1.Name = "xInputF1";
            this.xInputF1.Size = new System.Drawing.Size(31, 32);
            this.xInputF1.TabIndex = 1;
            this.xInputF1.Text = "0";
            // 
            // yInputF1
            // 
            this.yInputF1.AutoSize = true;
            this.yInputF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yInputF1.Location = new System.Drawing.Point(880, 821);
            this.yInputF1.Name = "yInputF1";
            this.yInputF1.Size = new System.Drawing.Size(31, 32);
            this.yInputF1.TabIndex = 2;
            this.yInputF1.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(933, 862);
            this.Controls.Add(this.yInputF1);
            this.Controls.Add(this.xInputF1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label xInputF1;
        private System.Windows.Forms.Label yInputF1;
    }
}

