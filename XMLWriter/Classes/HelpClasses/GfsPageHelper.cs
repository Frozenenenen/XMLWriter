using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Linq;
using XMLWriter.Classes;

namespace XMLWriter.Classes.HelpClasses {
    internal class GfsPageHelper {
        DataSetService data = new DataSetService();
        Language language = new Language();
        GUIMovementHelper gui = new GUIMovementHelper();
        DropDownOptionLists ddList = new DropDownOptionLists();
        XAMLHelperFunctions xamlHelper = new XAMLHelperFunctions();
        ConsoleControl consol = new ConsoleControl();
    }
}
