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
using System.Windows.Shapes;

namespace QuizConfiguratorV2.Dialogs
{
    /// <summary>
    /// Interaction logic for PackOptionsDialog.xaml
    /// </summary>
    public partial class PackOptionsDialog : Window
    {
        public string SelectedPackName { get; private set; } = "";
        public string SelectedDifficulty { get; private set; } = "Medium";
        public int SelectedTimeLimit { get; private set; } = 30;
        public PackOptionsDialog(string packName, string difficulty, int timeLimit)
        {
            InitializeComponent();
            SelectedPackName = packName;
            PackNameTextBox.Text = packName;
            SelectedDifficulty = difficulty;
            SelectedTimeLimit = timeLimit;

            foreach (ComboBoxItem item in DifficultyComboBox.Items)
            {
                if (item.Content.ToString() == difficulty)
                {
                    DifficultyComboBox.SelectedItem = item;
                    break;
                }

            }

        TimeLimitTextBox.Text = timeLimit.ToString();

            }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string packName = PackNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(packName))
            {
                MessageBox.Show("Please enter a pack name.");
                return;
            }

            if (!int.TryParse(TimeLimitTextBox.Text, out int timeLimit) || timeLimit <= 0)
            {
                MessageBox.Show("Please enter a valid time limit greater than 0.",
                    "Invalid Time Limit", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DifficultyComboBox.SelectedItem is not ComboBoxItem selectedItem)
            {
                MessageBox.Show("Please select a difficulty.");
                return;
            }


            SelectedPackName = packName;
            SelectedDifficulty = selectedItem.Content.ToString() ?? "Medium";
            SelectedTimeLimit = timeLimit;


            DialogResult = true;
        }

    }

}