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
            string answer = "";

            foreach (var key in keySpace)
            {
                mastermindGame.PlayRound(key);
                if(mastermindGame.LastCheck.IsCorrect)
                {
                    answer = key;
                    break;
                }
            }

            return new GameResultDto(mastermindGame.LastCheck.IsCorrect, answer, mastermindGame.RoundsPlayed);
        }
    }
}
