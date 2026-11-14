using System;
using System.Collections.Generic;
using System.Text;

namespace QuizConfiguratorV2.Models
{
    public class Question
    {
        public string Text { get; set; } = string.Empty;

        public string Answer1 { get; set; } = string.Empty;

        public string Answer2 { get; set; } = string.Empty;

        public string Answer3 { get; set; } = string.Empty;

        public string Answer4 { get; set; } = string.Empty;

        public int CorrectAnswerIndex { get; set; }

    }
}
