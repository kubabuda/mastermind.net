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

        protected List<string> _keysLeft;

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

        protected void PruneKeys(IAnswerCheckDto dto, string keyGuess)
        {
            _keysLeft.Remove(keyGuess);
            _keysLeft.RemoveAll(key => IsKeyToBeRemoved(key, keyGuess, dto));
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
