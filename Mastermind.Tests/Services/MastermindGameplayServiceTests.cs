using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services
{
    public class MastermindGameplayServiceTests
    {
        string answerToGuess;
        IMastermindGame mastermindGame;
        IGameSettings settings;
        IInterfaceService gameInterface;
        MastermindGameplayService _serviceUnderTests;

        const string wrongAnswer = "ABD";
        readonly IAnswerCheckDto correctAnswerCheck = Substitute.For<IAnswerCheckDto>();
        readonly IAnswerCheckDto wrongAnswerCheck = Substitute.For<IAnswerCheckDto>();

        [SetUp]
        public void Setup()
        {
            answerToGuess = "ABC";
            settings = Substitute.For<IGameSettings>();
            mastermindGame = Substitute.For<IMastermindGame>();
            mastermindGame.Settings.Returns(settings);
            gameInterface = Substitute.For<IInterfaceService>();
            _serviceUnderTests = new MastermindGameplayService(gameInterface);

            wrongAnswerCheck.IsCorrect.Returns(false);
            correctAnswerCheck.IsCorrect.Returns(true);
            mastermindGame.PlayRound(wrongAnswer).Returns(wrongAnswerCheck);
            mastermindGame.PlayRound(answerToGuess).Returns(correctAnswerCheck);
        }

        [Test]
        public void Game_ShouldPlayOneRound_WhenAnswerFoundInFirstRound()
        {
            // Arrange 
            gameInterface.GetCurrentAnswer().Returns(answerToGuess);
            settings.RoundLimit.Returns(-1);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.AreEqual(true, result.IsAnswerFound);
            Assert.AreEqual(1, result.Rounds);
        }

        [Test]
        public void Game_ShouldPlayTwoRounds_WhenAnswerFoundInSecondRound()
        {
            // Arrange 
            gameInterface.GetCurrentAnswer().Returns(wrongAnswer, answerToGuess);
            settings.RoundLimit.Returns(-1);
            
            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.AreEqual(true, result.IsAnswerFound);
            Assert.AreEqual(2, result.Rounds);
        }

        [Test]
        public void Game_ShouldPlayTwoRounds_WhenAnswerFoundInSecondRound_GivenTwoRounds()
        {
            // Arrange 
            gameInterface.GetCurrentAnswer().Returns(wrongAnswer, answerToGuess);
            var rounds = 2;
            settings.RoundLimit.Returns(rounds);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.AreEqual(true, result.IsAnswerFound);
            Assert.AreEqual(rounds, result.Rounds);
        }

        [Test]
        public void Game_ShouldPlayTwoRounds_WhenAnswerNotFound_GivenTwoRounds()
        {
            // Arrange 
            gameInterface.GetCurrentAnswer().Returns(wrongAnswer, wrongAnswer);
            var rounds = 2;
            settings.RoundLimit.Returns(rounds);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.AreEqual(false, result.IsAnswerFound);
            Assert.AreEqual(rounds, result.Rounds);
        }
    }
}
