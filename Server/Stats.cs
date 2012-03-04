using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Server
{
    class Stats
    {
        public static void AddVictory()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load("Stats.xml");

                XmlNode node = xmlDoc.SelectSingleNode("minesweeper_stats/victories");
                node.FirstChild.Value = (1 + Convert.ToInt32(node.FirstChild.Value)).ToString();
                xmlDoc.Save("Stats.xml");
            }
            catch (Exception e)
            {
                Console.Write(e);
                CreateFile();
                AddVictory();
            }
        }

        public static int Victories
        {
            get
            {
                try
                {
                    XmlTextReader reader = new XmlTextReader("Stats.xml");
                    while (reader.Read())
                    {
                        if (reader.Name == "victories")
                        {
                            int result = reader.ReadElementContentAsInt();
                            reader.Close();
                            return result;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    CreateFile();
                    return 0;
                }
                return 0;
            }
        }

        public static void CreateFile()
        {
            XmlDocument statsDoc = new XmlDocument();
            statsDoc.AppendChild(statsDoc.CreateXmlDeclaration("1.0", null, null));
            XmlElement el = (XmlElement)statsDoc.AppendChild(statsDoc.CreateElement("minesweeper_stats"));
            el.AppendChild(statsDoc.CreateElement("victories")).InnerText = "0";
            statsDoc.Save("Stats.xml");
        }
    }
}
