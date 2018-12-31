using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Services
{
    public class MastermindGameService
    {
        private readonly string _correctAnswer;
        private List<string> _answers;
        public int AnswersCount { get => _answers.Count; }
        public double AnswerLength { get; set; }

        public MastermindGameService(string correctAnswer)
        {
            _correctAnswer = correctAnswer;
            _answers = new List<string>();
        }
    }
}
