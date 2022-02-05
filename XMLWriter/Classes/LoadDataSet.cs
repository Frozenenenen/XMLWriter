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
        ManageDataSets dataSets = new ManageDataSets();
        XmlTextReader xtr;
        DataSet dataSet = new DataSet("","","","","","","","","","","","",false,false,"");

        public void LoadDataFromFile()
        {
            loadHelper.LookForInitialDirectory();
            OpenFileDialog();
            System.Diagnostics.Debug.WriteLine("Path: " + loadDataHelper.GetFileNameAndPath());
            System.Diagnostics.Debug.WriteLine("Path: " + loadDataHelper.GetFileNameAndPath());
            System.Diagnostics.Debug.WriteLine("Path: " + loadDataHelper.GetFileNameAndPath());
            if (!string.IsNullOrEmpty(loadDataHelper.GetFileNameAndPath()))
            {
                System.Diagnostics.Debug.WriteLine("Path: " + loadDataHelper.GetFileNameAndPath());
                System.Diagnostics.Debug.WriteLine("Path: " + loadDataHelper.GetFileNameAndPath());
                System.Diagnostics.Debug.WriteLine("Path: " + loadDataHelper.GetFileNameAndPath());
                xtr = new XmlTextReader(loadDataHelper.GetFileNameAndPath());
                FillDataSets();
            }
        }
        public void OpenFileDialog()
        {
            System.Diagnostics.Debug.WriteLine("OpenFileDialog Start");
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt;*.xml)|*.txt;*.xml|All files (*.*)|*.*";

            if (loadHelper.GetInitialDirectory()!="")
            {
                openFileDialog.InitialDirectory = loadHelper.GetInitialDirectory();
            }
            if (openFileDialog.ShowDialog() == true)
            {
                System.Diagnostics.Debug.WriteLine("FilePath: " + openFileDialog.FileName);
                loadDataHelper.SetFilePathAndName(openFileDialog.FileName);
            }
            System.Diagnostics.Debug.WriteLine("OpenFileDialog Ende");
        }
        private void FillDataSets()
        {
            int i = 0;
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nStarte Laaden!!!\n");

            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element)
                {
                    //dataSet.
                    switch (xtr.Name)
                    {
                        case "Repair":
                            dataSet.stepName = xtr.GetAttribute("step");
                            break;
                        case "Gfs":
                            dataSet.stepName = xtr.GetAttribute("step");
                            break;
                        case "content":
                            System.Diagnostics.Debug.WriteLine("Element: " + xtr.ReadElementString());
                            dataSet.text = xtr.ReadElementString();
                            break;
                        case "anim":
                            dataSet.anim = xtr.ReadElementString();
                            break;
                        case "specialStep":
                            dataSet.specialText = xtr.ReadElementString();
                            consol.ConsoleShowDataSetOfIndex(dataSet, i);
                            data.SetDataSet(dataSet);
                            GUI.IncrementSteps();
                            //dataType = "rep";
                            break;
                        case "instructions":
                            dataSet.instruction = xtr.ReadElementString();
                            break;
                        case "positiveID":
                            dataSet.positiveID = xtr.ReadElementString();
                            break;
                        case "negativeID":
                            dataSet.negativeID = xtr.ReadElementString();
                            break;
                        case "positiveResult":
                            dataSet.positiveResult = xtr.ReadElementString();
                            break;
                        case "RepXml":
                            dataSet.repXML = xtr.ReadElementString();
                            break;
                        case "actuatorTest":
                            dataSet.actuatorTest = xtr.ReadElementString();
                            break;
                        case "ReadData":
                            dataSet.RDID = xtr.ReadElementString();
                            break;
                        case "SmartTool":
                            dataSet.smartTool = xtr.ReadElementString();
                            break;
                        case "NextStep":
                            dataSet.nextStep = xtr.ReadElementString() == "true" ? true : false;
                            break;
                        case "lastStep":
                            dataSet.lastStep = xtr.ReadElementString() == "true" ? true : false;
                            if (dataSet.smartTool != "" && dataSet.smartTool != "false")
                            {
                                dataSet.toolChoice = ddList.GetToolChoice()[2];
                            }
                            else if (dataSet.actuatorTest != "" && dataSet.actuatorTest != "false")
                            {
                                dataSet.toolChoice = ddList.GetToolChoice()[1];
                            }
                            else if (dataSet.RDID != "" && dataSet.RDID != "false")
                            {
                                dataSet.toolChoice = ddList.GetToolChoice()[3];
                            }
                            if (consol.showMiscLoadData) System.Diagnostics.Debug.WriteLine("repXML= " + dataSet.repXML + "                ---LoadData.LoadDataFromFile()");
                            consol.ConsoleShowDataSetOfIndex(dataSet, i);
                            data.SetDataSet(dataSet);
                            GUI.IncrementSteps();
                            //dataType = "gfs";
                            break;
                        default:
                            if(consol.showLoadFile) System.Diagnostics.Debug.WriteLine("Fehler: " + xtr.Name);
                            if(consol.showLoadFile) System.Diagnostics.Debug.WriteLine("Fehler: " + xtr.Name);

                            break;
                    }
                    i++;
                }

            }
            data.SetStepCount(0);
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\n");
        }
    }
}
