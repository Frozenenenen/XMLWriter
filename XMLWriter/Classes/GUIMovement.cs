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
                data.SetStepCountMax(data.GetStepCountMax() + 1);
            }
            data.SetStepCount(data.GetStepCount() + 1);
        }
        public void DecrementStepsForSaving()
        {
            if (data.GetStepCount() == data.GetStepCountMax())
            {
                data.SetStepCountMax(data.GetStepCountMax() - 1);
            }
            data.SetStepCount(data.GetStepCount() - 1);
        }
        public void DecrementSteps()
        {
            data.SetStepCount(data.GetStepCount() - 1);
        }
        public void DecrementStepsMax()
        {
            data.SetStepCount(data.GetStepCount() - 1);
            data.SetStepCountMax(data.GetStepCountMax() - 1);

        }
    }
}
