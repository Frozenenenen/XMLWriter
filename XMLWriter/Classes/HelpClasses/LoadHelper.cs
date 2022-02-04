using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLWriter.Classes.HelpClasses
{
    internal class LoadHelper
    {
        ConsoleControl consol = new ConsoleControl();   
        private static string initialDirectory = "";
        private static string initialDirectoryFilePath = @"Files/";
        private static string initialDirectoryFileName = @"InitialDirectory.txt";


        public void LookForInitialDirectory()
        {
            StreamReader sr = new StreamReader(initialDirectoryFilePath + initialDirectoryFileName);
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine(sr);
            initialDirectory = sr.ReadLine();
        }
        public string GetInitialDirectory() => initialDirectory;
    }
}
