using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLWriter.Classes.HelpClasses
{
    internal class LoadHelper
    {
        ConsoleControl consol = new ConsoleControl();   
        LoadDataHelper loadDataHelper = new LoadDataHelper();
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
        public void OpenFileDialog() {
            System.Diagnostics.Debug.WriteLine("OpenFileDialog Start");
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt;*.xml)|*.txt;*.xml|All files (*.*)|*.*";

            if (initialDirectory != "") {
                openFileDialog.InitialDirectory = initialDirectory;
            }
            if (openFileDialog.ShowDialog() == true) {
                System.Diagnostics.Debug.WriteLine("FilePath: " + openFileDialog.FileName);
                loadDataHelper.SetFilePathAndName(openFileDialog.FileName);
            }
            System.Diagnostics.Debug.WriteLine("OpenFileDialog Ende");
        }
    }
}
