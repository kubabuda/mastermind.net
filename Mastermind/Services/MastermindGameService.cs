using System.Collections.Generic;

namespace Mastermind.Services
{
    public class MastermindGameService
    {
        private readonly string _correctAnswer;
        private List<string> _answers;

        public int AnswersCount { get => _answers.Count; }
        public int AnswerLength { get => _correctAnswer.Length; }

        public MastermindGameService(string correctAnswer)
        {
            _correctAnswer = correctAnswer;
            _answers = new List<string>();
        }
    }
}
