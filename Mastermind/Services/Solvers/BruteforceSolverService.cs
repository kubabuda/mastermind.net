using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }
    }
}
