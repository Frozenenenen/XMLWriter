using System.Windows.Controls;

namespace XMLWriter.Classes
{
    internal class XAMLHelperFunctions
    {
        public void SetDropDownActiveELementFor(ComboBox comboBox, string  selectedElement)
        {
            comboBox.SelectedIndex = comboBox.Items.IndexOf(selectedElement);
        }
        public void SetDropdownListFor(ComboBox comboBox, string[] array)
        {
            comboBox.ItemsSource = array;
        }
        public void SetLabelTextFor(Label label, string text)
        {
            label.Content = text;
        }
        public void SetButtonFor(Button button, string text)
        {
            button.Content = text;
        }
    }
}
