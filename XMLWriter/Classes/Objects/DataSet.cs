using System;
using System.Collections.Generic;
using XMLWriter.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;

namespace XMLWriter
{
    class DataSet
    {
        public string stepName="";
        public string text="";
        public string anim="";
        //Rep-spezifische Variable
        public string specialText="";
        //Gfs-spezifische Variablen
        public string instruction="";
        public string positiveID="";
        public string negativeID="";
        public string positiveResult="";
        public string repXML="";
        public string actuatorTest="";
        public string RDID="";
        public string smartTool="";
        public bool? nextStep=false;
        public bool? lastStep=false;
        public string toolChoice="";

        //private static List<DataSet> dataSets = new List<DataSet>();

        public DataSet(string _steps, string _stepTexts, string _stepAnims, string _stepSpecial, string _stepInstruction, string _stepPositiveID, 
            string _stepNegativeID, string _stepPositiveResult, string _stepRepXML, string _stepActuatorTest, string _stepRDBI, 
            string _stepSmartTool, bool? _stepNextStep, bool? _stepLastStep, string _stepToolChoice)
        {
            stepName = _steps;
            text = _stepTexts;
            anim = _stepAnims;
            //Rep-spezifische Variable
            specialText = _stepSpecial;
            //Gfs-spezifische Variablen
            instruction = _stepInstruction;
            positiveID = _stepPositiveID;
            negativeID = _stepNegativeID;
            positiveResult = _stepPositiveResult;
            repXML = _stepRepXML;
            actuatorTest = _stepActuatorTest;
            RDID = _stepRDBI;
            smartTool = _stepSmartTool;
            nextStep = _stepNextStep;
            lastStep = _stepLastStep;
            toolChoice = _stepToolChoice;
        }
        
        
    }

    
}
