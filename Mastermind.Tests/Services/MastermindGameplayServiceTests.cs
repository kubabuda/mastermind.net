using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Tests.Services
{
    public class MastermindGameplayServiceTests
    {
        string answerToGuess;
        IMastermindGame mastermindGame;
        IInterfaceService gameInterface;
        MastermindGameplayService _serviceUnderTests;

        string wrongAnswer;
        readonly IAnswerCheckDto correcrAnswerCheck = Substitute.For<IAnswerCheckDto>();
        readonly IAnswerCheckDto wrongAnswerCheck = Substitute.For<IAnswerCheckDto>();

        [SetUp]
        public void Setup()
        {
            answerToGuess = "ABC";
            mastermindGame = Substitute.For<IMastermindGame>();
            gameInterface = Substitute.For<IInterfaceService>();
            _serviceUnderTests = new MastermindGameplayService(answerToGuess, mastermindGame, gameInterface);

            wrongAnswer = "ABD";
            correcrAnswerCheck.IsCorrect.Returns(true);
            correcrAnswerCheck.IsCorrect.Returns(false);
        }

        [Test]
        public void Game_ShouldPlayOneRound_WhenAnswerFoundInFirstRound()
        {
            mastermindGame.PlayRound(wrongAnswer);
        }
    }
}
