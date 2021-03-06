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
            SetDropDownContent(comboBox, array);
            SetDropDownActiveELementFor(comboBox, selectedElement);
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
        public void SetTooltipFor(Button button, string text) { 
            button.ToolTip = text;
        }
        public void SetStateFor(CheckBox checkBox, bool? value) {
            checkBox.IsChecked = value;
        }
        public string GetActiveElementOf(ComboBox comboBox) {
            int index = comboBox.SelectedIndex;
            return comboBox.SelectedValue.ToString();
        }
        public bool IsActiveElementOf(ComboBox comboBox, string text) {
            if (comboBox.SelectedIndex == comboBox.Items.IndexOf(text)) {
                return true;
            }
            return false;
        }

    }
}
