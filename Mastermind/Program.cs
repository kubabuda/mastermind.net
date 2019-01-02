using Mastermind.Services;
using System;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = "ABCD";
            var maxRounds = 6;
            var game = MastermindGameplayService.CreateTerminalGame(answer);
            game.Start();
            
            Console.ReadLine();
        }
    }
}
