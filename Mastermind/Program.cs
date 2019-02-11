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
        const int roundLimit = 7;

        static void Main(string[] args)
        {
            // var answer = "1234";
            // PlayWithHumanCodeBreaker(answer);
            
            // TestKnuthOnRange();
            TestKnuthParallelOnRange();
            // TestSwaszekOnRange();
            // TestEduInfOnRange();
        }

        private static void PlayWithHumanCodeBreaker(string answer)
        {
            // var colors = 6;
            var roundsLimit = 6;

            var gameFactory = new GameFactory();
            var gameplay = gameFactory.PrepareDefaultGameplay();
            var game = gameFactory.PrepareGame(answer, colors, roundsLimit);
            gameplay.SolveGame(game);

            Console.ReadLine();
        }


        private static IEnumerable<string> GetKeysRange(IGameSettings settings)
        {
            var generator = new GenerateKeyRangesService();
            // var keys = new[] { "83721", "55321", "85821", "55321" };
            // var keys = new[] { "83111" };
            var keys = generator.GenerateCodes(settings);
            var rangeLimit = 1000;
            keys = keys.Take(rangeLimit);
            return keys;
        }

        public static void TestKnuthOnRange(){
            var generator = new GenerateKeyRangesService();
            var serviceUnderTests = new KnuthSolverService(generator);

            TestOnRange(serviceUnderTests, "Knuth");
        }

        public static void TestKnuthParallelOnRange(){
            var generator = new GenerateKeyRangesService();
            var serviceUnderTests = new KnuthSolverParallelService(generator);

            TestOnRange(serviceUnderTests, "KnuthParallel");
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
            var gameFactory = new GameFactory();
            var keys = GetKeysRange(settings);

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

                Console.Write($"\r[{i}/{keys.Count()}] '{answer}'");
                stopwatch.Start();
                // Act
                var result = serviceUnderTests.SolveGame(mastermindGame);

                // Assert
                stopwatch.Stop();
                allRoundsCount += result.Rounds;
                elapsedTotal = elapsedTotal.Add(stopwatch.Elapsed);
                if (answer != result.Answer || result.Rounds > settings.RoundLimit)
                {
                    Console.WriteLine($"[ERROR] Got {result.Answer} instead in {result.Rounds} rounds!");
                    fails[answer] = result.Rounds;
                }
                else
                {
                    var meanExexSoFarMs = (double)elapsedTotal.TotalMilliseconds / i;
                    var ETA = TimeSpan.FromMilliseconds(meanExexSoFarMs * (keys.Count() - i));
                    Console.Write($"| Mean rounds {((double)allRoundsCount / i):0.00} max {maxRounds} for {maxExample}, mean time {meanExexSoFarMs:0.00} ms, max {longestElapsed.TotalMilliseconds:0.00} ms for {longestExecutionExample} [ETA {ETA}]");
                    if (result.Rounds > maxRounds)
                    {
                        maxRounds = result.Rounds;
                        maxExample = answer;
                    }
                    if (stopwatch.Elapsed > longestElapsed)
                    {
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
            Console.WriteLine($"\rMean rounds per solution is {mean}{new string(' ', 90)}");
            Console.WriteLine($"Example with most rounds - {maxRounds} - is {maxExample}");
            double meanExecMs = (double)elapsedTotal.TotalMilliseconds / keys.Count();
            Console.WriteLine($"\rMean execution time is {meanExecMs} ms");
            Console.WriteLine($"Longest execution time {longestElapsed.TotalMilliseconds} ms found for {longestExecutionExample}");            
        }
    }
}
