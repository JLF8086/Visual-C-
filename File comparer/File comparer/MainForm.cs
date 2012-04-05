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
        public MainForm()
        {
            InitializeComponent();
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
            fileInfoGridView.Enabled = true;
            controlsGroupBox.Enabled = true;
            markDupesButton.Enabled = false;
            duplicateHashes = null;
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

        private string folderName;
        private List<string> files;
        private List<string> duplicateHashes;
        private Dictionary<string, string> filesWithHashes;
        private Random rand = new Random();

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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            PrepareForWork();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
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
        }
    }
}
