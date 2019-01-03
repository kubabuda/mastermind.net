using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services.Solvers
{
    public class BruteforceSolverService : ISolveMastermindService
    {
        readonly IGenerateKeyRangesService _keyRangesGenerator;

        public BruteforceSolverService(IGenerateKeyRangesService keyRangesGenerator)
        {
            _keyRangesGenerator = keyRangesGenerator;
        }

        public IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            var keySpace = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings);
            
            foreach (var key in keySpace)
            {
                if(mastermindGame.LastCheck.IsCorrect)
                {
                    break;
                }
                mastermindGame.PlayRound(key);
            }

            return new GameResultDto(mastermindGame.Rounds, mastermindGame.LastCheck.IsCorrect);
        }
    }
}
