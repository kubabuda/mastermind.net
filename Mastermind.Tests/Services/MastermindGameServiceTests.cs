using Mastermind.Models;
using Mastermind.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind.Tests.Services
{
    public class MastermindGameServiceTests
    {
        const string correctAnswer = "ABBAC";
        ICheckAnswersService checkAnswersService;
        MastermindGameService _serviceUnderTest;

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
            var answerToCheck = "fooBar";
            var answerStats = new AnswerCheckDto(0, 1);
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerStats);

            // Act
            var result = _serviceUnderTest.Round(answerToCheck);

            // Assert
            Assert.AreEqual(answerStats, result);
        }

        [Test]
        public void Round_ShouldCacheAnswer_GivenAnswerToCheck()
        {
            // Arrange
            var answerToCheck = "fooBar";
            var answerStats = new AnswerCheckDto(0, 1);
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerStats);

            // Act
            var result = _serviceUnderTest.Round(answerToCheck);

            // Assert
            Assert.AreEqual(answerToCheck, _serviceUnderTest.Answers.First());
        }

        [Test]
        public void Round_ShouldRaiseAnswerCount_GivenAnswerToCheck()
        {
            // Arrange
            var answerToCheck = "fooBar";
            var answerStats = new AnswerCheckDto(0, 1);
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerStats);

            // Act
            _serviceUnderTest.Round(answerToCheck);

            // Assert
            Assert.AreEqual(1, _serviceUnderTest.Answers.Count());
        }

        [Test]
        public void Round_ShouldRaiseAnswerCountTo2_GivenAnswerToCheckTwice()
        {
            // Arrange
            var answerToCheck = "fooBar";
            var answerStats = new AnswerCheckDto(0, 1);
            checkAnswersService.CheckAnswer(correctAnswer, answerToCheck).Returns(answerStats);

            // Act
            _serviceUnderTest.Round(answerToCheck);
            _serviceUnderTest.Round(answerToCheck);

            // Assert
            Assert.AreEqual(2, _serviceUnderTest.Answers.Count());
        }
    }
}
