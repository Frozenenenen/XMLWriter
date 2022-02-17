using XMLWriter.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter {
    class GUIMovementHelper {
        private static int stepIndex = 0;
        private static int stepIndexMax = 0;

        public int GetStepCount() => (stepIndex + 1);
        public int GetStepCountMax() => (stepIndexMax + 1);
        public int GetIndex() => stepIndex;
        public int GetIndexMax() => stepIndexMax;
        public void ResetStepCount() {
            stepIndex = 0;
        }
        public void ResetStepCountMax() {
            stepIndexMax = 0;
        }
        public bool IsFirstPage() {
            if (stepIndex == 0) {
                return true;
            }
            return false;
        }
        public bool IsLastPage() {
            if (stepIndex == stepIndexMax) {
                return true;
            }
            return false;
        }

        public void IncrementSteps() {
            System.Diagnostics.Debug.WriteLine("\nIn IncrementSteps");
            System.Diagnostics.Debug.WriteLine("Vorm Increment: " + stepIndex);
            if (IsLastPage()) {
                stepIndexMax++;
                //data.SetStepCountMax(data.GetStepCountMax() + 1);
            }
            stepIndex++;
            //data.SetStepCount(data.GetStepCount() + 1);
            System.Diagnostics.Debug.WriteLine("Nachm Increment: " + stepIndex + "\n");
        }
        public void DecrementStepsForGoingBackFromSaving() {
            System.Diagnostics.Debug.WriteLine("In DecrementStepsForSaving");
            System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + stepIndex);
            if (IsLastPage()) {
                stepIndexMax--;
                //data.SetStepCountMax(data.GetStepCountMax() - 1);
            }
            stepIndex--;
            //data.SetStepCount(data.GetStepCount() - 1);
            System.Diagnostics.Debug.WriteLine("Nachm Decrement: " + stepIndex);
        }
        public void DecrementSteps() {
            System.Diagnostics.Debug.WriteLine("In DecrementSteps");
            System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + stepIndex);
            if (!IsFirstPage()) {
                stepIndex--;
            }
            System.Diagnostics.Debug.WriteLine("Nachm Decrement: " + stepIndex);
        }
        public void DecrementStepsMax() {
            System.Diagnostics.Debug.WriteLine("In DecrementStepsMax");
            System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + stepIndex);
            if (!IsFirstPage()) {
                stepIndex--;
                stepIndexMax--;
            }
            System.Diagnostics.Debug.WriteLine("Nachm Decrement: " + stepIndex);

        }
    }
}
