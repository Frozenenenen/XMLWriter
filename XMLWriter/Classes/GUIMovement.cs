using XMLWriter.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class GUIMovement
    {
        DataSet data = new DataSet();
        ConsoleControl consol = new ConsoleControl();   
        public void IncrementSteps()
        {
            if(consol.showInDecrement) System.Diagnostics.Debug.WriteLine("In IncrementSteps");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Increment: " + data.GetStepCount());
            if (data.GetStepCount() == data.GetStepCountMax())
            {
                data.SetStepCountMax(data.GetStepCountMax() + 1);
            }
            data.SetStepCount(data.GetStepCount() + 1);
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Increment: " + data.GetStepCount());
        }
        public void DecrementStepsForSaving()
        {
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("In DecrementStepsForSaving");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Decrement: " + data.GetStepCount());
            if (data.GetStepCount() == data.GetStepCountMax())
            {
                data.SetStepCountMax(data.GetStepCountMax() - 1);
            }
            data.SetStepCount(data.GetStepCount() - 1);
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Increment: " + data.GetStepCount());
        }
        public void DecrementSteps()
        {
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("In DecrementSteps");
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Vorm Decrement: "+data.GetStepCount());
            data.SetStepCount(data.GetStepCount() - 1);
            if (consol.showInDecrement) System.Diagnostics.Debug.WriteLine("Nachm Decrement: "+data.GetStepCount());
        }
        public void DecrementStepsMax()
        {
            data.SetStepCount(data.GetStepCount() - 1);
            data.SetStepCountMax(data.GetStepCountMax() - 1);

        }
    }
}
