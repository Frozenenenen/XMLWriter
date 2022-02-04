using System.Windows.Controls;

namespace XMLWriter.Classes
{
    internal class XAMLHelperFunctions
    {
        public void SetActiveELementForDropDown(ComboBox comboBox, string  selectedElement)
        {
            comboBox.SelectedIndex = comboBox.Items.IndexOf(selectedElement);
        }
        public void SetListForDropDown(ComboBox comboBox, string[] array)
        {
            comboBox.ItemsSource = array;
        }
        public void SetLabelTextFor(Label label, string text)
        {
            label.Content = text;
        }
    }
}
