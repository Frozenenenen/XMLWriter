using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLWriter.Classes.HelpClasses {
    internal class RepPageHelper {
        DataSetService dataSetService = new DataSetService();
        GUIMovementHelper gui = new GUIMovementHelper();
        DataSet dataSet;
        Language language = new Language();
        ConsoleControl consol = new ConsoleControl();

        public void InitDataSet() {
            dataSet = dataSetService.GetDataSets().ElementAt(gui.GetIndex());
        }
        public void AddCurrentDataSetToList() {
            //dataSetService.InitNewDataSet();
            WriteInputToDataSet();
            dataSetService.SetDataSet(dataSet);
        }
        public void PrepareNextPage() {
            gui.IncrementSteps();
        }
        private void WriteInputToDataSet() {

            dataSet.stepName = inputStepName.Text;
            dataSet.text = inputText.Text;
            dataSet.anim = inputAnim.Text;
            dataSet.specialText = inputSpecialText.Text;
        }
    }
}
