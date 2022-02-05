using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLWriter.Classes
{
    internal class ConsoleControl
    {
        //dont change this one if not really neccessary
        public bool showErrors = true;

        
        
        public bool showSaveStep = true;
        public bool showInDecrement = false;
        public bool showGetOtherPart = false;
        public bool showLoadFile = true;
        public bool showStep = true;
        //gfs
        public bool showMiscGfs = false;
        public bool showGfsData = false;
        //rep
        public bool showMiscRep = false;
        //loadData
        public bool showMiscLoadData = false;
        public bool showBtn = false;
        
        public bool showMiscStarPage = true;
        public bool showWriteFile = true;//macht gerade nix
        public bool showSaveFile = false;

        public void ConsoleShowDataSetOfIndex(DataSet dataSet, int index, string a)
        {
            System.Diagnostics.Debug.WriteLine(">>>" + a + "<<<");
            System.Diagnostics.Debug.WriteLine("Tool:   " + dataSet.toolChoice);
            System.Diagnostics.Debug.WriteLine("Step:   " + dataSet.stepName);
            System.Diagnostics.Debug.WriteLine("Text:   " + dataSet.text);
            System.Diagnostics.Debug.WriteLine("Anim:   " + dataSet.anim);
            System.Diagnostics.Debug.WriteLine("instr:  " + dataSet.instruction);
            System.Diagnostics.Debug.WriteLine("posID:  " + dataSet.positiveID);
            System.Diagnostics.Debug.WriteLine("negID:  " + dataSet.negativeID);
            System.Diagnostics.Debug.WriteLine("posRes: " + dataSet.positiveResult);
            System.Diagnostics.Debug.WriteLine("repXML: " + dataSet.repXML);
            System.Diagnostics.Debug.WriteLine("A-Test: " + dataSet.actuatorTest);
            System.Diagnostics.Debug.WriteLine("SmarT:  " + dataSet.smartTool);
            System.Diagnostics.Debug.WriteLine("RDID:   " + dataSet.RDID);
            System.Diagnostics.Debug.WriteLine("Next:   " + dataSet.nextStep);
            System.Diagnostics.Debug.WriteLine("Last:   " + dataSet.lastStep);
            System.Diagnostics.Debug.WriteLine(">>>" + a + "<<<");
        }
        public void ConsoleShowDataSetOfIndex(DataSet dataSet, int index)
        {
            System.Diagnostics.Debug.WriteLine(">>> Index: " + index + " <<<");
            System.Diagnostics.Debug.WriteLine("Tool:   " + dataSet.toolChoice);
            System.Diagnostics.Debug.WriteLine("Step:   " + dataSet.stepName);
            System.Diagnostics.Debug.WriteLine("Text:   " + dataSet.text);
            System.Diagnostics.Debug.WriteLine("Anim:   " + dataSet.anim);
            System.Diagnostics.Debug.WriteLine("instr:  " + dataSet.instruction);
            System.Diagnostics.Debug.WriteLine("posID:  " + dataSet.positiveID);
            System.Diagnostics.Debug.WriteLine("negID:  " + dataSet.negativeID);
            System.Diagnostics.Debug.WriteLine("posRes: " + dataSet.positiveResult);
            System.Diagnostics.Debug.WriteLine("repXML: " + dataSet.repXML);
            System.Diagnostics.Debug.WriteLine("A-Test: " + dataSet.actuatorTest);
            System.Diagnostics.Debug.WriteLine("SmarT:  " + dataSet.smartTool);
            System.Diagnostics.Debug.WriteLine("RDID:   " + dataSet.RDID);
            System.Diagnostics.Debug.WriteLine("Next:   " + dataSet.nextStep);
            System.Diagnostics.Debug.WriteLine("Last:   " + dataSet.lastStep);
            System.Diagnostics.Debug.WriteLine(">>> Index: " + index + "<<<");
        }
        public void ConsoleShowDataSetOfIndex(DataSet dataSet)
        {
            System.Diagnostics.Debug.WriteLine(">>>---<<<");
            System.Diagnostics.Debug.WriteLine("Tool:   " + dataSet.toolChoice);
            System.Diagnostics.Debug.WriteLine("Step:   " + dataSet.stepName);
            System.Diagnostics.Debug.WriteLine("Text:   " + dataSet.text);
            System.Diagnostics.Debug.WriteLine("Anim:   " + dataSet.anim);
            System.Diagnostics.Debug.WriteLine("instr:  " + dataSet.instruction);
            System.Diagnostics.Debug.WriteLine("posID:  " + dataSet.positiveID);
            System.Diagnostics.Debug.WriteLine("negID:  " + dataSet.negativeID);
            System.Diagnostics.Debug.WriteLine("posRes: " + dataSet.positiveResult);
            System.Diagnostics.Debug.WriteLine("repXML: " + dataSet.repXML);
            System.Diagnostics.Debug.WriteLine("A-Test: " + dataSet.actuatorTest);
            System.Diagnostics.Debug.WriteLine("SmarT:  " + dataSet.smartTool);
            System.Diagnostics.Debug.WriteLine("RDID:   " + dataSet.RDID);
            System.Diagnostics.Debug.WriteLine("Next:   " + dataSet.nextStep);
            System.Diagnostics.Debug.WriteLine("Last:   " + dataSet.lastStep);
            System.Diagnostics.Debug.WriteLine(">>>---<<<");
        }

    }
}
