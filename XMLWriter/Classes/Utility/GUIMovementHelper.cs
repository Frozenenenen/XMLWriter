using XMLWriter.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class GUIMovementHelper
    {
        private static int stepIndex = 0;
        private static int stepIndexMax = 0;
        ConsoleControl consol = new ConsoleControl();

        public int GetStepCount() => (stepIndex+1);
        public int GetStepCountMax() => (stepIndexMax+1);
        public int GetIndex() => stepIndex;
        public int GetIndexMax() => stepIndexMax;
        public void ResetStepCount() {
            stepIndex = 0;
        }
        public void ResetStepCountMax() {
            stepIndexMax = 0;
        }
        public bool IsFirstPage() {
            if (stepIndex==0) {
                return true;
            }
            return false;
        }

        public void IncrementSteps()
        {
            if(consol.showInDecrement) System.Diagnostics.Debug.WriteLine("\nIn IncrementSteps");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Increment: " + stepIndex);
            if (stepIndex == stepIndexMax)
            {
                stepIndexMax++;
                //data.SetStepCountMax(data.GetStepCountMax() + 1);
            }
            stepIndex++;
            //data.SetStepCount(data.GetStepCount() + 1);
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Increment: " + stepIndex + "\n");
        }
        public void DecrementStepsForGoingBackFromSaving()
        {
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("In DecrementStepsForSaving");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + stepIndex);
            if (stepIndex == stepIndexMax)
            {
                stepIndexMax--;
                //data.SetStepCountMax(data.GetStepCountMax() - 1);
            }
            stepIndex--;
            //data.SetStepCount(data.GetStepCount() - 1);
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Decrement: " + stepIndex);
        }
        public void DecrementSteps()
        {
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("In DecrementSteps");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Decrement: "+ stepIndex);
            stepIndex--;
            //data.SetStepCount(data.GetStepCount() - 1);
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Decrement: "+ stepIndex);
        }
        public void DecrementStepsMax()
        {
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("In DecrementStepsMax");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + stepIndex);
            stepIndex--;
            stepIndexMax--;
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Decrement: " + stepIndex);

        }
    }
}
