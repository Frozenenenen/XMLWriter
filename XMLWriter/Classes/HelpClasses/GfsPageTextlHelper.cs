using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageTextlHelper {
        DataSetService dataSetService = new DataSetService();
        UtilityFunctions utility = new UtilityFunctions();
        Language language = new Language();
        GUIMovementHelper guiHelper = new GUIMovementHelper();
        DropDownOptionLists dropDownList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        ConsoleControl consol = new ConsoleControl();

        //Label
        //linke Seite
        public void SetLabelStepName(Label step) {
            xamlHelper.SetTextFor(step, language.GetStringStepTitel() + ": " + guiHelper.GetStepCount());
        }
        public void SetLabelContent(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringContent());
        }
        public void SetLabelAnimation(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringAnim());
        }
        public void SetLabelInstruction(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringInstruction());
        }
        public void SetLabelTitel(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPleaseFill());
        }
        //rechte Seite
        public void SetLabelPositiveID(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPosID());
        }
        public void SetLabelNegativeID(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringNegID());
        }
        public void SetLabelPositiveResult(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringPosResult());
        }
        public void SetLabelRepXML(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringRepXML());
        }
        public void SetLabelActuatorTesst(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringActuatorTest());
        }
        public void SetLabelRDID(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringReadData());
        }
        public void SetLabelSmartTool(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringSmartTool());
        }
        public void SetLabelSmartToolOption(Label label) {
            xamlHelper.SetTextFor(label, language.GetStringOptional());
        }//eh? Ich glaub das gibts nicht mehr
        //Buttons
        public void SetButtonNext(Button next) {
            xamlHelper.SetTextFor(next, language.GetStringNext());
        }
        public void SetButtonSave(Button save) {
            xamlHelper.SetTextFor(save, language.GetStringSave());
        }
        public void SetButtonDelete(Button delete) {
            xamlHelper.SetTextFor(delete, language.GetStringReset());
        }
        public void SetButtonBack(Button back) {
            xamlHelper.SetTextFor(back, language.GetStringBack());
        }
    }
}
