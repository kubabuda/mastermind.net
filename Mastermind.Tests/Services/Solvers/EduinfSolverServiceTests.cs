using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class EduinfSolverServiceTests
    {
        IGenerateKeyRangesService _keyRangesGenerator;
        EduinfSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = Substitute.For<IGenerateKeyRangesService>();
            _serviceUnderTests = new EduinfSolverService(_keyRangesGenerator);
        }

        [TestCase(4, "AABB")]
        [TestCase(5, "AAABB")]
        public void GetInitialKeyGuess_ShouldReturnExpectedKeyGuess_GivenKeyLength(int length, string expectedKey)
        {
            // Act
            var result = _serviceUnderTests.GetInitialKeyGuess(length);

            // Assert
            Assert.AreEqual(expectedKey, result);
        }

        [TestCase(4, "AABB")]
        [TestCase(5, "AAABB")]
        public void GetKeyGuess_ShouldReturnInitialKeyGuess_GivenDto(int length, string expectedKey)
        {
            // Arrange
            var dto = Substitute.For<ISolvingRoundStateDto>();
            dto.Round.Returns(0);
            var settings = Substitute.For<IGameSettings>();
            dto.Settings.Returns(settings);
            settings.Digits.Returns(length);

            // Act
            var result = _serviceUnderTests.GetKeyGuess(dto);

            // Assert
            Assert.AreEqual(expectedKey, result);
        }

        [TestCase(1, 2, 1, 2, true)]
        [TestCase(1, 1, 1, 2, false)]
        [TestCase(1, 2, 0, 2, false)]
        [TestCase(1, 1, 3, 4, false)]
        public void IsCheckDifferent_ShouldReturnExpectedValue_GivenTwoChecks(int white1, int black1, int white2, int black2, bool expectedResult)
        {
            // Arrange
            var check1 = Substitute.For<IAnswerCheckDto>();
            check1.WhitePoints.Returns(white1);
            check1.BlackPoints.Returns(black1);
            var check2 = Substitute.For<IAnswerCheckDto>();
            check2.WhitePoints.Returns(white2);
            check2.BlackPoints.Returns(black2);

            // Act
            var result = _serviceUnderTests.IsCheckDifferent(check1, check2);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("AAAA", "AAAA", 4, 0, false)]
        [TestCase("ABCD", "ABCE", 3, 0, false)]
        [TestCase("EEEE", "AAAA", 3, 0, true)]
        public void IsKeyToBeRemoved_ReturnsExpectedValue_GivenTwoKeysAndPreviousCheck(string key, string usedKey, int whitePts, int blackPts, bool expectedResult)
        {
            // Arrange
            var check = Substitute.For<IAnswerCheckDto>();
            check.WhitePoints.Returns(whitePts);
            check.BlackPoints.Returns(blackPts);
            
            // Act
            var result = _serviceUnderTests.IsKeyToBeRemoved(key, usedKey, check);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(true, 2, 2, true)]
        [TestCase(true, 1, 2, true)]
        [TestCase(false, 2, 2, true)]
        [TestCase(false, 1, 2, false)]
        public void IsGameFinished_True_WhenAnswerFoundOrRoundsEnded(bool answerFound, int round, int roundLimit, bool expectedResult)
        {
            // Arrange
            var dto = Substitute.For<ISolvingRoundStateDto>();
            dto.Round.Returns(round);
            var settings = Substitute.For<IGameSettings>();
            dto.Settings.Returns(settings);
            settings.RoundLimit.Returns(roundLimit);
            var answerCheck = Substitute.For<IAnswerCheckDto>();
            dto.LastCheck.Returns(answerCheck);
            answerCheck.IsCorrect.Returns(answerFound);

            // Act
            var result = _serviceUnderTests.IsGameFinished(dto);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
