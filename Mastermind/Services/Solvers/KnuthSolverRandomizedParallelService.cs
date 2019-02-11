using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mastermind.Services.Solvers
{
    /*
        Knuth five-guess algorithm with lowest worst-case scenario, parallelized minimax calculation
    */
    public class KnuthSolverRandomizedParallelService : KnuthSolverParallelService
    {

        public KnuthSolverRandomizedParallelService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator)
        {
        
        }

        protected override string GetNextGuess(IKnuthRoundStateDto dto, IEnumerable<string> maxScores)
        {
            var maxScoresLeft = maxScores
                .Where(maxScoredCode => dto.KeysLeft.Contains(maxScoredCode))
                .ToList();
            var maxScoresPossible = maxScores
                .Where(maxScoredCode => dto.PossibleKeys.Contains(maxScoredCode))
                .ToList();

            if (maxScoresLeft.Any()) 
            {
                return GetRandomKeyGuess(maxScoresLeft);
            } 
            else if (maxScoresPossible.Any())
            {
                return GetRandomKeyGuess(maxScoresPossible);
            }
            throw new InvalidOperationException("No minimax scores for possible keys!");
        }
    }
}
