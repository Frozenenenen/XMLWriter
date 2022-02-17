using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace XMLWriter
{
    class RepToXMLWriter: DataSetToXMLWriter
    {
        public void OutputToXML(int stepCountMax, List<DataSet> data, string fileName) //Output to file
        {

            fileName = SetFileName(fileName);//, dataType);
            string[] output = FillList(stepCountMax, data);
            //File.WriteAllLines(pathVehicleID + "/" + pathLanguage + "/" + fileName + "_" + pathLanguage + fileExtension, output);
            File.WriteAllLines(fileName + "_" + pathLanguage + fileExtension, output);
        }
        public string[] FillList(int stepCountMax, List<DataSet> data)
        {

            List<string> list = new List<string> { };
            list.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            list.Add("<RepairCollection>");
            list.Add("\t" + "<Repairs>");

            for (int i = 0; i <= stepCountMax; i++)
            {

                list.Add(WriteStep(data.ElementAt(i).stepName));
                list.Add(WriteText(data.ElementAt(i).text));
                list.Add(WriteAnim(data.ElementAt(i).anim));
                list.Add(WriteSpecialStep(data.ElementAt(i).specialText));

                list.Add("\t\t" + "</Repair>");
            }


            list.Add("\t" + "</Repairs>");
            list.Add("</RepairCollection>");

            string[] output = list.ToArray();

            return output;
        }

        private string WriteStep(string step)
        {
            return "\t\t" + "<Repair step=\"" + step + "\">";
        }
        private string WriteText(string text)
        {
            return "\t\t\t" + "<content>" + text + "</content>";
        }
        private string WriteAnim(string anim)
        {
            return anim == ""
                ? "\t\t\t" + "<anim>" + "default" + "</anim>"
                : "\t\t\t" + "<anim>" + anim + "</anim>";
        }
        private string WriteSpecialStep(string special)
        {
            return special == ""
                ? "\t\t\t" + "<specialStep>" + "false" + "</specialStep>"
                : "\t\t\t" + "<specialStep>" + special + "</specialStep>";
        }
    }
}
