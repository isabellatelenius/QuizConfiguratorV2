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
using System.Collections.ObjectModel;
using QuizConfiguratorV2.Models;

namespace QuizConfiguratorV2.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : System.Windows.Controls.UserControl
    {

        private QuestionPack? _currentPack;
        private int _questionIndex;
        private int _score;

        public PlayerView()
        {
            InitializeComponent();

            AnswerPanel.Visibility = Visibility.Collapsed;
            NextQuestionButton.Visibility = Visibility.Collapsed;
        }

        public void SetQuestionPack(
            ObservableCollection<QuestionPack> packs)

        {
            PackComboBox.ItemsSource = packs;
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (PackComboBox.SelectedItem is not QuestionPack pack)
            {

                MessageBox.Show("Please select a question pack.");
                return;
            }

            if (pack.Questions.Count == 0)
            {
                MessageBox.Show("This selected question pack has no questions.");
                return;
            }

            _currentPack = pack;
            _questionIndex = 0;
            _score = 0;

            ResultText.Text = "";
            AnswerPanel.Visibility = Visibility.Visible;
            NextQuestionButton.Visibility = Visibility.Visible;

            ShowQuestion();

        }

        private void ShowQuestion()
        {
            if (_currentPack == null)
                return;

            Question question = _currentPack.Questions[_questionIndex];

            QuestionNumberText.Text =
                $"Question {_questionIndex + 1} of {_currentPack.Questions.Count}";

            QuestionText.Text = question.Text;

            Answer1Radio.Content = question.Answer1;
            Answer2Radio.Content = question.Answer2;
            Answer3Radio.Content = question.Answer3;
            Answer4Radio.Content = question.Answer4;

            Answer1Radio.IsChecked = false;
            Answer2Radio.IsChecked = false;
            Answer3Radio.IsChecked = false;
            Answer4Radio.IsChecked = false;

            NextQuestionButton.Content = _questionIndex == _currentPack.Questions.Count - 1 ? "Finish" : "Next Question";


        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPack == null)
                return;

            int selectedAnswerIndex = -1;

            if (Answer1Radio.IsChecked == true) selectedAnswerIndex = 0;
            else if (Answer2Radio.IsChecked == true) selectedAnswerIndex = 1;
            else if (Answer3Radio.IsChecked == true) selectedAnswerIndex = 2;
            else if (Answer4Radio.IsChecked == true) selectedAnswerIndex = 3;

            if (selectedAnswerIndex == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            Question question = _currentPack.Questions[_questionIndex];

            if (selectedAnswerIndex == question.CorrectAnswerIndex)
            {
                _score++;

                _questionIndex++;


                if (_questionIndex < _currentPack.Questions.Count)
                {
                    ShowQuestion();
                }
                else
                {
                    QuestionNumberText.Text = "";
                    QuestionText.Text = "Quiz Completed!";

                    AnswerPanel.Visibility = Visibility.Collapsed;
                    NextQuestionButton.Visibility = Visibility.Collapsed;

                    ResultText.Text =
                        $"Your score: {_score} out of {_currentPack.Questions.Count}";

                }
            }
        }
    }
}


