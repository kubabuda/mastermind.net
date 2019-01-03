using Mastermind.Models;
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
            //var gameSettings = new GameSettings(6, answer.Length);
            var gameFactory = new GameFactory();
            //var gamePlay = new MastermindGameplayService();


            var game = gameFactory.PrepareGame(answer, colors);

            //gamePlay.Start();

            Console.ReadLine();
        }
    }
}
