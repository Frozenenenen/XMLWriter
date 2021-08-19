using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace XMLWriter.Classes
{
    class LoadDataSet
    {
        DataSet data = new DataSet();
        GUIMovement GUI = new GUIMovement();
        private string fileNameAndPath = "";
        private string dataType = "";
        public string GetFileNameAndPath() => fileNameAndPath;
        public string GetDataType() => dataType;

        private string stepName;
        private string text;
        private string anim;
        private string specialStep;
        private string instruction;
        private string positiveID;
        private string negativeID;
        private string positiveResult;
        private string repXML;
        private string actuatorTest;
        private string readData;
        private string smartTool;
        private bool? nextStep;
        private bool? lastStep;


        public void LoadDataFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt;*.xml)|*.txt;*.xml|All files (*.*)|*.*";
            //Falls der Standardordner gewechselt werden soll
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
            if (openFileDialog.ShowDialog() == true)
            {
                fileNameAndPath = openFileDialog.FileName;
            }
            XmlTextReader xtr;
            try
            {
                xtr = new XmlTextReader(fileNameAndPath);
            }
            catch
            {
                xtr = new XmlTextReader("");
            }


            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element)
                {
          
                
                    switch (xtr.Name)
                    {
                        case "Repair":
                            stepName = xtr.GetAttribute("step");
                            break;
                        case "Gfs":
                            stepName = xtr.GetAttribute("step");
                            break;
                        case "content":
                            text = xtr.ReadElementString();
                            break;
                        case "anim":
                            anim = xtr.ReadElementString();
                            break;
                        case "specialStep":
                            specialStep = xtr.ReadElementString();
                            data.SaveRepSet(stepName, text, anim, specialStep);
                            GUI.IncrementSteps();
                            dataType = "rep";
                            break;
                        case "instructions":
                            instruction = xtr.ReadElementString();
                            break;
                        case "positiveID":
                            positiveID = xtr.ReadElementString();
                            break;
                        case "negativeID":
                            negativeID = xtr.ReadElementString();
                            break;
                        case "positiveResult":
                            positiveResult = xtr.ReadElementString();
                            break;
                        case "RepXML":
                            repXML = xtr.ReadElementString();
                            break;
                        case "actuatorTest":
                            actuatorTest = xtr.ReadElementString();
                            break;
                        case "ReadData":
                            readData = xtr.ReadElementString();
                            break;
                        case "SmartTool":
                            smartTool = xtr.ReadElementString();
                            break;
                        case "NextStep":
                            nextStep = xtr.ReadElementString() == "true" ? true : false;
                            break;
                        case "lastStep":
                            lastStep = xtr.ReadElementString() == "true" ? true : false;
                            data.SaveGfsSet(stepName, text, anim, instruction, positiveID, negativeID,
                                            positiveResult, repXML, actuatorTest, readData, smartTool, nextStep, lastStep);
                            GUI.IncrementSteps();
                            dataType = "gfs";
                            break;
                        default:
                            //System.Diagnostics.Debug.WriteLine(readLine);
                            //System.Diagnostics.Debug.WriteLine(xtr.Name);

                            break;
                    }
                }
            }
        }
    }
}
