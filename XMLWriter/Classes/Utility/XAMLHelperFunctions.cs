using System.Windows.Controls;

namespace XMLWriter.Classes {
    internal class XAMLHelperFunctions {
        public void SetDropDownActiveELementFor(ComboBox comboBox, string selectedElement) {
            comboBox.SelectedIndex = comboBox.Items.IndexOf(selectedElement);
        }
        public void SetDropDownContent(ComboBox comboBox, string[] array) {
            comboBox.ItemsSource = array;
        }
        public void SetDropDownContent(ComboBox comboBox, string[] array, string selectedElement) {
            SetDropDownActiveELementFor(comboBox, selectedElement);
            SetDropDownContent(comboBox, array);
        }
        public void SetTextFor(Label label, string text) {
            label.Content = text;
        }
        public void SetTextFor(Button button, string text) {
            button.Content = text;
        }
        public void SetTextFor(TextBlock textBlock, string text) {
            textBlock.Text = text;
        }
        public void SetTextFor(TextBox textBox, string text) {
            textBox.Text = text;
        }
        public void SetTextFor(CheckBox checker, string text) {
            checker.Content = text;
        }
        public void SetStateFor(CheckBox checkBox, bool? value) {
            checkBox.IsChecked = value;
        }
        public bool IsActiveElementOf(ComboBox comboBox, string text) {
            if (comboBox.SelectedIndex == comboBox.Items.IndexOf(text)) {
                return true;
            }
            return false;
        }
    }
}
