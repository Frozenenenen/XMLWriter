using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using XMLWriter.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using XMLWriter.Classes.HelpClasses;

namespace XMLWriter.Classes
{
    class LoadDataSet
    {
        ManageDataSets data = new ManageDataSets();
        GUIMovement GUI = new GUIMovement();
        DropDownOptionLists ddList = new DropDownOptionLists(); 
        ConsoleControl consol = new ConsoleControl();
        LoadHelper loadHelper = new LoadHelper();
        LoadDataHelper loadDataHelper = new LoadDataHelper();
        XmlTextReader xtr;
        DataSet dataSet;

        private static string stepName;
        private static string text;
        private static string anim;
        private static string specialStep;
        private static string instruction;
        private static string positiveID;
        private static string negativeID;
        private static string positiveResult;
        private static string repXml;
        private static string actuatorTest;
        private static string readData;
        private static string smartTool;
        private static bool? nextStep;
        private static bool? lastStep;
        private static string toolChoice="";

        public void LoadDataFromFile()
        {
            loadHelper.LookForInitialDirectory();
            OpenFileDialog();
            if (!string.IsNullOrEmpty(loadDataHelper.GetFileNameAndPath()))
            {
                xtr = new XmlTextReader(loadDataHelper.GetFileNameAndPath());
                FillDataSets();
            }
        }
        public void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt;*.xml)|*.txt;*.xml|All files (*.*)|*.*";

            if (loadHelper.GetInitialDirectory()!="")
            {
                openFileDialog.InitialDirectory = loadHelper.GetInitialDirectory();
            }
            if (openFileDialog.ShowDialog() == true)
            {
                loadDataHelper.SetFilePathAndName(openFileDialog.FileName);
            }
            
        }
        private void FillDataSets()
        {
            int i = 0;
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nStarte Laden!!!\n");
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
                            SaveSet(i, stepName, text, anim, specialStep);
                            data.SetDataSet(dataSet);
                            GUI.IncrementSteps();
                            //dataType = "rep";
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
                        case "RepXml":
                            repXml = xtr.ReadElementString();
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
                            if (smartTool != "" && smartTool != "false")
                            {
                                toolChoice = ddList.GetToolChoice()[2];
                            }
                            else if (actuatorTest != "" && actuatorTest != "false")
                            {
                                toolChoice = ddList.GetToolChoice()[1];
                            }
                            else if (readData != "" && readData != "false")
                            {
                                toolChoice = ddList.GetToolChoice()[3];
                            }
                            SaveSet(i, toolChoice, stepName, text, anim, instruction, positiveID, negativeID,
                                            positiveResult, repXml, actuatorTest, readData, smartTool, nextStep, lastStep);
                            if (consol.showMiscLoadData) System.Diagnostics.Debug.WriteLine("repXML= " + repXml + "                ---LoadData.LoadDataFromFile()");
                            GUI.IncrementSteps();
                            //dataType = "gfs";
                            break;
                        default:
                            if(consol.showLoadFile) System.Diagnostics.Debug.WriteLine(xtr.Name);
                            if(consol.showLoadFile) System.Diagnostics.Debug.WriteLine(xtr.Name);

                            break;
                    }
                    i++;
                }

            }
            data.SetStepCount(0);
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\n");
        }
        private DataSet SaveSet(int i, string stepName, string text, string anim, string specialStep)
        {
            dataSet = data.GetDataSets().ElementAt(i);
            dataSet.stepName = stepName;
            dataSet.text = text;
            dataSet.anim = anim;
            dataSet.specialText = specialStep;
            return dataSet;
        }
        private DataSet SaveSet(int i, string toolChoice, string stepName, string text, string anim, string instruction, string positiveID, string negativeID,
                                            string positiveResult, string repXml, string actuatorTest, string readData, string smartTool, bool? nextStep, bool? lastStep)
        {
            dataSet = data.GetDataSets().ElementAt(i);
            dataSet.toolChoice = toolChoice;
            dataSet.stepName = stepName;
            dataSet.text = text;
            dataSet.anim = anim;
            dataSet.instruction = instruction;
            dataSet.positiveID = positiveID;
            dataSet.negativeID = negativeID;
            dataSet.positiveResult = positiveResult;
            dataSet.repXML = repXml;
            dataSet.actuatorTest = actuatorTest;
            dataSet.smartTool = smartTool;
            dataSet.RDID = readData;
            dataSet.nextStep = nextStep;
            dataSet.lastStep = lastStep;
            return dataSet;
        }
    }
}
