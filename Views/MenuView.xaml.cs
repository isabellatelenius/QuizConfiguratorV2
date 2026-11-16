using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizConfiguratorV2.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.NavigateToTab(0);
            }
        }

        private void PlayMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            

            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.NavigateToTab(1);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private async void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                await mainWindow.ReloadQuestionPacks();
            }
        }

    }
}
