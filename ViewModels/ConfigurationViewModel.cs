using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using QuizConfiguratorV2.Models;

namespace QuizConfiguratoV2.ViewModels
{
    public class ConfigurationViewModel
    {

        public ObservableCollection<QuestionPack> QuestionPacks
        { get; set; } = new();
    }
}
