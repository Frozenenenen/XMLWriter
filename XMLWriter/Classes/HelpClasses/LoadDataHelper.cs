using System.Xml;

namespace XMLWriter.Classes.HelpClasses {
    internal class LoadDataHelper {
        DataSet dataSet = new DataSet("", "", "", "", "", "", "", "", "", "", "", "", false, false, "");
        ConsoleControl consol = new ConsoleControl();
        DataSetManager manageData = new DataSetManager();
        GUIMovement GUI = new GUIMovement();
        DropDownOptionLists dropDownList = new DropDownOptionLists();

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
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nStarte Laaden!!!\n");

            while (xtr.Read()) {
                if (xtr.NodeType == XmlNodeType.Element) {
                    //dataSet.
                    switch (xtr.Name) {
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
                            manageData.SetDataSet(dataSet);
                            GUI.IncrementSteps();
                            i++;
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
                            if (dataSet.smartTool != "" && dataSet.smartTool != "false") {
                                dataSet.toolChoice = dropDownList.GetToolChoice()[2];
                            }
                            else if (dataSet.actuatorTest != "" && dataSet.actuatorTest != "false") {
                                dataSet.toolChoice = dropDownList.GetToolChoice()[1];
                            }
                            else if (dataSet.RDID != "" && dataSet.RDID != "false") {
                                dataSet.toolChoice = dropDownList.GetToolChoice()[3];
                            }
                            if (consol.showMiscLoadData) System.Diagnostics.Debug.WriteLine("repXML= " + dataSet.repXML + "                ---LoadData.LoadDataFromFile()");
                            manageData.SetDataSet(dataSet);
                            GUI.IncrementSteps();
                            i++;
                            //dataType = "gfs";
                            break;
                        default:
                            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("Fehler: " + xtr.Name);
                            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("Fehler: " + xtr.Name);

                            break;
                    }

                }

            }
            manageData.SetStepCount(0);
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\n");
        }
    }
}
