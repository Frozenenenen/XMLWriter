using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes.HelpClasses
{
    internal class LoadDataHelper
    {
        private static string fileNameAndPath = "";

        public string GetFileNameAndPath() => fileNameAndPath;
        public void SetFilePathAndName(string filePath) => fileNameAndPath = filePath;
        //public void SetFilePathAndName(string filePath, string fileName) => fileNameAndPath = filePath + fileName;
    }
}
