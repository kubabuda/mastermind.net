using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace Mastermind.Tests.Services
{
    public class AnswerCheckServiceTests
    {
        private ICheckAnswersService _serviceUnderTests = new AnswerCheckService();

        private IGameSettings gameSettings;

        [SetUp]
        public void Setup()
        {
            gameSettings = Substitute.For<IGameSettings>();
        }

        [Test]
        public void CheckAnswer_ThrowsException_WhenAnswerAndCorrectAnswerLengthsDiffer()
        {
            Assert.Throws<ArgumentException>(() => _serviceUnderTests.CheckAnswer("11", "111"));
        }

        [TestCase("1111", "1111", 4)]
        [TestCase("11", "11", 2)]
        [TestCase("1", "1", 1)]
        [TestCase("1111", "1112", 3)]
        [TestCase("1111", "1123", 2)]
        public void CheckAnswer_returnsCountOfCorrectValuesOnCorrectPosition_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int whitePoints)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(whitePoints, result.WhitePoints);
        }

        [TestCase("32", "23", 2)]
        [TestCase("312", "123", 3)]
        public void CheckAnswer_returnsCountOfCorrectValuesOnWrongPosition_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int blackPoints)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(blackPoints, result.BlackPoints);
        }

        [TestCase("1312", "1123", 1, 3)]
        [TestCase("1122", "3344", 0, 0)]
        public void CheckAnswer_returnsProperWhiteAndBlackPoints_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int whitePoints, int blackPoints)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(whitePoints, result.WhitePoints);
            Assert.AreEqual(blackPoints, result.BlackPoints);
        }

        [TestCase("1312", "1123")]
        [TestCase("1122", "1144")]
        public void CheckAnswer_ShouldReturnTheSame_GivenReversedParameters(string answer1, string answer2)
        {
            var result1 = _serviceUnderTests.CheckAnswer(answer1, answer2);
            var result2 = _serviceUnderTests.CheckAnswer(answer2, answer1);

            Assert.AreEqual(result1.WhitePoints, result2.WhitePoints);
            Assert.AreEqual(result1.BlackPoints, result2.BlackPoints);
        }

        [TestCase("aAa", 3, 0, true)]
        [TestCase("aAa", 2, 1, false)]
        [TestCase("aAa", 2, 0, false)]
        [TestCase("aAa", 0, 1, false)]
        [TestCase("aAa", 0, 0, false)]        
        public void BuildAnswerCheck_ReturnsAnswerCheck_GivenCorrectAnswerWhiteAndBlackPoints(string correctAnswer, int whitePts, int blackPts, bool isValid)
        {
            var result = _serviceUnderTests.BuildAnswerCheck(correctAnswer.Length, whitePts, blackPts);
            Assert.AreEqual(whitePts, result.WhitePoints);
            Assert.AreEqual(blackPts, result.BlackPoints);
            Assert.AreEqual(isValid, result.IsCorrect);
        }

        [TestCase("aAa", 4, 0)]
        [TestCase("aAa", 4, 1)]
        [TestCase("aAa", 1, 4)]
        [TestCase("aAa", 2, 2)]
        [TestCase("aAa", -2, 0)]
        [TestCase("aAa", 0, -2)]
        [TestCase("", 0, 0)]
        public void BuildAnswerCheck_ThrowsArgumentException_GivenInvalidData(string correctAnswer, int whitePts, int blackPts)
        {
            Assert.Throws<ArgumentException>(() => _serviceUnderTests.BuildAnswerCheck(correctAnswer.Length, whitePts, blackPts));
        }

        [TestCase("3456", 6, 4, true)]
        [TestCase("111", 4, 3, true)]
        [TestCase("11", 4, 3, false)]
        [TestCase("1111", 4, 3, false)]
        // [TestCase("aAa", 4, 3, false)]
        [TestCase("114", 4, 3, true)]
        [TestCase("116", 4, 3, false)]
        [TestCase("6", 6, 1, true)]
        [TestCase("7", 6, 1, false)]
        [TestCase("", 4, 0, false)]
        [TestCase("", 0, 0, false)]
        [TestCase(null, 4, 0, false)]
        public void IsAnswerValid_ReturnsExpectedValue_GivenAnswerAndSettings(string answer, int colors, int digits, bool expectedResult)
        {
            // arrange
            gameSettings.Colors.Returns(colors);
            gameSettings.Digits.Returns(digits);

            // act
            var result = _serviceUnderTests.IsAnswerValid(answer, gameSettings);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CheckAnswer_returns1White3BlackPoints_Given1312and1123()
        {
            // Arrange
            var correctAnswer = new byte[] { 1, 3, 1, 2 };
            var answer = new byte[] { 1, 1, 2, 3 };

            // Act
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            // Assert
            Assert.AreEqual(1, result.WhitePoints);
            Assert.AreEqual(3, result.BlackPoints);
        }

        [Test]
        public void CheckAnswer_returns0White0BlackPoints_Given1122and3344()
        {
            // Arrange
            var correctAnswer = new byte[] { 1, 1, 2, 2 };
            var answer = new byte[] { 3, 3, 4, 4 };

            // Act
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            // Assert
            Assert.AreEqual(0, result.WhitePoints);
            Assert.AreEqual(0, result.BlackPoints);
        }

        [Test]
        public void CheckAnswer_ShouldReturnTheSame_Given1312and1123()
        {
            var answer1 = new byte[] { 1, 2, 1, 2 };
            var answer2 = new byte[] { 1, 1, 2, 3 };

            var result1 = _serviceUnderTests.CheckAnswer(answer1, answer2);
            var result2 = _serviceUnderTests.CheckAnswer(answer2, answer1);

            Assert.AreEqual(result1.WhitePoints, result2.WhitePoints);
            Assert.AreEqual(result1.BlackPoints, result2.BlackPoints);
        }

        [Test]
        public void CheckAnswer_ShouldReturnTheSame_Given1122and1144()
        {
            var answer1 = new byte[] { 1, 1, 2, 2 };
            var answer2 = new byte[] { 1, 1, 4, 4 };

            var result1 = _serviceUnderTests.CheckAnswer(answer1, answer2);
            var result2 = _serviceUnderTests.CheckAnswer(answer2, answer1);

            Assert.AreEqual(result1.WhitePoints, result2.WhitePoints);
            Assert.AreEqual(result1.BlackPoints, result2.BlackPoints);
        }
    }
}
