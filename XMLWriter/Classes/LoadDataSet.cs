using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLWriter.Classes
{
    class LoadDataSet
    {
        DataSet data = new DataSet();
        string fileNameAndPath = "";
        public string GetFileNameAndPath() => fileNameAndPath;


    public void LoadDataFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt;*.xml)|*.txt;*.xml|All files (*.*)|*.*";
            //Falls der Standardordner gewechselt werden soll
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
            if (openFileDialog.ShowDialog() == true)
            {
                string loadedFileContent = File.ReadAllText(openFileDialog.FileName);
                fileNameAndPath = openFileDialog.FileName;
            }

            System.Diagnostics.Debug.WriteLine(File.ReadAllText(openFileDialog.FileName));
            StreamReader sr = new StreamReader(openFileDialog.FileName);
            string readLine = sr.ReadLine();
            while (readLine != null)
            {
                System.Diagnostics.Debug.WriteLine(readLine);
                readLine = sr.ReadLine();
            }
        }
    }
}
