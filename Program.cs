using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SolarFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
          //  DriveCommandLineSample.Test();
            bool validDirectory = false;
            while (!validDirectory)
            {
                Console.Out.Write("Enter Directory containing files you wish to process: ");
                string directoryContainingFiles = Console.ReadLine();
               if ( Directory.Exists(directoryContainingFiles))
               {
                   validDirectory= true;
                   FileUtilities.ConvertFile(directoryContainingFiles);
               }
               else
               {
                   Console.Out.WriteLine("Please enter a valid directory");
               }
             }
        }
    }
}
