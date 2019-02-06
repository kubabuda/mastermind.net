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
        public KnuthSolverService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator, new AnswerCheckService())
        {
        
        }

        public override IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            // Knuth five-guess algorithm from wiki
            var dto = BuildInitialState(mastermindGame);
            var possibleKeys = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList();
            var keysLeft = possibleKeys.ToList(); // S
            
            string keyGuess = GetInitialKeyGuess(dto.Settings.Digits);

            for (dto.Round = 0; !IsGameFinished(dto); ++dto.Round)
            {
                possibleKeys.Remove(keyGuess);
                keysLeft.Remove(keyGuess);

                dto.LastCheck = dto.MastermindGame.PlayRound(keyGuess);
                dto.Answer = keyGuess;

                if (!dto.LastCheck.IsCorrect)
                {
                    PruneKeys(keysLeft, dto.LastCheck, keyGuess);
                    // keysLeft.RemoveAll(key => IsKeyToBeRemoved(key, keyGuess, dto.LastCheck));
                    
                    var maxScores = GetMinMax(possibleKeys,keysLeft);
                    keyGuess = GetNextGuess(maxScores, possibleKeys, keysLeft);
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }

        private string GetNextGuess(IEnumerable<string> maxScores, IEnumerable<string> possibleKeys, IEnumerable<string> keysLeft)
        {
            foreach(var maxScoredCode in maxScores) {
                if (keysLeft.Contains(maxScoredCode)) 
                {
                    return maxScoredCode;
                }
            }
            foreach (var maxScoredCode in maxScores) {
                if(possibleKeys.Contains(maxScoredCode)) {
                    return maxScoredCode;
                }
            }
            throw new InvalidOperationException("No minimax scores for possible keys!");
        }

// https://github.com/nattydredd/Mastermind-Five-Guess-Algorithm/blob/master/Five-Guess-Algorithm.cpp
        private IEnumerable<string> GetMinMax(IEnumerable<string> possibleKeys, IEnumerable<string> keysLeft)
        {
            var score = new Dictionary<string, int>();
            var scoreCount = new Dictionary<IAnswerCheckDto, int>();

            foreach(var possibleKey in possibleKeys) {
                foreach(var keyLeft in keysLeft) {
                    var check = CheckAnswer(possibleKey, keyLeft);
                    if(scoreCount.Keys.Contains(check)) 
                    {
                        var count = scoreCount[check];
                        scoreCount[check] = count + 1;
                    }
                    else
                    {
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
