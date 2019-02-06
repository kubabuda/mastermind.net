using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class BruteforceSolverServiceIntegrationTests
    {
        BruteforceSolverService _serviceUnderTests;
        IGameFactory _gameFactory;

        [SetUp]
        public void Setup()
        {
            _gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            _serviceUnderTests = new BruteforceSolverService(generator);
        }

        [TestCase("1234", 4)]
        [TestCase("1234", 6)]
        [TestCase("12346", 6)]
        [TestCase("66666", 6)]
        public void SolveGame_SuccesfullyAt256MovesOrLess_GivenGameWith256(string answer, int colors, int roundsLimit = -1)
        {
            // Arrange
            var mastermindGame = _gameFactory.PrepareGame(answer, colors);
            if (roundsLimit == -1)
            {
                roundsLimit = (int)System.Math.Pow(colors, answer.Length);
            }

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.True(result.IsAnswerFound);
            Assert.True(result.Rounds <= roundsLimit);
            Assert.AreEqual(answer, result.Answer);
        }
    }
}
