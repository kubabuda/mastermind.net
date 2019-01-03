using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services
{
    public class MastermindGameService: IMastermindGame
    {
        private readonly string _correctAnswer;
        private readonly ICheckAnswersService _checkAnswersService;
        private readonly List<string> _answers;

        public IGameSettings Settings { get; }
        public IEnumerable<string> Answers { get => _answers; }
        public Dictionary<string, IAnswerCheckDto> AnswerChecks { get; private set; }
        public int RoundsPlayed { get => _answers.Count; }
        public IAnswerCheckDto LastCheck
        {
            get => _answers.Count == 0 ?
                _checkAnswersService.BuildAnswerCheck(_correctAnswer, 0, 0) :
                AnswerChecks[_answers.Last()];
        }

        public MastermindGameService(string correctAnswer, ICheckAnswersService checkAnswersService, IGameSettings settings)
        {
            if(!checkAnswersService.IsAnswerValid(correctAnswer, settings))
            {
                throw new ArgumentException();
            }
            _correctAnswer = correctAnswer;
            _checkAnswersService = checkAnswersService;
            Settings = settings;
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
