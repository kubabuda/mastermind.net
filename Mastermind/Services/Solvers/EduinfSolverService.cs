using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services.Solvers
{
    public class EduinfSolverService : ISolveMastermindService
    {
        static Random _rnd = new Random();
        readonly IGenerateKeyRangesService _keyRangesGenerator;
        readonly ICheckAnswersService _checkAnswersService;

        public EduinfSolverService(IGenerateKeyRangesService keyRangesGenerator)
        {
            _keyRangesGenerator = keyRangesGenerator;
            _checkAnswersService = new AnswerCheckService();
        }

        public IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            // simplified Knuth five-guess algorithm from EduInf page
            var dto = GetInitialState(mastermindGame);

            for (dto.Round = 0; dto.Round < mastermindGame.Settings.RoundLimit && !mastermindGame.LastCheck.IsCorrect; ++dto.Round)
            {
                string keyGuess = GetKeyGuess(dto);

                var check = dto.MastermindGame.PlayRound(keyGuess);

                if (check.IsCorrect)    // refactor: simplify control flow
                {
                    dto.Answer = keyGuess;
                    break;
                }
                else
                {
                    dto.KeySpace.Remove(keyGuess);
                    dto.KeySpace.RemoveAll(key => IsKeyToBeRemoved(key, keyGuess, check));
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }

        public ISolvingRoundStateDto GetInitialState(IMastermindGame mastermindGame)
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
            return false;
        }

        public string GetInitialKeyGuess(int length)
        {
            var aas = new string('A', length - (length / 2));
            var bbs = new string('B', length / 2);

            return string.Format($"{aas}{bbs}");
        }

        public string GetRandomKeyGuess(List<string> keySpace)
        {
            var i = _rnd.Next(keySpace.Count);  // refactor: move out

            return keySpace[i];
        }

        public string GetKeyGuess(ISolvingRoundStateDto dto)
        {
            var result = dto.Round == 0 ? 
                GetInitialKeyGuess(dto.Settings.Digits) : 
                GetRandomKeyGuess(dto.KeySpace);

            return result;
        }

        public bool IsKeyToBeRemoved(string key, string usedKey, IAnswerCheckDto check)
        {
            var commonCheck = _checkAnswersService.CheckAnswer(key, usedKey);

            return !IsCheckDifferent(check, commonCheck);
        }

        public bool IsCheckDifferent(IAnswerCheckDto check1, IAnswerCheckDto check2)
        {
            return check1.WhitePoints == check2.WhitePoints && check1.BlackPoints == check2.BlackPoints;
        }
    }
}
