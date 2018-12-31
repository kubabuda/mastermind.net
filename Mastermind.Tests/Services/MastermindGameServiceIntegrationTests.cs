using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mastermind.Tests.Services
{
    public class MastermindGameServiceIntegrationTests
    {
        readonly string correctAnswer = "ABCD";
        ICheckAnswersService checkAnswersService;
        IMastermindGamePlay serviceUnderTest;
        List<string> answers;


        [SetUp]
        public void Setup()
        {
            checkAnswersService = new AnswerCheckService();
            serviceUnderTest = new MastermindGameService(correctAnswer, checkAnswersService);
            answers = new List<string> { "AACC", "AADD", "ABDD", "ABDC", correctAnswer };
        }


        [Test]
        public void PlayIntegrationTest_ShouldBeFinished_WhenCorrectAnswerIsGuessed()
        {
            // Arrange
            
            // Act
            foreach (var answer in answers)
            {
                if (!serviceUnderTest.IsFinished)
                {
                    serviceUnderTest.PlayRound(answer);
                }
            }

            // Assert
            Assert.True(serviceUnderTest.IsFinished);
        }

        [Test]
        public void PlayIntegrationTest_ShouldDisplay5Rounds_After5Rounds()
        {
            // Arrange

            // Act
            foreach (var answer in answers)
            {
                if (!serviceUnderTest.IsFinished)
                {
                    serviceUnderTest.PlayRound(answer);
                }
            }

            // Assert
            Assert.AreEqual(5, serviceUnderTest.Rounds);
        }

        [Test]
        public void PlayIntegrationTest_ShouldFinishAfter5Rounds_WhenCorrectAnswerIsGuessedIn5Round()
        {
            // Arrange
            int i = 0;

            // Act
            while (!serviceUnderTest.IsFinished)
            {
                serviceUnderTest.PlayRound(answers[i]);
                ++i;
            }

            // Assert
            Assert.AreEqual(5, serviceUnderTest.Rounds);
        }
    }
}
