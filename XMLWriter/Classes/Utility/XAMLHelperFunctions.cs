using System.Windows.Controls;

namespace XMLWriter.Classes
{
    internal class XAMLHelperFunctions
    {
        public void SetActiveELementFor(ComboBox comboBox, string  selectedElement)
        {
            comboBox.SelectedIndex = comboBox.Items.IndexOf(selectedElement);
        }
    }
}
