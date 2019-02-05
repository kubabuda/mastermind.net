using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services.Solvers
{
    public class KnuthSolverService : ASolverService
    {
        private List<string> _possibleKeys;
        Dictionary<string, IAnswerCheckDto> _answerResults;

        public KnuthSolverService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator, new AnswerCheckService())
        {
        
        }

        public override IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            // Knuth five-guess algorithm from wiki
            var dto = BuildInitialState(mastermindGame);
            _possibleKeys = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList();
            _keysLeft = _possibleKeys.ToList(); // S
            _answerResults = new Dictionary<string, IAnswerCheckDto>();
            
            string keyGuess = GetInitialKeyGuess(dto.Settings.Digits);

            for (dto.Round = 0; !IsGameFinished(dto); ++dto.Round)
            {
                dto.LastCheck = dto.MastermindGame.PlayRound(keyGuess);
                dto.Answer = keyGuess;
                _answerResults[keyGuess] = dto.LastCheck;

                if (!dto.LastCheck.IsCorrect)
                {
                    PruneKeys(dto.LastCheck, keyGuess);
                    var maxScores = GetMinMax();
                    keyGuess = GetNextGuess(maxScores);
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }

        private string GetNextGuess(IEnumerable<string> maxScores)
        {
            string keyGuess;
            if (maxScores.Any(s => _keysLeft.Contains(s)))
            {
                keyGuess = maxScores.Where(s => _keysLeft.Contains(s)).First();
            }
            else
            {
                keyGuess = maxScores.First();
            }

            return keyGuess;
        }

// https://github.com/nattydredd/Mastermind-Five-Guess-Algorithm/blob/master/Five-Guess-Algorithm.cpp
        private IEnumerable<string> GetMinMax()
        {
            var score = new Dictionary<string, int>();
            var scoreCount = new Dictionary<IAnswerCheckDto, int>();

            foreach(var possibleKey in _possibleKeys) {
                foreach(var keyLeft in _keysLeft) {
                    var check = CheckAnswer(possibleKey, keyLeft);
                    try
                    {
                        var count = scoreCount[check];
                        scoreCount[check] = count + 1;
                    }
                    catch(KeyNotFoundException) {
                        scoreCount[check] = 1;
                    }
                }
                var max = scoreCount.Values.Max();
                score[possibleKey] = max;
                scoreCount.Clear();
            }
            var min = score.Values.Min();
            var result = score.Keys
                .Where(k => score[k] == min)
                .OrderBy(k => k);

            return result;
        }
    }
}
