using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;

namespace Mastermind.Services.Solvers
{
    public class EduinfSolverService : ISolveMastermindService
    {
        readonly IGenerateKeyRangesService _keyRangesGenerator;

        public EduinfSolverService(IGenerateKeyRangesService keyRangesGenerator)
        {
            _keyRangesGenerator = keyRangesGenerator;
        }

        public IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            throw new NotImplementedException();
        }
    }
}
