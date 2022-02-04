using System.Collections.Generic;
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
        public void SetTextFor(Label label, string text)
        {
            label.Content = text;
        }
        public void SetTextFor(TextBlock textBlock, string text)
        {
            textBlock.Text = text;
        }
        public void SetButtonFor(Button button, string text)
        {
            button.Content = text;
        }
        public void InitTextItems(List<Label> labelList, List<string> textList)
        {
            int i = 0;
            foreach (Label label in labelList)
            {
                SetTextFor(label, textList[i]);
                i++;
            }
        }
    }
}
