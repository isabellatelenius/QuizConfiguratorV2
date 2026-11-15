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
using System.ComponentModel;
using QuizConfiguratorV2.ViewModels;


namespace QuizConfiguratorV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()

        {
            InitializeComponent();

            if (ConfigurationViewControl.DataContext is ConfigurationViewModel viewModel)
            {
                PlayerViewControl.SetQuestionPack(viewModel.QuestionPacks);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConfigurationViewControl.DataContext is ConfigurationViewModel viewModel)
            {
                viewModel.SaveQuestionPacks();
            }
            base.OnClosing(e);
        }
    }
}