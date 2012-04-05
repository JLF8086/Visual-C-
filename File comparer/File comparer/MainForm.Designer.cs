namespace File_comparer
{
    partial class MainForm
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileInfoGridView = new System.Windows.Forms.DataGridView();
            this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HashColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlsGroupBox = new System.Windows.Forms.GroupBox();
            this.threadNumberLabel = new System.Windows.Forms.Label();
            this.delayLabel = new System.Windows.Forms.Label();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.threadNumberTextBox = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.startButton = new System.Windows.Forms.Button();
            this.currentFolderLabel = new System.Windows.Forms.Label();
            this.textBoxErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.resetButton = new System.Windows.Forms.Button();
            this.markDupesButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileInfoGridView)).BeginInit();
            this.controlsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(737, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder..";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.printToolStripMenuItem.Text = "Print..";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // fileInfoGridView
            // 
            this.fileInfoGridView.AllowUserToAddRows = false;
            this.fileInfoGridView.AllowUserToDeleteRows = false;
            this.fileInfoGridView.AllowUserToOrderColumns = true;
            this.fileInfoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fileInfoGridView.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.fileInfoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileInfoGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileColumn,
            this.HashColumn});
            this.fileInfoGridView.Enabled = false;
            this.fileInfoGridView.Location = new System.Drawing.Point(12, 72);
            this.fileInfoGridView.Name = "fileInfoGridView";
            this.fileInfoGridView.ReadOnly = true;
            this.fileInfoGridView.Size = new System.Drawing.Size(526, 442);
            this.fileInfoGridView.TabIndex = 0;
            // 
            // FileColumn
            // 
            this.FileColumn.HeaderText = "File";
            this.FileColumn.Name = "FileColumn";
            this.FileColumn.ReadOnly = true;
            this.FileColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // HashColumn
            // 
            this.HashColumn.HeaderText = "Hash";
            this.HashColumn.Name = "HashColumn";
            this.HashColumn.ReadOnly = true;
            // 
            // controlsGroupBox
            // 
            this.controlsGroupBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.controlsGroupBox.Controls.Add(this.markDupesButton);
            this.controlsGroupBox.Controls.Add(this.resetButton);
            this.controlsGroupBox.Controls.Add(this.threadNumberLabel);
            this.controlsGroupBox.Controls.Add(this.delayLabel);
            this.controlsGroupBox.Controls.Add(this.delayTextBox);
            this.controlsGroupBox.Controls.Add(this.threadNumberTextBox);
            this.controlsGroupBox.Controls.Add(this.radioButton2);
            this.controlsGroupBox.Controls.Add(this.radioButton1);
            this.controlsGroupBox.Controls.Add(this.startButton);
            this.controlsGroupBox.Enabled = false;
            this.controlsGroupBox.Location = new System.Drawing.Point(549, 72);
            this.controlsGroupBox.Name = "controlsGroupBox";
            this.controlsGroupBox.Size = new System.Drawing.Size(176, 442);
            this.controlsGroupBox.TabIndex = 1;
            this.controlsGroupBox.TabStop = false;
            this.controlsGroupBox.Text = "Controls";
            // 
            // threadNumberLabel
            // 
            this.threadNumberLabel.AutoSize = true;
            this.threadNumberLabel.Location = new System.Drawing.Point(7, 98);
            this.threadNumberLabel.Name = "threadNumberLabel";
            this.threadNumberLabel.Size = new System.Drawing.Size(74, 13);
            this.threadNumberLabel.TabIndex = 6;
            this.threadNumberLabel.Text = "No. of threads";
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.Location = new System.Drawing.Point(25, 124);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(56, 13);
            this.delayLabel.TabIndex = 5;
            this.delayLabel.Text = "Delay (ms)";
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(85, 121);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(58, 20);
            this.delayTextBox.TabIndex = 4;
            this.delayTextBox.Text = "0";
            // 
            // threadNumberTextBox
            // 
            this.threadNumberTextBox.Location = new System.Drawing.Point(85, 95);
            this.threadNumberTextBox.Name = "threadNumberTextBox";
            this.threadNumberTextBox.Size = new System.Drawing.Size(58, 20);
            this.threadNumberTextBox.TabIndex = 3;
            this.threadNumberTextBox.Text = "1";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(116, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "All Inner Directories";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(118, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Immediate Directory";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(6, 152);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(83, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Find Hashes";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // currentFolderLabel
            // 
            this.currentFolderLabel.AutoSize = true;
            this.currentFolderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.currentFolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentFolderLabel.Location = new System.Drawing.Point(12, 36);
            this.currentFolderLabel.Name = "currentFolderLabel";
            this.currentFolderLabel.Size = new System.Drawing.Size(0, 24);
            this.currentFolderLabel.TabIndex = 2;
            // 
            // textBoxErrorProvider
            // 
            this.textBoxErrorProvider.ContainerControl = this;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(6, 210);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(83, 23);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // markDupesButton
            // 
            this.markDupesButton.Enabled = false;
            this.markDupesButton.Location = new System.Drawing.Point(6, 181);
            this.markDupesButton.Name = "markDupesButton";
            this.markDupesButton.Size = new System.Drawing.Size(83, 23);
            this.markDupesButton.TabIndex = 8;
            this.markDupesButton.Text = "Mark Dupes";
            this.markDupesButton.UseVisualStyleBackColor = true;
            this.markDupesButton.Click += new System.EventHandler(this.markDupesButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(737, 536);
            this.Controls.Add(this.currentFolderLabel);
            this.Controls.Add(this.controlsGroupBox);
            this.Controls.Add(this.fileInfoGridView);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileInfoGridView)).EndInit();
            this.controlsGroupBox.ResumeLayout(false);
            this.controlsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.DataGridView fileInfoGridView;
        private System.Windows.Forms.GroupBox controlsGroupBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label currentFolderLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HashColumn;
        private System.Windows.Forms.Label threadNumberLabel;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.TextBox threadNumberTextBox;
        private System.Windows.Forms.ErrorProvider textBoxErrorProvider;
        private System.Windows.Forms.Button markDupesButton;
        private System.Windows.Forms.Button resetButton;
    }
}

