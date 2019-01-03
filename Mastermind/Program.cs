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
            var gameSettings = new GameSettings(6, answer.Length);
            var game = MastermindGameplayService.CreateTerminalGame(answer, gameSettings);
            game.Start();
            
            Console.ReadLine();
        }
    }
}
