using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mastermind.Tests.Services.Solvers
{
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


        [TestCase("12", 2, 1)]
        [TestCase("1122", 6, 1)]
        [TestCase("2233", 6, 5)]
        [TestCase("1234", 6, 5)]
        [TestCase("3456", 6, 5)]
        [TestCase("6666", 6, 5)]
        public void SolveGame_SuccesfullyAt5MovesOrLess_GivenClassicMastermind(string answer, int colors, int roundsLimit)
        {
            // Arrange
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

        [TestCase("6111", 6, 5)]
        [TestCase("5115", 6, 5)]
        [TestCase("6521", 6, 5)]
        [TestCase("5621", 6, 5)]
        public void SolveGame_At5MovesOrLess_GivenEdgeCase(string answer, int colors, int roundsLimit)
        {
            // Arrange
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

        // [Test] // attention, this takes half hour
        public void SolveGame_SuccesfullyAt5MovesOrLess_GivenAllCodesForClassicMastermind()
        {
            // Arrange
            var gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            var colors = 6;
            var digits = 4;
            var roundsLimit = 5;
            var settings = new GameSettings(colors, digits, roundsLimit);
            var keys = generator.GenerateCodes(settings);
            var _serviceUnderTests = new KnuthSolverService(generator);

            var failedAnswersCases = new Dictionary<string, int>();

            foreach (var answer in keys)
            {
                var mastermindGame = gameFactory.PrepareGame(answer, settings);
                // System.Console.Write($"\r{answer}:");
                // Act
                var result = _serviceUnderTests.SolveGame(mastermindGame);
                
                if (result.Rounds <= settings.RoundLimit || answer != result.Answer)
                {
                    // System.Console.WriteLine($" FAIL, found instead {result.Answer} in {result.Rounds}");
                    failedAnswersCases[result.Answer] = result.Rounds;
                }
            }

            // Assert
            Assert.IsEmpty(failedAnswersCases);
        }


        // [TestCase("ABCDF", 8, 8)]
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

        // [TestCase("AAAAA", 8, 7)]
        // [TestCase("ABCDF", 8, 7)]
        // [TestCase("CDFEA", 8, 7)]
        // [TestCase("FFFFF", 8, 7)]
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
