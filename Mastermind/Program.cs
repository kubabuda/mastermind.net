using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Solvers;
using System;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var _gameFactory = new GameFactory();
            var generator = new GenerateKeyRangesService();
            var colors = 6;
            var digits = 4;
            var roundsLimit = 5;
            var settings = new GameSettings(colors, digits, roundsLimit);
            var keys = generator.GenerateCodes(settings);
            var _serviceUnderTests = new KnuthSolverService(generator);

            // var failedAnswersCases = new Dictionary<string, int>();

            foreach (var answer in keys)
            {
                var mastermindGame = _gameFactory.PrepareGame(answer, settings);
                System.Console.Write($"\r{answer}:");
                // Act
                var result = _serviceUnderTests.SolveGame(mastermindGame);
                
                if (result.Rounds > settings.RoundLimit || answer != result.Answer)
                {
                    System.Console.WriteLine($" FAIL, found instead {result.Answer} in {result.Rounds}");
                    // failedAnswersCases[result.Answer] = result.Rounds;
                }
            }


            // var answer = "ABCD";
            // var colors = 6;
            // var roundsLimit = 6;
            
            // var gameFactory = new GameFactory();
            // var gameplay = gameFactory.PrepareDefaultGameplay();
            // var game = gameFactory.PrepareGame(answer, colors, roundsLimit);
            // gameplay.SolveGame(game);

            // Console.ReadLine();
        }
    }
}
