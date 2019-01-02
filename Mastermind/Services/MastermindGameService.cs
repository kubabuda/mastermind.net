using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services
{
    public class MastermindGameService: IMastermindGame
    {
        private readonly string _correctAnswer;
        private readonly ICheckAnswersService _checkAnswersService;
        private readonly List<string> _answers;

        public IEnumerable<string> Answers { get => _answers; }
        public Dictionary<string, IAnswerCheckDto> AnswerChecks { get; private set; }
        public int Rounds { get => _answers.Count; }
        public int AnswerLength { get => _correctAnswer.Length; }
        public bool IsFinished
        {
            get => _answers.Count != 0 && AnswerChecks[_answers.Last()].IsCorrect;
        }
        public IAnswerCheckDto InitialCheckState { get => _checkAnswersService.BuildAnswerCheck(_correctAnswer, 0, 0); }

        public MastermindGameService(string correctAnswer, ICheckAnswersService checkAnswersService)
        {
            _checkAnswersService = checkAnswersService;
            _correctAnswer = correctAnswer;
            _answers = new List<string>();
            AnswerChecks = new Dictionary<string, IAnswerCheckDto>();
        }

        public IAnswerCheckDto PlayRound(string answerToCheck)
        {
            _answers.Add(answerToCheck);
            var result = _checkAnswersService.CheckAnswer(_correctAnswer, answerToCheck);
            AnswerChecks[answerToCheck] = result;

            return result;
        }
    }
}
