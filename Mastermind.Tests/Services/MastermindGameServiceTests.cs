using Mastermind.Models;
using Mastermind.Services;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace Mastermind.Tests.Services
{
    public class MastermindGameServiceTests
    {
        const string correctAnswer = "ABBAC";
        ICheckAnswersService checkAnswersService;
        MastermindGameService _serviceUnderTest;

        string wrongAnswerToCheck = "fooBar";
        IAnswerCheckDto wrongAnswerCheck = Substitute.For<IAnswerCheckDto>();

        [SetUp]
        public void Setup()
        {
            checkAnswersService = Substitute.For<ICheckAnswersService>();
            _serviceUnderTest = new MastermindGameService(correctAnswer, checkAnswersService);
        }

        [Test]
        public void AnswersCount_ShouldBe0_OnStart()
        {
            Assert.AreEqual(0, _serviceUnderTest.Answers.Count());
        }

        [Test]
        public void AnswerLength_ShouldBeCorrectAnswerLength_OnStart()
        {
            Assert.AreEqual(correctAnswer.Length, _serviceUnderTest.AnswerLength);
        }

        [Test]
        public void Round_ShouldReturnStatsFromService_GivenAnswerToCheck()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, wrongAnswerToCheck).Returns(wrongAnswerCheck);

            // Act
            var result = _serviceUnderTest.Round(wrongAnswerToCheck);

            // Assert
            Assert.AreEqual(wrongAnswerCheck, result);
        }

        [Test]
        public void Round_ShouldCacheAnswer_GivenAnswerToCheck()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, wrongAnswerToCheck).Returns(wrongAnswerCheck);

            // Act
            var result = _serviceUnderTest.Round(wrongAnswerToCheck);

            // Assert
            Assert.AreEqual(wrongAnswerToCheck, _serviceUnderTest.Answers.First());
        }

        [Test]
        public void Round_ShouldRaiseAnswerCount_GivenAnswerToCheck()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, wrongAnswerToCheck).Returns(wrongAnswerCheck);

            // Act
            _serviceUnderTest.Round(wrongAnswerToCheck);

            // Assert
            Assert.AreEqual(1, _serviceUnderTest.Answers.Count());
        }

        [Test]
        public void Round_ShouldCacheAnswerCheck_GivenAnswerToCheck()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, wrongAnswerToCheck).Returns(wrongAnswerCheck);

            // Act
            _serviceUnderTest.Round(wrongAnswerToCheck);

            // Assert
            Assert.AreEqual(wrongAnswerCheck, _serviceUnderTest.AnswerChecks[wrongAnswerToCheck]);
        }


        [Test]
        public void Round_ShouldRaiseAnswerCountTo2_GivenAnswerToCheckTwice()
        {
            // Arrange
            checkAnswersService.CheckAnswer(correctAnswer, wrongAnswerToCheck).Returns(wrongAnswerCheck);

            // Act
            _serviceUnderTest.Round(wrongAnswerToCheck);
            _serviceUnderTest.Round(wrongAnswerToCheck);

            // Assert
            Assert.AreEqual(2, _serviceUnderTest.Answers.Count());
        }

        
    }
}
