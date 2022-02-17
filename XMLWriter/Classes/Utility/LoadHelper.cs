using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLWriter.Classes.HelpClasses
{
    internal class LoadHelper
    {
        private static string initialDirectory = "";
        private static string initialDirectoryFilePath = @"Files/";
        private static string initialDirectoryFileName = @"InitialDirectory.txt";
        private static string fileNameAndPath = "";


        public void LookForInitialDirectory()
        {
            StreamReader sr = new StreamReader(initialDirectoryFilePath + initialDirectoryFileName);
            System.Diagnostics.Debug.WriteLine("InitialDirectory " + sr);
            initialDirectory = sr.ReadLine();
        }
        public string GetFileNameAndPath() => fileNameAndPath;
        public string GetInitialDirectory() => initialDirectory;
        public void OpenFileDialog() {
            System.Diagnostics.Debug.WriteLine("OpenFileDialog Start");
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt;*.xml)|*.txt;*.xml|All files (*.*)|*.*";

            if (initialDirectory != "") {
                openFileDialog.InitialDirectory = initialDirectory;
            }
            if (openFileDialog.ShowDialog() == true) {
                System.Diagnostics.Debug.WriteLine("FilePath: " + openFileDialog.FileName);
                fileNameAndPath =openFileDialog.FileName;
            }
            System.Diagnostics.Debug.WriteLine("OpenFileDialog Ende");
        }
    }
}
