using System;
using System.Collections.Generic;
using System.IO;

namespace XMLWriter.Classes
{
    class LoadInputOptions
    {
        /**
         * In dieser Klasse werden die Inhalte für die gfs-Dropdowns erschlossen.
         * 
         * Dabei werden für das obere Dropdown im allgemeinen die Stichwörter 
         * "ECU" oder "SmartTool" verwendet, um daraus in den CreatePath...() oder CreateSQLQuery()
         *  Strings zum Datei öffnen oder zur Datenbankfrage zu erstellen.
         *  >>Dies passiert in "LoadVariableOptions()"
         *  
         *  Für das untere Dropdown wird zusätzlich die Auswahl des oberen Dropdowns verwendet.
         *  Da der Anzeigename Deutsch und der Datei bzw Queryname in englisch ist, muss dafür jeweils
         *  vorher eine Anfrage erstellt werden um erst mal den englischen Namen zu ermitteln.
         *  Es wird also zuerst eine Anfrage erstellt, der Index gesucht und das Tupel pendant raus gesucht.
         *  >>Dies passiert in "GetFirstPartOfTupel()"
         *  Mit Hilfe des englischen Namens wird dann die Query für die eigentliche Anfrage erstellt.
         *  >>Dann Weiter mit der überladenen CreatePath...() bzw CreateSQLQuery() mit dann jeweils 2 Variablen.
         *  >>LoadVariableOptions
         *  
         *  Hierbei wird an mehreren Stellen unterschieden, ob Infos aus der Datenbank oder aus einer .txt-Datei stammen sollen.
         *  
         *  
         *  Ansonszen wird in dieser Klasse auch der Jeweils andere Teil der Informationen bezogen.
         *  Also aus Englisch->Deutsch oder aus Deutsch->Englisch, weil diese, wie oben beschrieben in Deutsch angezeigt 
         *  aber in englisch für die Verarbeitung und das Abspeichern benötigt werden.
         *  Bedeutet: XML, SQL und Zwischenspeichern alles in englisch. Für's Anzeigen dann das Deutsche raussuchen  
         *  und fürs Abspeichern jeweils das Englische raussuchen.
         *  
         *  
         *  Aktuell funktioniert es noch nicht, weil noch Logikfehler vorhanden sind, 
         *  die vor Allem zu falschen Querys bzw Filenames führen.
         **/
        Language language = new Language();
        private static bool useDataBase; //True=Database, False=txt
        private static readonly string path = @"Files/";
        private static readonly string[] toolChoice = { "", "ActuatorTest", "SmartTool", "ReadDataByIdentifier" };

        public void SetDataBase(bool? check)
        {
            if (check == true)
            {
                useDataBase = true;
            }
            else
            {
                useDataBase = false;
            }
        }
        public bool? GetDataBaseStatus() => useDataBase;
        public string[] GetToolChoice() => toolChoice;
        public string[] GetECUChoices()
        {//Loads long part of ECU_List
            string filePathAndNameOrQuery;
            if (useDataBase)
            {
                filePathAndNameOrQuery = CreateSQLQuery("ECU");
                System.Diagnostics.Debug.WriteLine("ECU Query: " + filePathAndNameOrQuery);
            }
            else
            {
                filePathAndNameOrQuery = CreatePathAndNameString("ECU");
                System.Diagnostics.Debug.WriteLine("ECU Filename: " + filePathAndNameOrQuery);
            }
            return LoadVariableOptions("long", filePathAndNameOrQuery);
        }
        public string[] GetSmartToolChoices()
        {//Loads german part of SmartTools_List
            string filePathAndName;
            if (true)//Test
            {
                if (useDataBase)
                {
                    filePathAndName = CreateSQLQuery("SmartTool");
                    System.Diagnostics.Debug.WriteLine("SmartTool Query: " + filePathAndName);
                }
                else
                {
                    filePathAndName = CreatePathAndNameString("SmartTool");
                    System.Diagnostics.Debug.WriteLine("SmartTool FileName: " + filePathAndName);
                }
                return LoadVariableOptions(language.GetStringLanguage(), filePathAndName);
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }
        }
        public string[] GetIOChoices_ActuatorTest(string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("GetIOChoices_Aktortest Start");
                if (useDataBase)
                {
                    string shortSpecificECU = GetOtherPartOfTupel("ECU", longSpecificECU, "first");
                    string SQLQuery = CreateSQLQuery(toolChoice[1], shortSpecificECU);
                    return LoadVariableOptions(language.GetStringLanguage(), SQLQuery);
                }
                else
                {
                    string shortSpecificECU = GetOtherPartOfTupel("ECU", longSpecificECU, "first"); //Soll aus der ECU_List den longspecific  ECU Index rausholen und dadurch dann die shortspecific
                    string filePathAndName = CreatePathAndNameString(toolChoice[1], shortSpecificECU);
                    System.Diagnostics.Debug.WriteLine("Aktortest FileName: >" + filePathAndName + "< - >" + shortSpecificECU);
                    return LoadVariableOptions(language.GetStringLanguage(), filePathAndName);
                }

            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }

        }
        public string[] GetMeasureValueChoices_ReadData(string longSpecificECU)
        {//Takes long string of ECU an looks up short version. Then loads languagespecific List of selected ECU
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("GetMeasureValueChoices_ReadData Start");
                if (useDataBase)
                {
                    string shortSpecificECU = GetOtherPartOfTupel("ECU", longSpecificECU, "first");
                    string SQLQuery = CreateSQLQuery(toolChoice[3], shortSpecificECU);
                    System.Diagnostics.Debug.WriteLine("ReadData SQL Query: " + SQLQuery);
                    return LoadVariableOptions(language.GetStringLanguage(), SQLQuery); ;
                }
                else
                {
                    string shortSpecificECU = GetOtherPartOfTupel("ECU", longSpecificECU, "first");
                    string filePathAndName = CreatePathAndNameString(toolChoice[3], shortSpecificECU);
                    System.Diagnostics.Debug.WriteLine("ReadData Filename: " + filePathAndName);
                    return LoadVariableOptions(language.GetStringLanguage(), filePathAndName); ;
                }
                
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }
        }
        public string[] GetMeasurementChoices_SmartTool( string specificSmartTool)
        {
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("GetMeasureValueChoices_SmartTool Start");
                if (useDataBase)
                {
                    string shortSpecificECU = GetOtherPartOfTupel("SmartTool", specificSmartTool, "first");
                    string SQLQuery = CreateSQLQuery(toolChoice[2], shortSpecificECU);
                    System.Diagnostics.Debug.WriteLine("Measure Filename: " + SQLQuery);
                    return LoadVariableOptions(language.GetStringLanguage(), SQLQuery); ;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("SM_Tupelwechsel von " + specificSmartTool);
                    string shortSpecificECU = GetOtherPartOfTupel("SmartTool", specificSmartTool, "first");
                    System.Diagnostics.Debug.WriteLine(" zu " + shortSpecificECU);
                    string filePathAndName = CreatePathAndNameString(toolChoice[2], shortSpecificECU);
                    System.Diagnostics.Debug.WriteLine("Measure Filename: " + filePathAndName);
                    return LoadVariableOptions(language.GetStringLanguage(), filePathAndName); ;
                }
                
            }
            else
            {
                string[] test = { "1", "2" };
                return test;
            }
            
        }

        private string[] LoadVariableOptions(string languageORLengthChoice, string fileNameAndPathOrSQLQuery)
        {//Depending on Chosen input Loads String and splits it apart. That gives it to another method that splits even further 
            string[] fileContentPart_EN_DE;
            if (useDataBase)
            {
                string stream = LoadInputFromDatabase(fileNameAndPathOrSQLQuery);
                fileContentPart_EN_DE = stream.Split('|');
            }
            else
            {
                string stream = LoadInputFromTxtFile(fileNameAndPathOrSQLQuery);
                fileContentPart_EN_DE = stream.Split('|');
            }
            return GetFirstOrSecondPartOfSubString(languageORLengthChoice, fileContentPart_EN_DE);
        }

        private string CreatePathAndNameString(string toolChoiceFromDropDown, string smartToolOrECU)
        { //Create path for the lower dropdown choices
            System.Diagnostics.Debug.WriteLine("In CreatePath... mit 2 Var. Inputs: >>" + toolChoiceFromDropDown + "<< >>" + smartToolOrECU + "<<");
            string filePathAndName;
            if (toolChoiceFromDropDown == toolChoice[1]) //"ActuatorTest" 
            {
                filePathAndName = path + "IO_" + smartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[2]) //"SmartTool" 
            {
                filePathAndName = path + "Measure_" + smartToolOrECU + ".txt";
            }
            else if (toolChoiceFromDropDown == toolChoice[3]) //"ReadDataByIdentifier"
            {
                filePathAndName = path + "RDID_" + smartToolOrECU + ".txt";
            }
            else
            {
                if (toolChoiceFromDropDown == "")
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath... mit 2 Var: >>" + "toolChoiceFromDropDown ist Empty" + "<<");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath... mit 2 Var: >>" + toolChoiceFromDropDown + "<<");
                }
                System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.CreatePathString mit 2var");
                filePathAndName = "";
            }
            System.Diagnostics.Debug.WriteLine("In CreatePath mit 2 Var: >>" + filePathAndName + "<< - also für unteres Dropdown");
            return filePathAndName;
        }
        private string CreatePathAndNameString(string toolChoiceFromDropDown)
        { //Create path for the upper dropdown choice

            string filePathAndName;
            if (toolChoiceFromDropDown == "ECU")
            {
                filePathAndName = path + "ECU_List.txt";
            }
            else if (toolChoiceFromDropDown == "SmartTool")
            {
                filePathAndName = path + "SmartTool_List.txt";
            }
            else
            {
                if (toolChoiceFromDropDown == "")
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath mit 1 Var: >>" + "toolChoiceFromDropDown ist Empty" + "<<");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("In CreatePath mit 1 Var: >>" + toolChoiceFromDropDown + "<<");
                }
                
                System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.CreatePathString mit 1 var");
                filePathAndName = "";
            }
            System.Diagnostics.Debug.WriteLine("In CreatePath mit 1 Var: >>" + filePathAndName + "<< - also für oberes Dropdown");
            return filePathAndName;
        }
        private string CreateSQLQuery(string toolChoiceFromDropDown)
        {
            throw new NotImplementedException();
        }
        private string CreateSQLQuery(string toolChoiceFromDropDown, string smartToolOrECU)
        {
            throw new NotImplementedException();
        }
        private string[] GetFirstOrSecondPartOfSubString(string languageORLengthChoice, string[] fileContentPart)
        {//Splits the Tupel into two and gives only the first or the second parts in a new List
            List<string> tempOutput = new List<string>();
            string[] fileContentSubPart;
            for (int i = 0; i < fileContentPart.Length - 1; i++)      //Die txt Dateien haben immer ein | am Ende, weshalb Length-1 ...
            {
                fileContentSubPart = fileContentPart[i].Split(';');
                if (languageORLengthChoice == language.GetLanguageChoises()[1] || languageORLengthChoice == "first" || languageORLengthChoice == "short")
                {//"English" || "first" || "short"
                    tempOutput.Add(fileContentSubPart[0]);
                }
                else if (languageORLengthChoice == language.GetLanguageChoises()[0] || languageORLengthChoice == "second" || languageORLengthChoice == "long")
                {//"German" || "second" || "long"
                    tempOutput.Add(fileContentSubPart[1]);
                }
                else
                {
                    tempOutput.Add("Fehler in LoadInputOptions.LoadInputFromTxtFile");
                    System.Diagnostics.Debug.WriteLine("Fehler in LoadInputOptions.GetFirstOrSecondPartOfSubString");
                }
            }
            string[] output = tempOutput.ToArray();
            return output;
        }

        
        private string LoadInputFromDatabase(string SQLQuery)
        {
            throw new NotImplementedException();
        }
        public string GetOtherPartOfTupel(string toolChoice, string wantedElement, string wantdTupelPart) 
        {//Gets Index of the item in the List it derives from and returns the opposite Part of the Tupel.
            //First part is short ECU or english name
            string firstPart;
            if (useDataBase)
            {
                string SQLQuery = CreateSQLQuery(toolChoice);
                System.Diagnostics.Debug.WriteLine("In GetOtherPartOfTupel SQL Query: " + SQLQuery);
                System.Diagnostics.Debug.WriteLine("In Get-"+ wantdTupelPart + "-PartOfTupel toolChoice: >>" + toolChoice + "<< wanted Element: >>" + wantedElement + "<<");
                if (wantdTupelPart=="first")
                {
                    int index = GetIndexOfElementFromFile(wantedElement, SQLQuery, "second");
                    firstPart = LoadVariableOptions("first", SQLQuery)[index];
                }
                else
                {
                    int index = GetIndexOfElementFromFile(wantedElement, SQLQuery, "first");
                    firstPart = LoadVariableOptions("second", SQLQuery)[index];
                }
                
            }
            else
            {
                string filePath = CreatePathAndNameString(toolChoice);
                System.Diagnostics.Debug.WriteLine("In GetFirstPartOfTupel toolChoice: >>" + toolChoice + "<< wanted Element: >>" + wantedElement + "<<");
                if (wantdTupelPart=="first")
                {
                    int index = GetIndexOfElementFromFile(wantedElement, filePath, "second");
                    firstPart = LoadVariableOptions("first", filePath)[index];
                }
                else
                {
                    int index = GetIndexOfElementFromFile(wantedElement, filePath, "first");
                    firstPart = LoadVariableOptions("second", filePath)[index];
                }
                
                
            }
            System.Diagnostics.Debug.WriteLine("GetFirstPartOfTupel - Ende. First Part: >>" + firstPart + "<<");
            return firstPart;

        }
        public string GetOtherPartOfTupel(string dropDownToolChoice, string DropDownSmartToolOrECU, string wantedSpecificTool, string firstOrSecondPart) 
        {//Gets Index of the item in the List it derives from and returns the opposite Part of the Tupel

            int index;
            string temp;
            string SmartToolOrECU_en = GetOtherPartOfTupel("ECU", DropDownSmartToolOrECU, firstOrSecondPart);
            System.Diagnostics.Debug.WriteLine(SmartToolOrECU_en);
            string fileName = CreatePathAndNameString(dropDownToolChoice, DropDownSmartToolOrECU);
                index = Array.IndexOf(LoadVariableOptions("second", fileName), wantedSpecificTool);
                System.Diagnostics.Debug.WriteLine("FirstOfTupel_Nicht-SmartTool");
            string filePathAndName = CreatePathAndNameString(dropDownToolChoice, wantedSpecificTool);
            temp = LoadVariableOptions("first", filePathAndName)[index];
            
            if (index == -1)
            {
                System.Diagnostics.Debug.WriteLine("->->->->->-> " + index + " <-<-<-<-<-<-");
                System.Diagnostics.Debug.WriteLine("-> " + dropDownToolChoice + " " + DropDownSmartToolOrECU + " <-");
                for (int i = 0; i < LoadVariableOptions("second", filePathAndName).Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine("-> " + LoadVariableOptions("second", filePathAndName)[i] + " <-");
                }
            }//Errormessage
            return temp;
        }
        public int GetIndexOfElementFromFile(string Element, string fileNameOrQuery, string firstOrSecondPart)
        {
            int index;
            string[] elements;
            elements = LoadVariableOptions(firstOrSecondPart, fileNameOrQuery);
            System.Diagnostics.Debug.WriteLine("In GetIndexOfElementFromFile: Suche Index von " + Element + " aus File: " + fileNameOrQuery);
            index = Array.IndexOf(elements,Element);
            System.Diagnostics.Debug.WriteLine("In GetIndexOfElementFromFile: Ergebnis: " + index);
            if (index == -1)
            {//Errormessage
                System.Diagnostics.Debug.WriteLine("->->->->->-> " + index + " <-<-<-<-<-<-");
                System.Diagnostics.Debug.WriteLine("-> " + toolChoice + " " + fileNameOrQuery + " <-");
                for (int i = 0; i < LoadVariableOptions("second", fileNameOrQuery).Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine("-> " + LoadVariableOptions("second", fileNameOrQuery) + " <-");
                }
            }
            return index;
        }
        private string LoadInputFromTxtFile(string fileNameAndPath)
        {
            System.Diagnostics.Debug.WriteLine("Load File: >>" + fileNameAndPath + "<<");
            StreamReader sr = new StreamReader(fileNameAndPath);
            return sr.ReadLine();
        }
    }
}
