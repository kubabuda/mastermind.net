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
    public class KnuthSolverParallelService : KnuthSolverService
    {

        public KnuthSolverParallelService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator)
        {
        
        }

        // todo unit test me
        public override IEnumerable<string> GetMinMax(IKnuthRoundStateDto dto)
        {
            var score = new  ConcurrentDictionary<string, int>();

            Parallel.ForEach(dto.PossibleKeys, possibleKey => {
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
            });

            var min = score.Values.Min();
            var result = score.Keys
                .Where(k => score[k] == min)
                .OrderBy(k => k);

            return result;
        }
    }
}
