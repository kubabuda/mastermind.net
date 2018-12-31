using Mastermind.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Tests.Services
{
    public class MastermindGameServiceTests
    {
        const string correctAnswer = "ABBAC";
        MastermindGameService _serviceUnderTest;

        [SetUp]
        public void Setup()
        {
            _serviceUnderTest = new MastermindGameService(correctAnswer);
        }

        [Test]
        public void AnswersCount_ShouldBe0_OnStart()
        {
            Assert.AreEqual(0, _serviceUnderTest.AnswersCount);
        }

        [Test]
        public void AnswerLength_ShouldBeCorrectAnswerLength_OnStart()
        {
            Assert.AreEqual(correctAnswer.Length, _serviceUnderTest.AnswerLength);
        }
    }
}
