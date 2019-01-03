using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class BruteforceSolverServiceTests
    {
        IGenerateKeyRangesService _keyRangesGenerator;
        BruteforceSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = Substitute.For<IGenerateKeyRangesService>();
            _serviceUnderTests = new BruteforceSolverService(_keyRangesGenerator);
        }
    }

    public class BruteforceSolverServiceIntegrationTests
    {
        string correctAnswer = "ABCD";
        IGameSettings _gameSettings;
        ICheckAnswersService _checkAnswers;
        IMastermindGame mastermindGame;
        IGenerateKeyRangesService _keyRangesGenerator;
        BruteforceSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = new GenerateKeyRangesService();
            _gameSettings = new GameSettings(4, correctAnswer.Length);
            _serviceUnderTests = new BruteforceSolverService(_keyRangesGenerator);
            _checkAnswers = new AnswerCheckService();
            mastermindGame = new MastermindGameService(correctAnswer, _checkAnswers, _gameSettings);
        }

        [Test]
        public void SolveGame_()
        {
            var result = _serviceUnderTests.SolveGame(mastermindGame);

            Assert.True(result.IsAnswerFound);
            Assert.True(result.Rounds <= 256);
        }
    }
}
