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

        [TestCase("ABCD", 6, 2)]
        [TestCase("FFFF", 6, 5)]
        [TestCase("ABCD", 6, 5)]
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

        [TestCase("AB", 2, 1)]
        [TestCase("AABB", 6, 1)]
        // [TestCase("ABCD", 6, 2)]
        // [TestCase("FFFF", 6, 5)]
        [TestCase("CDEF", 6, 5)] // TODO should do in just 5!
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

        //[Test]
        public void SolveGame_SuccesfullyAt5MovesOrLess_GivenAllCodesForClassicMastermind()
        {
            // Arrange
            var generator = new GenerateKeyRangesService();
            var settings = Substitute.For<IGameSettings>();
            settings.Colors.Returns(6);
            settings.Digits.Returns(4);
            settings.RoundLimit.Returns(6);
            var keys = generator.GenerateCodes(settings);

            var failedAnswersCases = new Dictionary<string, int>();

            foreach (var answer in keys)
            {
                var mastermindGame = _gameFactory.PrepareGame(answer, settings);

                // Act
                var result = _serviceUnderTests.SolveGame(mastermindGame);

                if (result.Rounds <= settings.RoundLimit || answer != result.Answer)
                {
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
