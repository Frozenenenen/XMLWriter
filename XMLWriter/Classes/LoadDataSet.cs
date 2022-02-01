﻿using Microsoft.Win32;
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
        DropDownOptionLists ddList = new DropDownOptionLists(); 
        ConsoleControl consol = new ConsoleControl();  
        private static string fileNameAndPath = "";
        private static string dataType = "";
        public string GetFileNameAndPath() => fileNameAndPath;
        public string GetDataType() => dataType;

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


            if (consol.showLoad)
                System.Diagnostics.Debug.WriteLine("\nStarte Laden!!!\n");
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
                            data.SaveSet(stepName, text, anim, specialStep);
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
                            if (smartTool!="" && smartTool!= "false")
                            {
                                toolChoice = ddList.GetToolChoice()[2];
                            }
                            else if(actuatorTest!="" && actuatorTest != "false")
                            {
                                toolChoice = ddList.GetToolChoice()[1];
                            }
                            else if (readData != "" && readData !="false")
                            {
                                toolChoice = ddList.GetToolChoice()[3];
                            }
                            data.SaveSet(toolChoice, stepName, text, anim, instruction, positiveID, negativeID,
                                            positiveResult, repXml, actuatorTest, readData, smartTool, nextStep, lastStep);
                            if(consol.showMiscLoadData) System.Diagnostics.Debug.WriteLine("repXML= " + repXml + "                ---LoadData.LoadDataFromFile()");
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
            if (consol.showLoad)
            System.Diagnostics.Debug.WriteLine("\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\n");
        }
    }
}
