using Mastermind.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Tests.Services
{
    public class AnswerCheckServiceTests
    {
        private AnswerCheckService serviceUnderTests = new AnswerCheckService();

        [TestCase("AAAA", "AAAA", 4, 0)]
        [TestCase("AAAA", "AAAB", 3, 0)]
        public void Somemethod(string answer, string correctAnswer, int white, int black)
        {
            var result = serviceUnderTests.Somemethod(correctAnswer, answer);

            Assert.AreEqual(result.White, white);
            Assert.AreEqual(result.Black, black);
        }
    }
}
