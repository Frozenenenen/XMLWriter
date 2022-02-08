using System.Linq;
using System.Xml;

namespace XMLWriter.Classes.HelpClasses {
    internal class LoadDataService {
        
        DataSetService dataSetService = new DataSetService();
        LoadHelper loadHelper = new LoadHelper();   
        GUIMovementService GUI = new GUIMovementService();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        ConsoleControl consol = new ConsoleControl();
        XmlTextReader xtr;
        

        //public void SetFilePathAndName(string filePath, string fileName) => fileNameAndPath = filePath + fileName;
        public bool IsFilePathValid() {
            return !string.IsNullOrEmpty(loadHelper.GetFileNameAndPath());
        }
        public void ReadXMLStreamAndWriteToDataSets() {
            
            xtr = new XmlTextReader(loadHelper.GetFileNameAndPath());
            int i = 0;
            dataSetService.InitNewDataSet();
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nStarte Laaden!!!\n");
            while (xtr.Read()) {
                if (xtr.NodeType == XmlNodeType.Element) {
                    switch (xtr.Name) {
                        case "Repair":
                            dataSetService.GetDataSets().ElementAt(i).stepName = xtr.GetAttribute("step");
                            break;
                        case "Gfs":
                            dataSetService.GetDataSets().ElementAt(i).stepName = xtr.GetAttribute("step");
                            break;
                        case "content":
                            System.Diagnostics.Debug.WriteLine("Element: " + xtr.ReadElementString());
                            dataSetService.GetDataSets().ElementAt(i).text = xtr.ReadElementString();
                            break;
                        case "anim":
                            dataSetService.GetDataSets().ElementAt(i).anim = xtr.ReadElementString();
                            break;
                        case "specialStep":
                            dataSetService.GetDataSets().ElementAt(i).specialText = xtr.ReadElementString();
                            //Speichern dataType = "rep";
                            dataSetService.InitNewDataSet();
                            GUI.IncrementSteps();
                            i++;
                            break;
                        case "instructions":
                            dataSetService.GetDataSets().ElementAt(i).instruction = xtr.ReadElementString();
                            break;
                        case "positiveID":
                            dataSetService.GetDataSets().ElementAt(i).positiveID = xtr.ReadElementString();
                            break;
                        case "negativeID":
                            dataSetService.GetDataSets().ElementAt(i).negativeID = xtr.ReadElementString();
                            break;
                        case "positiveResult":
                            dataSetService.GetDataSets().ElementAt(i).positiveResult = xtr.ReadElementString();
                            break;
                        case "RepXml":
                            dataSetService.GetDataSets().ElementAt(i).repXML = xtr.ReadElementString();
                            break;
                        case "actuatorTest":
                            dataSetService.GetDataSets().ElementAt(i).actuatorTest = xtr.ReadElementString();
                            break;
                        case "ReadData":
                            dataSetService.GetDataSets().ElementAt(i).RDID = xtr.ReadElementString();
                            break;
                        case "SmartTool":
                            dataSetService.GetDataSets().ElementAt(i).smartTool = xtr.ReadElementString();
                            break;
                        case "NextStep":
                            dataSetService.GetDataSets().ElementAt(i).nextStep = xtr.ReadElementString() == "true" ? true : false;
                            break;
                        case "lastStep":
                            dataSetService.GetDataSets().ElementAt(i).lastStep = xtr.ReadElementString() == "true" ? true : false;
                            if (dataSetService.GetDataSets().ElementAt(i).smartTool != "" && dataSetService.GetDataSets().ElementAt(i).smartTool != "false") {
                                dataSetService.GetDataSets().ElementAt(i).toolChoice = dropDownList.GetToolChoice()[2];
                            }
                            else if (dataSetService.GetDataSets().ElementAt(i).actuatorTest != "" && dataSetService.GetDataSets().ElementAt(i).actuatorTest != "false") {
                                dataSetService.GetDataSets().ElementAt(i).toolChoice = dropDownList.GetToolChoice()[1];
                            }
                            else if (dataSetService.GetDataSets().ElementAt(i).RDID != "" && dataSetService.GetDataSets().ElementAt(i).RDID != "false") {
                                dataSetService.GetDataSets().ElementAt(i).toolChoice = dropDownList.GetToolChoice()[3];
                            }
                            if (consol.showMiscLoadData) System.Diagnostics.Debug.WriteLine("repXML= " + dataSetService.GetDataSets().ElementAt(i).repXML + "                ---LoadData.LoadDataFromFile()");
                            //Speichern dataType = "gfs";
                            dataSetService.InitNewDataSet();
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
            GUI.ResetStepCount();
            if (consol.showLoadFile) System.Diagnostics.Debug.WriteLine("\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\nLaden Abgeschlossen!!!\n");
        }
    }
}
