using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace XMLWriter.Classes.StartPage
{
    internal class StartPageData
    {
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        DropDownOptionLists loadInput = new DropDownOptionLists();
        DataSets data = new DataSets();
        DropDownOptionLists dropDownLists = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();

        private static readonly string[] processTypeList = { "gfs", "rep" };
        private static readonly string[] languageList = { "Deutsch", "English", "Espanol" };
        private string selectedLanguage = languageList[0];
        private string selectedProcessType = "rep";
        //Labels
        private static string stringTitel;
        private static string stringGeneralInstructionShort;
        private static string stringGeneralInstructionLong;
        private static string stringFileNameTitel;
        private static string stringDisplayStep;
        //Buttons
        private static string stringLoadFile;
        private static string stringDeleteSet;
        //TextBox
        private static string stringSaveSet;



        public void InitTextStrings()
        {
            //Labels
            stringTitel = language.GetStringCreateDataSet();
            stringGeneralInstructionShort = language.GetStringGeneralInstruction();
            stringGeneralInstructionLong = language.GetStringGeneralInstructionText();
            stringFileNameTitel = language.GetStringFileNameTitel();
            stringDisplayStep = language.GetStringSteps() + " ";
            //Buttons
            stringLoadFile = language.GetStringLoadFile();
            stringDeleteSet = language.GetStringDeleteSet();
        }

        //Getter for Displaying Labels
        public string GetTextTitel() => stringTitel;
        public string GetTextGeneralInstructionShort() => stringGeneralInstructionShort;
        public string GetTextGeneralInstructionLong() => stringGeneralInstructionLong;
        public string GetTextFileNameTitel() => stringFileNameTitel;
        public string GetTextDisplayStep() => stringDisplayStep;
        //Getter for Displaying Buttons
        public string GetTextLoadFile() => stringLoadFile;
        public string GetTextDeleteSet() => stringDeleteSet;

        //Dropdown Getter & Setter
        public string[] GetLanguageList() => languageList;
        public string GetSelectedLanguage() => selectedLanguage;
        public void SetLangauge(string _language)
        {
            if (utility.ArrayContainsElement(languageList, _language))
            {
                selectedLanguage = _language;
                language.InitLanguage(selectedLanguage);
                stringTitel = language.GetStringCreateDataSet();
            }

        } 
        
        
        public string[] GetProcessTypeList() => processTypeList;
        public string GetSelectedProcessType() => selectedProcessType;
        public void SetProcessType(string _processType)
        {
            if (utility.ArrayContainsElement(processTypeList, _processType))
            {
                selectedProcessType = _processType;
                data.SetDataType(_processType); //Setzt auch direkt die "globale" Variable beim ändern.
            }
            
        }
        public void SetProcessActiveElement(ComboBox comboBox)
        {
            xamlHelper.SetDropDownActiveELementFor(comboBox, data.GetDataType());
        }

        public void InitNewDataSet()
        {
            data.InitNewDataSet();

        }
        public void LoadDropDownOptions()
        {
            dropDownLists.LoadAllDropDownOptionsFromTxtOrDatabase();
        }
        public void InitLanguages()
        {
            language.InitLanguage(selectedLanguage);
        }

        public void DatabaseOrTxtCheck(bool? check)
        {
            if (check == true)
            {
                loadInput.UseDataBase();
            }
            else
            {
                loadInput.DontUseDataBase();
            }
        }
        public void Reset(TextBlock textBlock)
        {
            data.ResetDataSet();
            textBlock.Text = "";
        }

    }
}
