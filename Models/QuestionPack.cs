using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace QuizConfiguratorV2.Models
{
    public class QuestionPack
    {
        public string Name { get; set; } = string.Empty;

        public string Difficulty { get; set; } = "Medium";

        public int TimeLimit { get; set; } = 30;

        public ObservableCollection<Question> Questions { get; set; } = new();

    }
}
