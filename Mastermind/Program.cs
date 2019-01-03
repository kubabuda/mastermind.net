using Mastermind.Services;
using System;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = "ABCD";
            var colors = 6;
            var roundsLimit = 6;
            
            var gameFactory = new GameFactory();
            var gameplay = gameFactory.PrepareDefaultGameplay();
            var game = gameFactory.PrepareGame(answer, colors, roundsLimit);
            gameplay.SolveGame(game);

            Console.ReadLine();
        }
    }
}
