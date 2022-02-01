using System;
using System.IO;
using System.Collections.Generic;

namespace XMLWriter
{
    class WriteGFSToXML:WriteToXML
    {
        public void OutputToXML(int stepCountMax, List<string> toolChoice, List<string> step, List<string> text, List<string> anim, List<string> instruction, List<string> posID, List<string> negID, List<string> posResult, List<string> repXML, List<string> actuatorTest, List<bool?> checkActuatorTest, List<string> RDBI, List<bool?> checkRDBI, List<string> smartTool, List<bool?> checkSmartTool, List<bool?> nextStep, List<bool?> lastStep, string fileName, string dataType)
        {
            fileName = SetFileName(fileName);//, dataType);
            string[] output = FillList(stepCountMax, step, toolChoice, text, anim, instruction, posID, negID, posResult, repXML, actuatorTest, checkActuatorTest, RDBI, checkRDBI, smartTool, checkSmartTool, nextStep, lastStep);
            //File.WriteAllLines(pathVehicleID + "/" + pathLanguage + "/" + fileName + "_" + pathLanguage + fileExtension, output);
            File.WriteAllLines(fileName + "_" + pathLanguage + fileExtension, output);
        }
        public string[] FillList(int stepCountMax, List<string> step, List<string> toolChoice, List<string> text, List<string> anim, List<string> instruction, List<string> posID, List<string> negID, List<string> posResult, List<string> repXML, List<string> actuatorTest, List<bool?> checkActuatorTest, List<string> RDBI, List<bool?> checkRDBI, List<string> smartTool, List<bool?> checkSmartTool, List<bool?> nextStep, List<bool?> lastStep)
        {
            List<String> list = new List<string> { };
            list.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            list.Add("<GfsCollection>");
            list.Add("\t" + "<GfsContent>");

            for (int i = 0; i < stepCountMax; i++)
            {
                Console.WriteLine(i);

                list.Add(WriteStep(step[i]));
                list.Add(WriteText(text[i]));
                list.Add(WriteAnim(anim[i]));
                list.Add(WriteInstruction(instruction[i]));
                list.Add(WritePosID(posID[i]));                
                list.Add(WriteNegID(negID[i]));
                list.Add(WritePosResult(posResult[i]));
                list.Add(WriteRepXML(repXML[i]));
                list.Add(WriteActuatorTest(toolChoice[i], actuatorTest[i]));
                list.Add(WriteRDBI(toolChoice[i], RDBI[i]));
                list.Add(WriteSmartTool(toolChoice[i], smartTool[i]));
                list.Add(WriteNextStep(nextStep[i]));
                list.Add(WriteLastStep(lastStep[i]));

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
