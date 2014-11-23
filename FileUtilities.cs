using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace SolarFileReader
{
    public static class FileUtilities
    {
        private static string _destinationDirectory = "C:\\test";
        static Dictionary<DateTime, float> _parsedData = new Dictionary<DateTime, float>();
        public static void ConvertFile(string directoryName)
        {
            string fileName  = String.Empty;
            string[] fileList = Directory.GetFiles(directoryName);
            foreach (string fulllyQualifiedfileName in fileList)
            {
                string[] lines = File.ReadAllLines(fulllyQualifiedfileName);
                List<string> noBlanks = new List<string>();
                int counter = 0;
                foreach (string line in lines)
                {
                     fileName = Path.GetFileName(fulllyQualifiedfileName);
                    if (lines.Length == 1)
                    {
                        if (!Directory.Exists(_destinationDirectory + "\\junk"))
                         {
                             Directory.CreateDirectory(_destinationDirectory + "\\junk");
                         }
                        Console.Out.WriteLine("File " + fulllyQualifiedfileName + " is junk");
                        fileName = Path.GetFileName(fulllyQualifiedfileName);
                        File.Move(fulllyQualifiedfileName, _destinationDirectory + "\\junk\\" + fileName);
                    }
                    else
                    {
                        Console.Out.WriteLine(line);
                        if (line.Length != 0)
                        {
                            ParseLine(line);
                            noBlanks.Add(line);
                        }
                    }
                    counter++;

                }
                WriteCSVFile(directoryName, fileName);
                _parsedData.Clear();
            }


        }

        private static void WriteCSVFile(string directoryName, string fileName)
        {
            int count = 0;
            float previousValue = 0;
            foreach (var item in _parsedData)
            {
                if (count == 0)
                {
                    if (!Directory.Exists(_destinationDirectory + "\\processed"))
                    {
                        Directory.CreateDirectory(_destinationDirectory + "\\processed");
                    }
                    TextWriter tw = new StreamWriter(_destinationDirectory + "\\processed\\" + fileName + ".csv", false);
                    tw.WriteLine(item.Key.ToString() + "," + item.Value.ToString() + ", 0");
                    previousValue = item.Value;
                    tw.Close();
                }
                else
                {
                    TextWriter tw = new StreamWriter(_destinationDirectory + "\\processed\\" + fileName + ".csv", true);
                    tw.WriteLine(item.Key.ToString() + "," + item.Value.ToString() + "," + (item.Value - previousValue).ToString());
                    previousValue = item.Value;
                    tw.Close();
                }
                count++;

 
                
            }
        }

        private static void ParseLine(string line)
        {
            DateTime dt = new DateTime();
            string[] csvLine = line.Split(',');
            if (DateTime.TryParse(csvLine[0],out dt))
            {
               float energy = float.Parse(csvLine[3]);
                Console.Out.WriteLine("start of data");
                _parsedData.Add(dt,energy);
                
            }
        }

    }
}
