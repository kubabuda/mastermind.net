using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mastermind.Tests.Services.Solvers
{
    public class EduinfSolverServiceIntegrationTests
    {
        EduinfSolverService _serviceUnderTests;
        IGameFactory _gameFactory;

        [SetUp]
        public void Setup()
        {
            _gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            _serviceUnderTests = new EduinfSolverService(generator);
        }

        [TestCase("1234", 6, 6)]
        [TestCase("6666", 6, 6)]
        [TestCase("1234", 6, 6)]
        public void SolveGame_SuccesfullyAt6MovesOrLess_GivenClassicMastermind(string answer, int colors, int roundsLimit)
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

        [TestCase("1122", 6, 1)]
        [TestCase("1234", 6, 5)]
        [TestCase("6666", 6, 5)]
        [TestCase("3456", 6, 7)]
        [TestCase("2621", 6, 7)]
        public void SolveGame_SuccesfullyAt5MovesOrLess_GivenClassicMastermind(string answer, int colors, int roundsLimit)
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

        //[Test] takes a while
        public void SolveGame_SuccesfullyAt5MovesOrLess_GivenAllCodesForClassicMastermind()
        {
            // Arrange
            var generator = new GenerateKeyRangesService();
            var gameFactory = new GameFactory();
            var colors = 6;
            var digits = 4;
            var roundLimit = 6;
            var settings = new GameSettings(colors, digits, roundLimit);
            var keys = generator.GenerateCodes(settings);
            var serviceUnderTests = new EduinfSolverService(generator);

            var failedAnswersCases = new Dictionary<string, int>();

            foreach (var answer in keys)
            {
                var mastermindGame = gameFactory.PrepareGame(answer, settings);

                // Act
                var result = serviceUnderTests.SolveGame(mastermindGame);

                if (result.Rounds <= settings.RoundLimit || answer != result.Answer)
                {
                    failedAnswersCases[result.Answer] = result.Rounds;
                }
            }

            // Assert
            Assert.IsEmpty(failedAnswersCases);
        }
        


        [TestCase("12346", 8, 8)]
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

        [TestCase("11111", 8, 7)]
        [TestCase("12345", 8, 7)]
        [TestCase("34651", 8, 7)]
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

        [TestCase("11111", 8, 6)]
        [TestCase("12346", 8, 6)]
        [TestCase("34651", 8, 6)]
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
