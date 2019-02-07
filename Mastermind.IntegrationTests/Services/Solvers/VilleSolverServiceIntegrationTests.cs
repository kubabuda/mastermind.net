using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class VilleSolverServiceIntegrationTests
    {
        ISolveMastermindService _serviceUnderTests;
        IGameFactory _gameFactory;

        [SetUp]
        public void Setup()
        {
            _gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            // _serviceUnderTests = new BruteforceSolverService(generator);
        }

        [Test]
        public void Test()
        {
            // Arrange
            
            // Act
            
            // Assert
            // Assert.Fail();
        }
    }
}
