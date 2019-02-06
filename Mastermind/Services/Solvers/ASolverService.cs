using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services.Solvers
{
    public class ASolverService : ISolveMastermindService
    {
        protected static Random _rnd = new Random();
        protected readonly IGenerateKeyRangesService _keyRangesGenerator;
        protected readonly ICheckAnswersService _checkAnswersService;

        public ASolverService(IGenerateKeyRangesService keyRangesGenerator,
            ICheckAnswersService checkAnswersService)
        {
            _keyRangesGenerator = keyRangesGenerator;
            _checkAnswersService = checkAnswersService;
        }

        public virtual IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            throw new NotImplementedException();
        }

        protected ISolvingRoundStateDto BuildInitialState(IMastermindGame mastermindGame)
        {
            var dto = new SolvingRoundStateDto()
            {
                Answer = string.Empty,
                Round = 0,
                MastermindGame = mastermindGame,
            };

            return dto;
        }

        public bool IsGameFinished(ISolvingRoundStateDto dto)
        {
            return dto.Round >= dto.Settings.RoundLimit 
                || dto.LastCheck != null && dto.LastCheck.IsCorrect;
        }

        public string GetInitialKeyGuess(int length)
        {
            var aas = new string('1', length - (length / 2)); // todo get it from GenerateKeyRanges and offset
            var bbs = new string('2', length / 2);

            return string.Format($"{aas}{bbs}");
        }

        public string GetRandomKeyGuess(List<string> keySpace)
        {
            var i = _rnd.Next(keySpace.Count);

            return keySpace[i];
        }

        public string GetFirstKeyGuess(List<string> keySpace)
        {
            return keySpace[0];
        }

        protected void PruneKeys(List<string> keysLeft, ISolvingRoundStateDto dto)
        {
            keysLeft.Remove(dto.Answer);
            keysLeft.RemoveAll(key => IsKeyToBeRemoved(key, dto.Answer, dto.LastCheck));
        }

        public IAnswerCheckDto CheckAnswer(string key, string guess) {
            return _checkAnswersService.CheckAnswer(key, guess);
        }

        public bool IsKeyToBeRemoved(string key, string usedKey, IAnswerCheckDto check)
        {
            var commonCheck = CheckAnswer(key, usedKey);

            return !IsCheckResultDifferent(check, commonCheck);
        }

        public bool IsCheckResultDifferent(IAnswerCheckDto check1, IAnswerCheckDto check2)
        {
            return check1.WhitePoints == check2.WhitePoints && check1.BlackPoints == check2.BlackPoints;
        }
    }
}
