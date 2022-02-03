using System;
using System.Windows;
using XMLWriter.Pages;

namespace XMLWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Diagnostics.Debug.WriteLine("!!!Start - 1!!!");
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("!!!Start - 2!!!");
            Main.Content = new StartPage();
        }

    }
}
