using System.Linq;
using System.Xml;

namespace XMLWriter.Classes.HelpClasses {
    internal class LoadDataHelper {
        DataSetManager dataManager = new DataSetManager();
        GUIMovement GUI = new GUIMovement();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        ConsoleControl consol = new ConsoleControl();

        XmlTextReader xtr;
        private static string fileNameAndPath = "";

        public string GetFileNameAndPath() => fileNameAndPath;
        public void SetFilePathAndName(string filePath) => fileNameAndPath = filePath;
        //public void SetFilePathAndName(string filePath, string fileName) => fileNameAndPath = filePath + fileName;
        public bool IsFilePathValid() {
            return !string.IsNullOrEmpty(fileNameAndPath);
        }
        public void ReadXMLStreamAndWriteToDataSets() {
            
            xtr = new XmlTextReader(fileNameAndPath);
            int i = 0;
            dataManager.InitNewDataSet();
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nStarte Laaden!!!\n");
            while (xtr.Read()) {
                if (xtr.NodeType == XmlNodeType.Element) {
                    switch (xtr.Name) {
                        case "Repair":
                            dataManager.GetDataSets().ElementAt(i).stepName = xtr.GetAttribute("step");
                            break;
                        case "Gfs":
                            dataManager.GetDataSets().ElementAt(i).stepName = xtr.GetAttribute("step");
                            break;
                        case "content":
                            System.Diagnostics.Debug.WriteLine("Element: " + xtr.ReadElementString());
                            dataManager.GetDataSets().ElementAt(i).text = xtr.ReadElementString();
                            break;
                        case "anim":
                            dataManager.GetDataSets().ElementAt(i).anim = xtr.ReadElementString();
                            break;
                        case "specialStep":
                            dataManager.GetDataSets().ElementAt(i).specialText = xtr.ReadElementString();
                            //Speichern dataType = "rep";
                            dataManager.InitNewDataSet();
                            GUI.IncrementSteps();
                            i++;
                            break;
                        case "instructions":
                            dataManager.GetDataSets().ElementAt(i).instruction = xtr.ReadElementString();
                            break;
                        case "positiveID":
                            dataManager.GetDataSets().ElementAt(i).positiveID = xtr.ReadElementString();
                            break;
                        case "negativeID":
                            dataManager.GetDataSets().ElementAt(i).negativeID = xtr.ReadElementString();
                            break;
                        case "positiveResult":
                            dataManager.GetDataSets().ElementAt(i).positiveResult = xtr.ReadElementString();
                            break;
                        case "RepXml":
                            dataManager.GetDataSets().ElementAt(i).repXML = xtr.ReadElementString();
                            break;
                        case "actuatorTest":
                            dataManager.GetDataSets().ElementAt(i).actuatorTest = xtr.ReadElementString();
                            break;
                        case "ReadData":
                            dataManager.GetDataSets().ElementAt(i).RDID = xtr.ReadElementString();
                            break;
                        case "SmartTool":
                            dataManager.GetDataSets().ElementAt(i).smartTool = xtr.ReadElementString();
                            break;
                        case "NextStep":
                            dataManager.GetDataSets().ElementAt(i).nextStep = xtr.ReadElementString() == "true" ? true : false;
                            break;
                        case "lastStep":
                            dataManager.GetDataSets().ElementAt(i).lastStep = xtr.ReadElementString() == "true" ? true : false;
                            if (dataManager.GetDataSets().ElementAt(i).smartTool != "" && dataManager.GetDataSets().ElementAt(i).smartTool != "false") {
                                dataManager.GetDataSets().ElementAt(i).toolChoice = dropDownList.GetToolChoice()[2];
                            }
                            else if (dataManager.GetDataSets().ElementAt(i).actuatorTest != "" && dataManager.GetDataSets().ElementAt(i).actuatorTest != "false") {
                                dataManager.GetDataSets().ElementAt(i).toolChoice = dropDownList.GetToolChoice()[1];
                            }
                            else if (dataManager.GetDataSets().ElementAt(i).RDID != "" && dataManager.GetDataSets().ElementAt(i).RDID != "false") {
                                dataManager.GetDataSets().ElementAt(i).toolChoice = dropDownList.GetToolChoice()[3];
                            }
                            if (consol.showMiscLoadData) System.Diagnostics.Debug.WriteLine("repXML= " + dataManager.GetDataSets().ElementAt(i).repXML + "                ---LoadData.LoadDataFromFile()");
                            //Speichern dataType = "gfs";
                            dataManager.InitNewDataSet();
                            GUI.IncrementSteps();
                            i++;
                            break;
                        default:
                            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("Fehler: " + xtr.Name);
                            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("Fehler: " + xtr.Name);

                            break;
                    }
                }
            }
            dataManager.SetStepCount(0);
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\n");
        }
        private void SaveDataSet(DataSet dataSet) {
            dataManager.SetDataSet(dataSet);
        }
    }
}
