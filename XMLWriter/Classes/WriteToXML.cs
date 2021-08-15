using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class WriteToXML
    {
        //protected static readonly string path = @"Testgelände/"; //Hier muss ich wohl drigend nochmal nachgucken, wie man eine Pfadsuche öffnet, wie man das normalerweise so macht
        protected static readonly string path = @"";
        protected static readonly string fileExtension = ".txt"; //später .xml

        public virtual void FillList()
        {
        }
        public virtual void OutputToXML() //Output to file
        {
        }

        public string SetFileName(string inputFileName, string dataType) //Damit keine vorherigen Daten überschrieben werden, wird der Dateiname iteriert, bis ein neuer Dateiname gefunden wurde.
        {
            string tempFileName;
            int i = 2;
            while (true)
            {

                if (!File.Exists(path + dataType + "_" + inputFileName + fileExtension) && i == 2)
                {
                    return inputFileName;
                }
                else
                {
                    tempFileName = inputFileName + Convert.ToString(i);
                    if (!File.Exists(path + dataType + "_" + tempFileName + fileExtension))
                    {
                        return tempFileName;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}
