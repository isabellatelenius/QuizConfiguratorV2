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
using System.Linq;
using System.Windows.Threading;

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

        private List<Question> _questions = new();
        private List<(string Text, int OriginalIndex)> _answers = new();

        private readonly DispatcherTimer _timer = new();
        private int _remainingSeconds;
        private bool _answerLocked = true;


        public PlayerView()
        {
            InitializeComponent();

            AnswerPanel.Visibility = Visibility.Collapsed;
            NextQuestionButton.Visibility = Visibility.Collapsed;
            TimerText.Visibility = Visibility.Collapsed;

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
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

            _timer.Stop();

            _currentPack = pack;

            _questions = pack.Questions
                .OrderBy(_ => Random.Shared.Next())
                .ToList();

            _questionIndex = 0;
            _score = 0;

            ResultText.Text = "";

            PackComboBox.IsEnabled = false;
            AnswerPanel.Visibility = Visibility.Visible;
            NextQuestionButton.Visibility = Visibility.Visible;
            TimerText.Visibility = Visibility.Visible;

            ShowQuestion();

        }

        private void ShowQuestion()
        {
            if (_currentPack == null)
                return;


            _timer.Stop();
            _answerLocked = true;

            Question question = _questions[_questionIndex];

            _answers = new List<(string Text, int OriginalIndex)>
            {
                (question.Answer1, 0),
                (question.Answer2, 1),
                (question.Answer3, 2),
                (question.Answer4, 3)
            }
            .OrderBy(_ => Random.Shared.Next())
            .ToList();

            Answer1Radio.IsChecked = false;
            Answer2Radio.IsChecked = false;
            Answer3Radio.IsChecked = false;
            Answer4Radio.IsChecked = false;

            Answer1Radio.Content = _answers[0].Text;
            Answer2Radio.Content = _answers[1].Text;
            Answer3Radio.Content = _answers[2].Text;
            Answer4Radio.Content = _answers[3].Text;

            Answer1Radio.IsEnabled = true;
            Answer2Radio.IsEnabled = true;
            Answer3Radio.IsEnabled = true;
            Answer4Radio.IsEnabled = true;

            QuestionNumberText.Text =
                $"Question {_questionIndex + 1} of {_questions.Count}";

            QuestionText.Text = question.Text;
            ResultText.Text = "";

            NextQuestionButton.IsEnabled = false;
            NextQuestionButton.Content =
                _questionIndex == _questions.Count - 1
                ? "Finish Quiz"
                : "Next Question";

            _remainingSeconds = Math.Max(1, _currentPack.TimeLimit);
            TimerText.Text = $"Time remaining: {_remainingSeconds}";

            _answerLocked = false;
            _timer.Start();

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _remainingSeconds--;

            TimerText.Text = $"Time remaining: {_remainingSeconds}";

            if (_remainingSeconds <= 0)
            {
                FinishQuestion(-1);

            }
        }

        private void FinishQuestion(int selectedAnswer)
        {
            if (_answerLocked || _currentPack == null)
                return;

            _answerLocked = true;
            _timer.Stop();

            Question question = _questions[_questionIndex];

            string correctAnswer = question.CorrectAnswerIndex switch
            {
                0 => question.Answer1,
                1 => question.Answer2,
                2 => question.Answer3,
                3 => question.Answer4,
                _ => ""
            };

            if (selectedAnswer == question.CorrectAnswerIndex)
            {
                _score++;
                ResultText.Text = "Correct answer!";
            }
            else if (selectedAnswer == -1)
            {
                ResultText.Text =
                    $"Time is up! Correct answer: {correctAnswer}";
            }
            else
            {
                ResultText.Text =
                    $"Incorrect! Correct answer: {correctAnswer}";
            }

            Answer1Radio.IsEnabled = false;
            Answer2Radio.IsEnabled = false;
            Answer3Radio.IsEnabled = false;
            Answer4Radio.IsEnabled = false;

            NextQuestionButton.IsEnabled = true;

        }


        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_answerLocked || _currentPack == null)
                return;

            _questionIndex++;

            if (_questionIndex < _questions.Count)
            {
                ShowQuestion();
            }
            else
            {
                _timer.Stop();

                QuestionNumberText.Text = "";
                QuestionText.Text = "Quiz completed!";

                AnswerPanel.Visibility = Visibility.Collapsed;
                NextQuestionButton.Visibility = Visibility.Collapsed;
                TimerText.Visibility = Visibility.Collapsed;

                ResultText.Text =
                    $"Your score: {_score} out of {_questions.Count}";

                PackComboBox.IsEnabled = true;

            }
        }




        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            if (_answerLocked || _currentPack == null)
                return;

            int selectedIndex = -1;

            if (sender == Answer1Radio) selectedIndex = 0;
            else if (sender == Answer2Radio) selectedIndex = 1;
            else if (sender == Answer3Radio) selectedIndex = 2;
            else if (sender == Answer4Radio) selectedIndex = 3;

            if (selectedIndex >= 0)
            {
                FinishQuestion(_answers[selectedIndex].OriginalIndex);

            }
            

        }
    }
}


