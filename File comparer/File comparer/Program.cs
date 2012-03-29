using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace File_comparer
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Stopwatch sw = new Stopwatch();
            sw.Start();
            string s = GetMD5HashFromFile(@"D:\Downloads\mc7647500k.wmv");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine(s);*/
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            var files = Directory.GetFiles(@"D:\Downloads", "*", SearchOption.AllDirectories);
            foreach (string str in files)
                Console.WriteLine(str);
        }

        private static string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
