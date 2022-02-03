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
            InitializeComponent();
            Main.Content = new StartPage();
            Console.WriteLine("Start:");

        }

    }
}
