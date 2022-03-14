using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace XMLWriter
{
    class GFSToXMLWriter:DataSetToXMLWriter
    {
        public void OutputToXML(int stepCountMax, List<DataSet> data, string fileName)
        {
            fileName = SetFileName(fileName);//, dataType);
            string[] output = FillList(stepCountMax, data);
            //File.WriteAllLines(pathVehicleID + "/" + pathLanguage + "/" + fileName + "_" + pathLanguage + fileExtension, output);
            File.WriteAllLines(fileName, output);
        }
        public string[] FillList(int stepCountMax, List<DataSet> data)
        {
            List<String> list = new List<string> { };
            list.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            list.Add("<GfsCollection>");
            list.Add("\t" + "<GfsContent>");

            for (int i = 0; i < stepCountMax; i++)
            {
                Console.WriteLine(i);

                list.Add(WriteStep(data.ElementAt(i).stepName));
                list.Add(WriteText(data.ElementAt(i).text));
                list.Add(WriteAnim(data.ElementAt(i).anim));
                list.Add(WriteInstruction(data.ElementAt(i).instruction));
                list.Add(WritePosID(data.ElementAt(i).positiveID));                
                list.Add(WriteNegID(data.ElementAt(i).negativeID));
                list.Add(WritePosResult(data.ElementAt(i).positiveResult));
                list.Add(WriteRepXML(data.ElementAt(i).repXML));
                list.Add(WriteActuatorTest(data.ElementAt(i).toolChoice, data.ElementAt(i).actuatorTest));
                list.Add(WriteRDBI(data.ElementAt(i).toolChoice, data.ElementAt(i).RDID));
                list.Add(WriteSmartTool(data.ElementAt(i).toolChoice, data.ElementAt(i).smartTool));
                list.Add(WriteNextStep(data.ElementAt(i).nextStep));
                list.Add(WriteLastStep(data.ElementAt(i).lastStep));

                list.Add("\t\t" + "</Gfs>");
            }

            list.Add("\t" + "</GfsContent>");
            list.Add("</GfsCollection>");

            string[] output = list.ToArray();
            return output;

        }

        private string WriteStep(string step)
        {
            return "\t\t" + "<Gfs step=\"" + step + "\">";
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
        private string WriteInstruction(string instruction)
        {
            return instruction == ""
                ? "\t\t\t" + "<instructions>" + "false" + "</instructions>"
                : "\t\t\t" + "<instructions>" + instruction + "</instructions>";
        }
        private string WritePosID(string posID)
        {
            return posID == ""
                ? "\t\t\t" + "<positiveID>" + "false" + "</positiveID>"
                : "\t\t\t" + "<positiveID>" + posID + "</positiveID>";
        }
        private string WriteNegID(string negID)
        {
            return negID == ""
                ? "\t\t\t" + "<negativeID>" + "false" + "</negativeID>"
                : "\t\t\t" + "<negativeID>" + negID + "</negativeID>";

        }
        private string WritePosResult(string posResult)
        {
            return posResult == ""
                ? "\t\t\t" + "<positiveResult>" + "false" + "</positiveResult>"
                : "\t\t\t" + "<positiveResult>" + posResult + "</positiveResult>";
        }
        private string WriteRepXML(string repXML)
        {
            return repXML == ""
                ? "\t\t\t" + "<RepXml>" + "false" + "</RepXml>"
                : "\t\t\t" + "<RepXml>" + repXML + "</RepXml>";
        }
        private string WriteActuatorTest(string toolChoice, string actuatorTest)
        {
            return actuatorTest == "" || toolChoice != "ActuatorTest" //Variable declaration in LoadInputOptions class oben
                ? "\t\t\t" + "<actuatorTest>" + "false" + "</actuatorTest>"
                : "\t\t\t" + "<actuatorTest>" + actuatorTest + "</actuatorTest>";


        }
        private string WriteRDBI(string toolChoice, string RDBI)
        {
            return RDBI == "" || toolChoice != "ReadDataByIdentifier" //Variable declaration in LoadInputOptions class oben
                ? "\t\t\t" + "<ReadData>" + "false" + "</ReadData>"
                : "\t\t\t" + "<ReadData>" + RDBI + "</ReadData>";
        }
        private string WriteSmartTool(string toolChoice, string smartTool)
        {
            return smartTool == "" || toolChoice != "SmartTool" //Variable declaration in LoadInputOptions class oben
                ? "\t\t\t" + "<SmartTool>" + "false" + "</SmartTool>"
                : "\t\t\t" + "<SmartTool>" + smartTool + "</SmartTool>";
        }
        private string WriteNextStep(bool? check)
        {
            if (check == true)
            {
                return "\t\t\t" + "<NextStep>" + "true" + "</NextStep>";
            }
            else
            {
                return "\t\t\t" + "<NextStep>" + "false" + "</NextStep>";
            }
        }
        private string WriteLastStep(bool? check)
        {
            return check == true
                ? "\t\t\t" + "<lastStep>" + "true" + "</lastStep>"
                : "\t\t\t" + "<lastStep>" + "false" + "</lastStep>";
        }
    }
}
