using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class BruteforceSolverServiceTests
    {
        IGenerateKeyRangesService _keyRangesGenerator;
        IGameSettings _gameSettings;
        BruteforceSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = Substitute.For<IGenerateKeyRangesService>();
            _gameSettings = new GameSettings(6, 4);
            _serviceUnderTests = new BruteforceSolverService(_keyRangesGenerator, _gameSettings);
        }
    }

    public class BruteforceSolverServiceIntegrationTests
    {
        IGenerateKeyRangesService _keyRangesGenerator;
        IGameSettings _gameSettings;
        BruteforceSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = new GenerateKeyRangesService();
            _gameSettings = new GameSettings(6, 4);
            _serviceUnderTests = new BruteforceSolverService(_keyRangesGenerator, _gameSettings);
        }

        [Test]
        public void SolveGame_()
        {

        }
    }
}
