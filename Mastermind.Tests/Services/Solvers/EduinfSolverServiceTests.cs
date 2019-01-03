using Mastermind.Models;
using Mastermind.Services;
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
    }

    public class EduinfSolverServiceIntegrationTests
    {
        EduinfSolverService _serviceUnderTests;
        IGameFactory _gameFactory;

        [SetUp]
        public void Setup()
        {
            _gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            _serviceUnderTests = new EduinfSolverService(generator);
        }

        [TestCase("ABCD", 4)]
        [TestCase("ABCD", 6)]
        [TestCase("ABCDF", 6)]
        [TestCase("FFFFF", 6)]
        public void SolveGame_SuccesfullyAt256MovesOrLess_GivenGameWith256(string answer, int colors, int roundsLimit = 10)
        {
            // Arrange
            var mastermindGame = _gameFactory.PrepareGame(answer, colors, roundsLimit);

            // Act
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            // Assert
            Assert.True(result.IsAnswerFound);
            Assert.True(result.Rounds <= roundsLimit);
        }
    }
}
