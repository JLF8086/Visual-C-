namespace Client
{
    partial class MinesweeperGUI
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
            this.button1 = new System.Windows.Forms.Button();
            this.mineField = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDismantles = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(153, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(269, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "R";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mineField
            // 
            this.mineField.Location = new System.Drawing.Point(12, 55);
            this.mineField.Name = "mineField";
            this.mineField.Size = new System.Drawing.Size(551, 518);
            this.mineField.TabIndex = 1;
            // 
            // labelTime
            // 
            this.labelTime.BackColor = System.Drawing.Color.Black;
            this.labelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Yellow;
            this.labelTime.Location = new System.Drawing.Point(12, 12);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(135, 23);
            this.labelTime.TabIndex = 4;
            this.labelTime.Text = "0";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDismantles
            // 
            this.labelDismantles.BackColor = System.Drawing.Color.Black;
            this.labelDismantles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDismantles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDismantles.ForeColor = System.Drawing.Color.Yellow;
            this.labelDismantles.Location = new System.Drawing.Point(428, 12);
            this.labelDismantles.Name = "labelDismantles";
            this.labelDismantles.Size = new System.Drawing.Size(135, 23);
            this.labelDismantles.TabIndex = 6;
            this.labelDismantles.Text = "0";
            this.labelDismantles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MinesweeperGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 585);
            this.Controls.Add(this.labelDismantles);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.mineField);
            this.Controls.Add(this.button1);
            this.Name = "MinesweeperGUI";
            this.Text = "MinesweeperGUI";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel mineField;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDismantles;
    }
}