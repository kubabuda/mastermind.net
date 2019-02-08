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
        const int colors = 8;
        const int digits = 5;
        const int roundLimit = 12;
        const int rangeLimit = 1000;

        static void Main(string[] args)
        {
            // var answer = "1234";
            // PlayWithHumanCodeBreaker(answer);
            
            TestKnuthOnRange();
            // TestSwaszekOnRange();
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

            TestOnRange(serviceUnderTests, "EduInf");
        }

        public static void TestSwaszekOnRange(){
            var generator = new GenerateKeyRangesService();
            var serviceUnderTests = new SwaszekSolverService(generator);

            TestOnRange(serviceUnderTests, "Swaszek");
        }

        public static  void TestOnRange(ISolveMastermindService serviceUnderTests, string algoLabel)
        {
            // Arrange
            var settings = new GameSettings(colors, digits, roundLimit);
            var generator = new GenerateKeyRangesService();
            var gameFactory = new GameFactory();
            
            // var keys = new [] { "55321", "85821", "55321" };
            var keys = generator.GenerateCodes(settings);
            if(rangeLimit > 0) {
                keys = keys.Take(rangeLimit);
            }
            
            var fails = new Dictionary<string, int>();
            long allRoundsCount = 0;
            var maxRounds = 0;
            var maxExample = "";
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan elapsedTotal = TimeSpan.FromMilliseconds(0);
            TimeSpan longestElapsed = TimeSpan.FromMilliseconds(0);
            string longestExecutionExample = keys.First();
            int i = 0;

            Console.WriteLine($"\rFor {algoLabel} algo on Mastermind({digits}, {colors}):");
            foreach (var answer in keys)
            {
                ++i;
                var mastermindGame = gameFactory.PrepareGame(answer, settings);

                Console.Write($"\r[{i}/{keys.Count()}] '{answer}':");
                stopwatch.Start();
            // Act
                var result = serviceUnderTests.SolveGame(mastermindGame);

            // Assert
                stopwatch.Stop();
                allRoundsCount += result.Rounds;
                elapsedTotal = elapsedTotal.Add(stopwatch.Elapsed);
                if (answer != result.Answer || result.Rounds > settings.RoundLimit)
                {
                    Console.WriteLine($"Got {result.Answer} instead in {result.Rounds} rounds");
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
                stopwatch.Reset();
            }
            // display results
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
            Console.WriteLine($"Example with most rounds - {maxRounds} - is {maxExample}");
            double meanExecMs = (double)elapsedTotal.TotalMilliseconds / keys.Count();
            Console.WriteLine($"\rMean execution time is {meanExecMs} ms");
            Console.WriteLine($"Longest execution time {longestElapsed.TotalMilliseconds} ms found for {longestExecutionExample}");
        }
    }
}
