using Mastermind.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Tests.Services
{
    public class AnswerCheckServiceTests
    {
        private AnswerCheckService _serviceUnderTests = new AnswerCheckService();


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
        public void CheckAnswer_returnsCountOfCorrectValuesOnCorrectPosition_GivenAnswerAndExpectedAnswer(string answer, string correctAnswer, int white)
        {
            var result = _serviceUnderTests.CheckAnswer(correctAnswer, answer);

            Assert.AreEqual(result.White, white);
        }


    }
}
