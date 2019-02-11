using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services.Solvers
{
    /*
        Knuth five-guess algorithm with lowest worst-case scenario
    */
    public class KnuthSolverService : ASolverService
    {
        public KnuthSolverService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator, new AnswerCheckService())
        {
        
        }

        public override IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            var dto = BuildInitialState(mastermindGame);

            for (dto.Round = 0; !IsGameFinished(dto); ++dto.Round)
            {
                dto.LastCheck = dto.MastermindGame.PlayRound(dto.Answer);

                if (!dto.LastCheck.IsCorrect)
                {
                    dto.PossibleKeys.Remove(dto.Answer);
                    PruneKeysLeft(dto.KeysLeft, dto);
                    
                    var maxScores = GetMinMax(dto);
                    dto.Answer = GetNextGuess(dto, maxScores);
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }

        protected virtual string GetNextGuess(IKnuthRoundStateDto dto, IEnumerable<string> maxScores)
        {
            foreach(var maxScoredCode in maxScores) {
                if (dto.KeysLeft.Contains(maxScoredCode)) 
                {
                    return maxScoredCode;
                }
            }
            foreach (var maxScoredCode in maxScores) {
                if(dto.PossibleKeys.Contains(maxScoredCode)) {
                    return maxScoredCode;
                }
            }
            throw new InvalidOperationException("No minimax scores for possible keys!");
        }

        protected IKnuthRoundStateDto BuildInitialState(IMastermindGame mastermindGame)
        {
            var allKeys = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList();

            var dto = new KnuthSolvingRoundStateDto()
            {
                Answer = GetInitialKeyGuess(mastermindGame.Settings.Digits),
                Round = 0,
                MastermindGame = mastermindGame,
                PossibleKeys = allKeys.ToList(),
                KeysLeft = allKeys.ToList(),
            };

            return dto;
        }

        public virtual IEnumerable<string> GetMinMax(IKnuthRoundStateDto dto)
        {
            var score = new Dictionary<string, int>();

            foreach(var possibleKey in dto.PossibleKeys) {
                var scoreCount = new Dictionary<string, int>();
                foreach(var keyLeft in dto.KeysLeft) {
                    var checkValue = CheckAnswer(possibleKey, keyLeft);
                    var check = $"{checkValue.WhitePoints}.{checkValue.BlackPoints}";
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
            }
            var min = score.Values.Min();
            var result = score.Keys
                .Where(k => score[k] == min)
                .OrderBy(k => k);

            return result;
        }
    }
}
