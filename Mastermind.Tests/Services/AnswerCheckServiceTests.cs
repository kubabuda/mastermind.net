using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;

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
            Assert.Throws<ArgumentException>(() => _serviceUnderTests.CheckAnswer("AA", "AAA"));
        }

        [TestCase("AAAA", "AAAA", 4)]
        [TestCase("AA", "AA", 2)]
        [TestCase("A", "A", 1)]
        [TestCase("AAAA", "AAAB", 3)]
        [TestCase("AAAA", "AABC", 2)]
        public void CheckAnswer_returnsCountOfCorrectValuesOnCorrectPosition_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int whitePoints)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(whitePoints, result.WhitePoints);
        }

        [TestCase("CB", "BC", 2)]
        [TestCase("CAB", "ABC", 3)]
        public void CheckAnswer_returnsCountOfCorrectValuesOnWrongPosition_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int blackPoints)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(blackPoints, result.BlackPoints);
        }

        [TestCase("ACAB", "AABC", 1, 3)]
        [TestCase("AABB", "CCDD", 0, 0)]
        public void CheckAnswer_returnsProperWhiteAndBlackPoints_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int whitePoints, int blackPoints)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(whitePoints, result.WhitePoints);
            Assert.AreEqual(blackPoints, result.BlackPoints);
        }

        [TestCase("aAa", 3, 0, true)]
        [TestCase("aAa", 2, 1, false)]
        [TestCase("aAa", 2, 0, false)]
        [TestCase("aAa", 0, 1, false)]
        [TestCase("aAa", 0, 0, false)]        
        public void BuildAnswerCheck_ReturnsAnswerCheck_GivenCorrectAnswerWhiteAndBlackPoints(string correctAnswer, int whitePts, int blackPts, bool isValid)
        {
            var result = _serviceUnderTests.BuildAnswerCheck(correctAnswer, whitePts, blackPts);
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
            Assert.Throws<ArgumentException>(() => _serviceUnderTests.BuildAnswerCheck(correctAnswer, whitePts, blackPts));
        }

        [TestCase("AAA", 4, 3, true)]
        [TestCase("AA", 4, 3, false)]
        [TestCase("AAAA", 4, 3, false)]
        [TestCase("aAa", 4, 3, false)]
        [TestCase("AAD", 4, 3, true)]
        [TestCase("AAE", 4, 3, false)]
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
    }
}
