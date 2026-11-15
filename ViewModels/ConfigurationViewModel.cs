using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using QuizConfiguratorV2.Models;
using System.Windows.Input;
using QuizConfiguratorV2.Commands;
using QuizConfiguratorV2.Services;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

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


        public ConfigurationViewModel()
        {

            NewPackCommand = new RelayCommand(_ => CreateNewPack());
            DeletePackCommand = new RelayCommand(_ => DeletePack());
            AddQuestionCommand = new RelayCommand(_ => AddQuestion());


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

            SelectedPack.Questions.Add(new Question
            {
                Text = "New Question",
                Answer1 = "",
                Answer2 = "",
                Answer3 = "",
                Answer4 = "",
                CorrectAnswerIndex = 0

            });
        }

    }

}
