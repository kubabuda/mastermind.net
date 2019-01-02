using Mastermind.Services;
using System;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = MastermindGameplayService.Create("ABCD");
            game.Play();
            
            Console.ReadLine();
        }
    }
}
