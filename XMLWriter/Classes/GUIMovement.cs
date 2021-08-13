using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class GUIMovement
    {
        DataSet data = new DataSet();
        public void IncrementSteps()
        {
            
            if (data.GetStepCount() == data.GetStepCountMax())
            {
                data.IncrementMaxSteps();
            }
            data.IncrementStepCount();
        }
        public void DecrementStepsForSaving()
        {
            if (data.GetStepCount() == data.GetStepCountMax())
            {
                data.DecrementMaxSteps();
            }
            data.DecrementStepCount();
        }
        public void DecrementSteps()
        {
            data.DecrementStepCount();
        }
        public void DecrementStepsMax() 
        {
            data.DecrementStepCount();
            data.DecrementMaxSteps();

        }
    }
}
