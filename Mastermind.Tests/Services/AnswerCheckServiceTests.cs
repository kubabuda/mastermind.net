using Mastermind.Services;
using NUnit.Framework;
using System;

namespace Mastermind.Tests.Services
{
    public class AnswerCheckServiceTests
    {
        private ICheckAnswersService _serviceUnderTests = new AnswerCheckService();


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

        [TestCase("AAAA", "AAAA", true)]
        [TestCase("AAAA", "AAAB", false)]
        public void CheckAnswer_IsFinished_WhenWhitePtsEqLengthAndBlackPtsEqZero(string answer, string correctAnswer, bool isFinished)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(isFinished, result.IsFinished);
        }
    }
}
