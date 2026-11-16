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

        private bool _allowClose = false;
        private bool _savingOnClose = false;

        public MainWindow()

        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            if (ConfigurationViewControl.DataContext is ConfigurationViewModel viewModel)
            {
          
                PlayerViewControl.SetQuestionPack(viewModel.QuestionPacks);
            }
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;

            if (ConfigurationViewControl.DataContext
                is ConfigurationViewModel viewModel)
            {
                await viewModel.LoadQuestionPacksAsync();
            }
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            if (_allowClose)
            {
                base.OnClosing(e);
                return;
            }

            e.Cancel = true;

            if (_savingOnClose)
                return;

            _savingOnClose = true;

            try
            {
                if (ConfigurationViewControl.DataContext
                    is ConfigurationViewModel viewModel)
                {
                    await viewModel.SaveQuestionPacks();

                }

                _allowClose = true;
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save: {ex.Message}");

            }
            finally
            {
                _savingOnClose = false;
            }
        }
    }
}