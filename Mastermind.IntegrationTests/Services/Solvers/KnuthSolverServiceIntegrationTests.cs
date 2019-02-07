using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mastermind.IntegrationTests.Services.Solvers
{
    [TestFixture]
    public class KnuthSolverServiceIntegrationTests
    {
        KnuthSolverService _serviceUnderTests;
        IGameFactory _gameFactory;

        [SetUp]
        public void Setup()
        {
            _gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            _serviceUnderTests = new KnuthSolverService(generator);
        }


        [TestCase("12", 1)]
        [TestCase("1122", 1)]
        // [TestCase("2211", 2)]
        // [TestCase("2233", 5)]
        // [TestCase("1234", 5)]
        // [TestCase("3456", 5)]
        [TestCase("6666", 5)]
        public void SolveGame_SuccesfullyAt5MovesOrLess_GivenClassicMastermind(string answer, int roundsLimit)
        {
            // Arrange
            int colors = 6;
            var mastermindGame = _gameFactory.PrepareGame(answer, colors, roundsLimit);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.Multiple(() => {
                Assert.IsTrue(result.IsAnswerFound);
                Assert.True(result.Rounds <= roundsLimit);
                Assert.AreEqual(answer, result.Answer);
            });
        }

        // [TestCase("6111", 5)]
        // [TestCase("5115", 5)]
        // [TestCase("6521", 5)]
        // [TestCase("5621", 5)]
        public void SolveGame_At5MovesOrLess_GivenEdgeCase(string answer, int roundsLimit)
        {
            // Arrange
            int colors = 6;
            var mastermindGame = _gameFactory.PrepareGame(answer, colors, roundsLimit);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.Multiple(() => {
                Assert.IsTrue(result.IsAnswerFound);
                Assert.True(result.Rounds <= roundsLimit);
                Assert.AreEqual(answer, result.Answer);
            });
        }

        // Knuth solver works few seconds on i5 for Deluxe Mastermind(8,5). It's too slow for realtime. 
        // Tests commented out for now.
        // [TestCase("12346", 8, 6)]
        public void SolveGame_SuccesfullyIn8MovesOrLess_GivenDeluxeMastermind(string answer, int colors, int roundsLimit)
        {
            // Arrange
            var mastermindGame = _gameFactory.PrepareGame(answer, colors, roundsLimit);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.True(result.IsAnswerFound);
            Assert.True(result.Rounds <= roundsLimit);
            Assert.AreEqual(answer, result.Answer);
        }

        // [TestCase("11111", 8, 7)]
        // [TestCase("12346", 8, 7)]
        // [TestCase("34651", 8, 7)]
        // [TestCase("11111", 8, 7)]
        public void SolveGame_SuccesfullyIn7MovesOrLess_GivenDeluxeMastermind(string answer, int colors, int roundsLimit)
        {
            // Arrange
            var mastermindGame = _gameFactory.PrepareGame(answer, colors, roundsLimit);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.True(result.IsAnswerFound);
            Assert.True(result.Rounds <= roundsLimit);
            Assert.AreEqual(answer, result.Answer);
        }

        // [TestCase("AAAAA", 8, 6)]
        // [TestCase("ABCDF", 8, 6)]
        // [TestCase("CDFEA", 8, 6)]
        // [TestCase("FFFFF", 8, 6)]
        public void SolveGame_SuccesfullyIn6MovesOrLess_GivenDeluxeMastermind(string answer, int colors, int roundsLimit)
        {
            // Arrange
            var mastermindGame = _gameFactory.PrepareGame(answer, colors, roundsLimit);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.True(result.IsAnswerFound);
            Assert.True(result.Rounds <= roundsLimit);
            Assert.AreEqual(answer, result.Answer);
        }
    }
}
