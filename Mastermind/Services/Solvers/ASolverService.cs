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
        static Random _rnd = new Random();
        readonly IGenerateKeyRangesService _keyRangesGenerator;
        readonly ICheckAnswersService _checkAnswersService;

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

        public ISolvingRoundStateDto BuildInitialState(IMastermindGame mastermindGame)
        {
            var dto = new SolvingRoundStateDto()
            {
                KeySpace = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList(),
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
            var aas = new string('A', length - (length / 2));
            var bbs = new string('B', length / 2);

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

        public string GetKeyGuess(ISolvingRoundStateDto dto)
        {
            var result = dto.Round == 0 ? 
                GetInitialKeyGuess(dto.Settings.Digits) : 
                GetFirstKeyGuess(dto.KeySpace);

            return result;
        }

        public bool IsKeyToBeRemoved(string key, string usedKey, IAnswerCheckDto check)
        {
            var commonCheck = _checkAnswersService.CheckAnswer(key, usedKey);

            return !IsCheckResultDifferent(check, commonCheck);
        }

        public bool IsCheckResultDifferent(IAnswerCheckDto check1, IAnswerCheckDto check2)
        {
            return check1.WhitePoints == check2.WhitePoints && check1.BlackPoints == check2.BlackPoints;
        }
    }
}
