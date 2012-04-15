using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace File_comparer
{
    public partial class MainForm : Form
    {

        private string folderName;
        private List<string> files;
        private List<string> duplicateHashes;
        private Dictionary<string, string> filesWithHashes;
        private Random rand = new Random();
        private Queue<string> hashesToPrint;
        private int conflictNumber;
        private bool firstPage;

        public MainForm()
        {
            InitializeComponent();
            this.MaximumSize = this.MinimumSize = this.Size;
        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new MainForm());
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.folderName = folder.SelectedPath;
                this.currentFolderLabel.Text = folder.SelectedPath;
                PrepareForWork();
            }
        }

        private void PrepareForWork()
        {
            
            fileInfoGridView.Rows.Clear();
            if (radioButton1.Checked)
                files = new List<string>(Directory.GetFiles(folderName));
            else if (radioButton2.Checked)
                files = new List<string>(Directory.GetFiles(folderName, "*", SearchOption.AllDirectories));
            filesWithHashes = new Dictionary<string, string>();
            foreach (string file in files)
            {
                filesWithHashes.Add(file, "not determined");
            }
            foreach (string key in filesWithHashes.Keys)
                fileInfoGridView.Rows.Add(new Object[] { key.Replace(folderName + "\\", String.Empty), filesWithHashes[key] });
            for (int i = 0; i < fileInfoGridView.Rows.Count; i++)
                fileInfoGridView.Rows[i].HeaderCell.Value = (i+1).ToString();
            fileInfoGridView.Enabled = true;
            controlsGroupBox.Enabled = true;
            markDupesButton.Enabled = false;
            duplicateHashes = null;
            printToolStripMenuItem.Enabled = printPreviewToolStripMenuItem.Enabled = false;
        }

        private void Run()
        {
            int sleepPeriod = Int32.Parse(delayTextBox.Text);
            while (files.Count != 0)
            {
                string selectedFile;
                lock (this.files)
                {
                    int randomVal = rand.Next(files.Count);
                    selectedFile = files[randomVal];
                    files.RemoveAt(randomVal);
                }
                string hash = GetMD5HashFromFile(selectedFile);
                filesWithHashes[selectedFile] = hash;
                foreach (DataGridViewRow row in this.fileInfoGridView.Rows)
                    if (row.Cells[0].Value.Equals(selectedFile.Replace(folderName + "\\", String.Empty)))
                    {
                        row.Cells[1].Value = hash;
                        break;
                    }
                Thread.Sleep(sleepPeriod);
            }
            if (!markDupesButton.Enabled && !controlsGroupBox.Enabled)
            {
                controlsGroupBox.Enabled = true;
                markDupesButton.Enabled = true;
            }

            if (duplicateHashes == null)
            {
                duplicateHashes = new List<string>();
                Dictionary<string, int> occurenceDict = new Dictionary<string, int>();
                foreach (string hash in filesWithHashes.Values.Distinct())
                    occurenceDict.Add(hash, 0);
                foreach (string hash in filesWithHashes.Values)
                    occurenceDict[hash]++;
                foreach (string key in occurenceDict.Keys)
                    if (occurenceDict[key] > 1)
                        duplicateHashes.Add(key);
            }
        }

        private string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x"));
            }
            return sb.ToString();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidatedInput())
                return;
            controlsGroupBox.Enabled = false;
            foreach (Control cont in controlsGroupBox.Controls)
                cont.Enabled = false;
            for (int i = 0; i < Int32.Parse(threadNumberTextBox.Text); i++)
                new Thread(new ThreadStart(Run)).Start();
            foreach (Control cont in controlsGroupBox.Controls)
               cont.Enabled = true;
        }

        private bool ValidatedInput()
        {
            textBoxErrorProvider.Clear();
            bool retval = true;
            int a, b;
            try
            {
                a = Int32.Parse(threadNumberTextBox.Text);
                if (a <= 0)
                {
                    textBoxErrorProvider.SetError(threadNumberTextBox, "Must be above 0");
                    retval = false;
                }
            }
            catch
            {
                textBoxErrorProvider.SetError(threadNumberTextBox, "Bad number format");
                retval = false;
            }
            try
            {
                b = Int32.Parse(delayTextBox.Text);
                if (b < 0)
                {
                    textBoxErrorProvider.SetError(delayTextBox, "Must be above or equal to 0");
                    retval = false;
                }
            }
            catch
            {
                textBoxErrorProvider.SetError(delayTextBox, "Bad number format");
                retval = false;
            }
            
            return retval;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PrepareForWork();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            PrepareForWork();
        }

        private void markDupesButton_Click(object sender, EventArgs e)
        {
            foreach (string hash in duplicateHashes)
                foreach (DataGridViewRow row in fileInfoGridView.Rows)
                    if (row.Cells[1].Value.Equals(hash))
                        row.DefaultCellStyle.BackColor = Color.Linen;
            printPreviewToolStripMenuItem.Enabled = printToolStripMenuItem.Enabled = true;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                hashesToPrint = new Queue<string>(duplicateHashes);
                conflictNumber = 0;
                this.firstPage = true;
                printDocument1.Print();
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hashesToPrint = new Queue<string>(duplicateHashes);
            conflictNumber = 0;
            this.firstPage = true;
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument1;
            printPreviewDialog.Show();
        }

        private void printPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font titleFont = new Font("Arial", 18);
            Font headerFont = new Font("Arial", 12);
            Font textFont = new Font("Arial", 10);
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            yPos += topMargin;
            if (this.firstPage)
            {
                string str = "Duplicate File Report";
                e.Graphics.DrawString(str, titleFont, Brushes.Black, leftMargin + 
                    ((e.MarginBounds.Width - e.Graphics.MeasureString(str, titleFont).Width)/2), yPos);
                yPos += titleFont.GetHeight(e.Graphics);
                e.Graphics.DrawString("Inspected folder (" + (radioButton1.Checked ? "Immediate" : "Full") + "):"
                    + "\n" +  folderName
                    , textFont, Brushes.Black, leftMargin, yPos);
                yPos += 2*headerFont.GetHeight(e.Graphics);
                if (hashesToPrint.Count == 0)
                {
                    e.Graphics.DrawString("No conflicts found", titleFont, Brushes.Green, leftMargin, yPos);
                    yPos += titleFont.GetHeight(e.Graphics);
                }
                this.firstPage = false;
            }

            while (hashesToPrint.Count > 0)
            {
                float requiredHeight = headerFont.GetHeight(e.Graphics) + textFont.GetHeight(e.Graphics)*2;
                string hash = hashesToPrint.Peek();
                var files = from f in filesWithHashes
                            where f.Value == hash
                            select f.Key;
                int duplicateCount = filesWithHashes.Count(a => a.Key == hash);
                requiredHeight += duplicateCount * textFont.GetHeight(e.Graphics);
                if (requiredHeight - textFont.GetHeight(e.Graphics) > e.MarginBounds.Height - yPos)
                    break;
                else
                {
                    ++conflictNumber;
                    e.Graphics.DrawString(String.Format("Conflict {0}:", conflictNumber), headerFont, Brushes.Red, leftMargin, yPos);
                    yPos += headerFont.GetHeight(e.Graphics);
                    e.Graphics.DrawString(String.Format("Hash : {0}", hash), textFont, Brushes.Green, leftMargin, yPos);
                    yPos += headerFont.GetHeight(e.Graphics);
                    
                    foreach (string file in files)
                    {
                        e.Graphics.DrawString(file.Replace(folderName, String.Empty), textFont, Brushes.Olive, leftMargin, yPos);
                        yPos += textFont.GetHeight(e.Graphics);
                    }
                    hashesToPrint.Dequeue();
                }
            }
            if (hashesToPrint.Count > 0)
                e.HasMorePages = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
