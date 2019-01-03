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
        readonly IGameSettings _gameSettings;

        public BruteforceSolverService(IGenerateKeyRangesService keyRangesGenerator, IGameSettings gameSettings)
        {
            _keyRangesGenerator = keyRangesGenerator;
            _gameSettings = gameSettings;
        }

        public IGameResultDto SolveGame()
        {
            throw new NotImplementedException();
        }
    }
}
