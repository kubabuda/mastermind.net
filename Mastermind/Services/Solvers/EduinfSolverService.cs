using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;
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
                var i = _rnd.Next(keySpace.Count);
                string keyGuess = keySpace[i];

                var check = mastermindGame.PlayRound(keyGuess);

                if (check.IsCorrect)
                {
                    break;
                }
                else
                {
                    keySpace.Remove(keyGuess);
                    keySpace.RemoveAll(key =>
                    {
                        var commonCheck = _checkAnswersService.CheckAnswer(key, keyGuess);
                        return !IsCheckDifferent(check, commonCheck);
                    });
                }
            }

            return new GameResultDto(mastermindGame.RoundsPlayed, mastermindGame.LastCheck.IsCorrect);
        }

        public bool IsCheckDifferent(IAnswerCheckDto check1, IAnswerCheckDto check2)
        {
            return check1.WhitePoints == check2.WhitePoints && check1.BlackPoints == check2.BlackPoints;
        }
    }
}
