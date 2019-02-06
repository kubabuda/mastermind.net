using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            // var answer = "1234";
            // PlayWithHumanCodeBreaker(answer);
            
            TestKnuthOnRange();
            // TestEduInfOnRange();
        }

        private static void PlayWithHumanCodeBreaker(string answer)
        {
            var colors = 6;
            var roundsLimit = 6;

            var gameFactory = new GameFactory();
            var gameplay = gameFactory.PrepareDefaultGameplay();
            var game = gameFactory.PrepareGame(answer, colors, roundsLimit);
            gameplay.SolveGame(game);

            Console.ReadLine();
        }

        public static void TestKnuthOnRange(){
            var generator = new GenerateKeyRangesService();
            var serviceUnderTests = new KnuthSolverService(generator);

            TestOnRange(serviceUnderTests, "Knuth");
        }

        public static void TestEduInfOnRange(){
            var generator = new GenerateKeyRangesService();
            var serviceUnderTests = new EduinfSolverService(generator);

            TestOnRange(serviceUnderTests, "Swaszek");
        }

        public static  void TestOnRange(ISolveMastermindService serviceUnderTests, string algoLabel)
        {
            // Arrange
            var colors = 8;
            var digits = 5;
            var roundLimit = 17;
            var settings = new GameSettings(colors, digits, roundLimit);

            var generator = new GenerateKeyRangesService();
            var keys = generator.GenerateCodes(settings).Take(10);
            var gameFactory = new GameFactory();

            var fails = new Dictionary<string, int>();
            Stopwatch stopwatch = new Stopwatch();
            long allRoundsCount = 0;
            var maxRounds = 0;
            var maxExample = "";
            TimeSpan elapsedTotal = TimeSpan.FromMilliseconds(0);
            TimeSpan longestElapsed = TimeSpan.FromMilliseconds(0);
            string longestExecutionExample = keys.First();
            foreach (var answer in keys)
            {
                var mastermindGame = gameFactory.PrepareGame(answer, settings);

                Console.Write($"\r{answer}:");
                stopwatch.Start();
            // Act
                var result = serviceUnderTests.SolveGame(mastermindGame);

            // Assert
                stopwatch.Stop();
                allRoundsCount += result.Rounds;
                elapsedTotal.Add(stopwatch.Elapsed);
                if (answer != result.Answer || result.Rounds > settings.RoundLimit)
                {
                    Console.WriteLine($"Got {result.Answer} in {result.Rounds} rounds");
                    fails[answer] = result.Rounds;
                }
                else {
                    if (result.Rounds > maxRounds) {
                        maxRounds = result.Rounds;
                        maxExample = answer;
                    }
                    if (stopwatch.Elapsed > longestElapsed) {
                        longestElapsed = stopwatch.Elapsed;
                        longestExecutionExample = answer;
                    }
                }
            }

            Console.WriteLine($"\rFor {algoLabel} algo on Mastermind({digits}, {colors}):");
            if (fails.Keys.Count() > 0)
            { 
                Console.WriteLine($"Failed to find solution in {fails.Count()} ");
                var worstRoundCount = fails.Values.Max();
                var worstCases = fails.Where((k, v) => v == worstRoundCount).Select((k, v) => k);
                string worstCaseExample = worstCases.First().Key;
                Console.WriteLine($"{worstCases.Count()} pessimistic cases - in {worstRoundCount} rounds, example: {worstCaseExample} ");
            }
            double mean = (double)allRoundsCount / keys.Count();
            Console.WriteLine($"\rMean rounds per solution is {mean}");
            Console.WriteLine($"Pessimistic case is {maxExample} with {maxRounds} rounds");
            double meanExecMs = (double)elapsedTotal.TotalMilliseconds / keys.Count();
            Console.WriteLine($"\rMean execution time is {meanExecMs}");
            Console.WriteLine($"Pessimistic case is {longestExecutionExample} with {longestElapsed.TotalMilliseconds} ms");
        }
    }
}
