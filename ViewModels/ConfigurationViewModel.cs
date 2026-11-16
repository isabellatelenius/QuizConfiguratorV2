using System;
using System.Collections.Generic;
using System.Text;
using QuizConfiguratorV2.Dialogs;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using QuizConfiguratorV2.Models;
using System.Windows.Input;
using QuizConfiguratorV2.Commands;
using QuizConfiguratorV2.Services;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace QuizConfiguratorV2.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly DialogService _dialogService = new();
        public ObservableCollection<QuestionPack> QuestionPacks
        { get; set; } = new();

        public QuestionPack? _selectedPack;

        public QuestionPack? SelectedPack
        {
            get => _selectedPack;
            set
            {
                if (_selectedPack != value)
                {
                    _selectedPack = value;
                    SelectedQuestion = null;
                    OnPropertyChanged(nameof(SelectedPack));
                    OnPropertyChanged(nameof(CurrentQuestions));
                }
            }
        }

        private Question? _selectedQuestion;

        public Question? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                if (_selectedQuestion != value)
                {
                    _selectedQuestion = value;
                    OnPropertyChanged(nameof(SelectedQuestion));
                    NotifyCorrectAnswerChanged();
                }
            }
        }

        public ObservableCollection<Question>? CurrentQuestions
        {
            get => SelectedPack?.Questions;
        }

        public bool IsAnswer1Correct
        {
            get => SelectedQuestion?.CorrectAnswerIndex == 0;
            set
            {
                if (value && SelectedQuestion != null)
                {
                    SelectedQuestion.CorrectAnswerIndex = 0;
                    NotifyCorrectAnswerChanged();
                }
            }

        }


        public bool IsAnswer2Correct
        {
            get => SelectedQuestion?.CorrectAnswerIndex == 1;
            set
            {
                if (value && SelectedQuestion != null)
                {
                    SelectedQuestion.CorrectAnswerIndex = 1;
                    NotifyCorrectAnswerChanged();
                }
            }
        }
        public bool IsAnswer3Correct
        {
            get => SelectedQuestion?.CorrectAnswerIndex == 2;
            set
            {
                if (value && SelectedQuestion != null)
                {
                    SelectedQuestion.CorrectAnswerIndex = 2;
                    NotifyCorrectAnswerChanged();
                }
            }
        }
        public bool IsAnswer4Correct
        {
            get => SelectedQuestion?.CorrectAnswerIndex == 3;
            set
            {
                if (value && SelectedQuestion != null)
                {
                    SelectedQuestion.CorrectAnswerIndex = 3;
                    NotifyCorrectAnswerChanged();
                }
            }
        }

        private void NotifyCorrectAnswerChanged()
        {
            OnPropertyChanged(nameof(IsAnswer1Correct));
            OnPropertyChanged(nameof(IsAnswer2Correct));
            OnPropertyChanged(nameof(IsAnswer3Correct));
            OnPropertyChanged(nameof(IsAnswer4Correct));
        }

        public ICommand NewPackCommand { get; }

        public ICommand DeletePackCommand { get; }

        public ICommand AddQuestionCommand { get; }
        public ICommand DeleteQuestionCommand { get; }
        public ICommand PackOptionsCommand { get; }




        public ConfigurationViewModel()
        {

            NewPackCommand = new RelayCommand(_ => CreateNewPack());
            DeletePackCommand = new RelayCommand(_ => DeletePack());
            AddQuestionCommand = new RelayCommand(_ => AddQuestion());
            DeleteQuestionCommand = new RelayCommand(_ => DeleteQuestion());
            PackOptionsCommand = new RelayCommand(_ => OpenPackOptions());

        }

        private void CreateNewPack()
        {
            string? packName = _dialogService.ShowCreateNewPackDialog();

            if (packName == null)
            {
                return;
            }

            QuestionPacks.Add(new QuestionPack
            {
                Name = packName
            });



        }

        private void DeletePack()
        {
            if (SelectedPack == null)
            {
                return;
            }

            QuestionPacks.Remove(SelectedPack);
            SelectedPack = null;
        }

        private void AddQuestion()
        {
            if (SelectedPack == null)
            {
                return;
            }

            Question question = new Question
            {
                Text = "New Question",
                Answer1 = "",
                Answer2 = "",
                Answer3 = "",
                Answer4 = "",
                CorrectAnswerIndex = 0

            };
            SelectedPack.Questions.Add(question);
            SelectedQuestion = question;
        }

        private void DeleteQuestion()
        {
            if (SelectedPack == null || SelectedQuestion == null)
            {
                return;
            }
            SelectedPack.Questions.Remove(SelectedQuestion);
            SelectedQuestion = null;
        }

        private void OpenPackOptions()
        {
            if (SelectedPack == null)
            {
                MessageBox.Show("Please select a question pack first.");
                return;
            }

            var dialog = new PackOptionsDialog(
                SelectedPack.Name, 
                SelectedPack.Difficulty,
                SelectedPack.TimeLimit);

            if (dialog.ShowDialog() == true)
            {
                SelectedPack.Name = dialog.SelectedPackName;
                SelectedPack.Difficulty = dialog.SelectedDifficulty;
                SelectedPack.TimeLimit = dialog.SelectedTimeLimit;
            }
        }

        private readonly string _savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizConfiguratorV2", "questionpacks.json");

        public async Task SaveQuestionPacks()
        {
            try
            {
                string? directory = Path.GetDirectoryName(_savePath);


            if (directory != null)
            {
                Directory.CreateDirectory(directory);

            }

            string json = JsonSerializer.Serialize(QuestionPacks, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(_savePath, json);

        }
            catch (Exception ex)
            {
            MessageBox.Show(
            $"Could not save question packs: {ex.Message}",
            "Save error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
            }

        }

        public async Task LoadQuestionPacksAsync()
        {
            try
            {
                string? directory = Path.GetDirectoryName(_savePath);

                if (directory != null)
                {
                    Directory.CreateDirectory(directory);
                }

                if (!File.Exists(_savePath))
                {
                    await File.WriteAllTextAsync(_savePath, "[]");
                }

                string json = await File.ReadAllTextAsync(_savePath);

                var packs = JsonSerializer.Deserialize<ObservableCollection<QuestionPack>>(json);

                if (packs == null)
                    return;

                QuestionPacks.Clear();

                foreach (QuestionPack pack in packs)
                {
                    QuestionPacks.Add(pack);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not load question packs: {ex.Message}",
                    "Load Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }


        }

    }

}
