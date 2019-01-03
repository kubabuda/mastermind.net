using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace Mastermind.Tests.Services
{
    public class MastermindGameServiceTests
    {
        const string correctAnswer = "ABBAC";
        ICheckAnswersService checkAnswersService;
        IGameSettings settings;
        IMastermindGame _serviceUnderTest;

        readonly string answerToCheck = "fooBar";
        IAnswerCheckDto answerCheck = Substitute.For<IAnswerCheckDto>();

        [SetUp]
        public void Setup()
        {
            checkAnswersService = Substitute.For<ICheckAnswersService>();
            settings = Substitute.For<IGameSettings>();
            settings.Colors.Returns(6);
            settings.Digits.Returns(correctAnswer.Length);
            _serviceUnderTest = new MastermindGameService(correctAnswer, checkAnswersService, settings);
        }

        [Test]
        public void AnswerLength_ShouldBeCorrectAnswerLength_OnStart()
        {
            Assert.AreEqual(correctAnswer.Length, _serviceUnderTest.Settings.Digits);
        }

        [Test]
        public void Rounds_ShouldBe0_OnStart()
        {
            Assert.AreEqual(0, _serviceUnderTest.Rounds);
        }


        [Test]
        public void Rounds_ShouldBe1_AfterOneRound()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerCheck);

            // Act
            _serviceUnderTest.PlayRound(answerToCheck);

            // Assert
            Assert.AreEqual(1, _serviceUnderTest.Rounds);
        }


        [Test]
        public void Rounds_ShouldBe2_After2Rounds()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerCheck);

            // Act
            _serviceUnderTest.PlayRound(answerToCheck);
            _serviceUnderTest.PlayRound(answerToCheck);

            // Assert
            Assert.AreEqual(2, _serviceUnderTest.Rounds);
        }

        [Test]
        public void IsFinished_ShouldBeFalse_OnStartup()
        {
            Assert.IsFalse(_serviceUnderTest.LastCheck.IsCorrect);
        }

        [Test]
        public void IsFinished_ShouldBeFalse_AfterRoundWithWrongAnswer()
        {
            answerCheck.WhitePoints.Returns(correctAnswer.Length - 1);
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerCheck);

            // Act
            var result = _serviceUnderTest.PlayRound(answerToCheck);

            // Assert
            Assert.IsFalse(_serviceUnderTest.LastCheck.IsCorrect);
        }

        [Test]
        public void IsFinished_ShouldBeTrue_AfterRoundWithRightAnswer()
        {
            // Arrange
            answerCheck.IsCorrect.Returns(true);
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerCheck);

            // Act
            var result = _serviceUnderTest.PlayRound(answerToCheck);

            // Assert
            Assert.IsTrue(_serviceUnderTest.LastCheck.IsCorrect);
        }

        [Test]
        public void PlayRound_ShouldReturnStatsFromService_GivenAnswerToCheck()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerCheck);

            // Act
            var result = _serviceUnderTest.PlayRound(answerToCheck);

            // Assert
            Assert.AreEqual(answerCheck, result);
        }

        [Test]
        public void PlayRound_ShouldCacheAnswer_GivenAnswerToCheck()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerCheck);

            // Act
            var result = _serviceUnderTest.PlayRound(answerToCheck);

            // Assert
            Assert.AreEqual(answerToCheck, _serviceUnderTest.Answers.First());
            Assert.AreEqual(answerCheck, _serviceUnderTest.AnswerChecks[answerToCheck]);
        }

        [Test]
        public void InitialCheckState_ShouldReturnEmptyNotCorrect()
        {
            // Arrange
            
            // Act
            IAnswerCheckDto result = _serviceUnderTest.LastCheck;

            // Assert
            Assert.AreEqual(false, result.IsCorrect);
            Assert.AreEqual(0, result.BlackPoints);
            Assert.AreEqual(0, result.WhitePoints);
        }
    }
}
