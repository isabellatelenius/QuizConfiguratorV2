using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using QuizConfiguratorV2.Models;
using System.Windows.Input;
using QuizConfiguratorV2.Commands;
using QuizConfiguratorV2.Services;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

namespace QuizConfiguratorV2.ViewModels
{
    public class ConfigurationViewModel
    {
        private readonly DialogService _dialogService = new();
        public ObservableCollection<QuestionPack> QuestionPacks
        { get; set; } = new();

        public QuestionPack? _selectedPack;

        public QuestionPack? SelectedPack
        {
            get => _selectedPack;
            set
            {
                _selectedPack = value;
            }
        }

        public ICommand NewPackCommand { get; }


        public ConfigurationViewModel()
        {

            NewPackCommand = new RelayCommand(_ => CreateNewPack());

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
    }


}
