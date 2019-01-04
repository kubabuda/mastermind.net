using Mastermind.Models;
using Mastermind.Services.Interfaces;
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
            // Knuth five-guess algorithm
            var keySpace = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList();

            for (int round = 0; round < mastermindGame.Settings.RoundLimit && !mastermindGame.LastCheck.IsCorrect; ++round)
            {
                string keyGuess = GetKeyGuess(keySpace, round);
                var check = mastermindGame.PlayRound(keyGuess);

                if (check.IsCorrect)    // refactor: simplify control flow
                {
                    break;
                }
                else
                {
                    keySpace.Remove(keyGuess);
                    keySpace.RemoveAll(key => IsKeyToBeRemoved(key, keyGuess, check));
                }
            }

            return new GameResultDto(mastermindGame.RoundsPlayed, mastermindGame.LastCheck.IsCorrect);
        }

        private static string GetKeyGuess(List<string> keySpace, int round)
        {
            var i = _rnd.Next(keySpace.Count);  // refactor: move out

            return keySpace[i];
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
