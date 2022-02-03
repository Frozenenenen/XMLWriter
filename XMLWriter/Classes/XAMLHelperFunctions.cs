using System.Windows.Controls;

namespace XMLWriter.Classes
{
    internal class XAMLHelperFunctions
    {
        public void SetActiveELementFor(ComboBox comboBox, DataSets data)
        {
            comboBox.SelectedIndex = comboBox.Items.IndexOf(data.GetDataType());
        }
    }
}
