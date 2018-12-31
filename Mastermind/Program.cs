using Mastermind.Services;
using System;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new MastermindGameplayService("ABCD");
            game.Play();
            
            Console.ReadLine();
        }
    }
}
