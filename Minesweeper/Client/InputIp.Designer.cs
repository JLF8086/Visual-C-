﻿namespace Client
{
    partial class InputIp
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
            this.label1 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minesBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(79, 16);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 1;
            this.ipBox.Text = "localhost";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(218, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(79, 42);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(100, 20);
            this.heightBox.TabIndex = 4;
            this.heightBox.Text = "20";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(79, 68);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(100, 20);
            this.widthBox.TabIndex = 6;
            this.widthBox.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Width";
            // 
            // minesBox
            // 
            this.minesBox.Location = new System.Drawing.Point(79, 94);
            this.minesBox.Name = "minesBox";
            this.minesBox.Size = new System.Drawing.Size(100, 20);
            this.minesBox.TabIndex = 8;
            this.minesBox.Text = "40";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mines";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // InputIp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 128);
            this.Controls.Add(this.minesBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.widthBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label1);
            this.Name = "InputIp";
            this.Text = "InputIp";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox minesBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}