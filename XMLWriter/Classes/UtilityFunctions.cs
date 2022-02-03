using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter.Classes
{
    internal class UtilityFunctions
    {
        public bool ContainsElement(string[] list, string element)
        {
            foreach (var listElement in list)
            {
                if (listElement == element)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
