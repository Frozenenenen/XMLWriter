namespace XMLWriter {
    class GUIMovementHelper {
        private static int stepIndex = 0;

        public int GetStepCount() => (stepIndex + 1);
        public int GetIndex() => stepIndex;
        public void ResetStepCount() {
            stepIndex = 0;
        }
        public bool IsFirstPage() {
            if (stepIndex == 0) {
                return true;
            }
            return false;
        }

        public void IncrementSteps() {
            System.Diagnostics.Debug.WriteLine("\nIn IncrementSteps");
            System.Diagnostics.Debug.WriteLine("Vorm Increment: " + stepIndex);
            stepIndex++;
            //data.SetStepCount(data.GetStepCount() + 1);
            System.Diagnostics.Debug.WriteLine("Nachm Increment: " + stepIndex + "\n");
        }
        public void DecrementStepsForGoingBackFromSaving() {
            System.Diagnostics.Debug.WriteLine("In DecrementStepsForSaving");
            System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + stepIndex);
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
            }
            System.Diagnostics.Debug.WriteLine("Nachm Decrement: " + stepIndex);

        }
    }
}
