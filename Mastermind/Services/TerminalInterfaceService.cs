using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;

namespace Mastermind.Services
{
    public class TerminalInterfaceService: ITerminalInterfaceService
    {
        private readonly IMastermindGame _gameService;

        public TerminalInterfaceService(IMastermindGame gameService)
        {
            _gameService = gameService;
        }

        public string GetCurrentAnswer()
        {
            string result = null;
            while(!IsAnswerValid(result))
            {
                try
                {
                    result = Console.ReadLine().ToUpper().Substring(0, _gameService.AnswerLength);
                    if(!IsAnswerValid(result))
                    {
                        throw new ArgumentException("Invalid answer");
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine($"Answer {result} is not valid! Pass new one.");
                }
            }
            return result;
        }

        public bool IsAnswerValid(string answer)
        {
            return !string.IsNullOrEmpty(answer) && answer.Length == _gameService.AnswerLength;
        }

        public void ShowIntroduction()
        {
            Console.WriteLine(string.Format($"Code length: {_gameService.AnswerLength}"));
            Console.WriteLine(string.Format($"Round\tGuess\tWhite,black points"));
        }


        public void ShowAnswerCheck(string currentAnswer, IAnswerCheckDto answerCheck)
        {
            var white = new String('x', answerCheck.WhitePoints);
            var black = new String('o', answerCheck.BlackPoints);

            Console.WriteLine(string.Format($"#{_gameService.Rounds}\t{currentAnswer}\t{white}{black}"));
        }

        public void ShowGameScore(IAnswerCheckDto answerCheck)
        {
            if(answerCheck.WhitePoints == _gameService.AnswerLength)
            {
                Console.WriteLine($"Congratulation, you win");
            }
            else
            {
               Console.WriteLine($"Game over, passcode not found");
            }
            Console.WriteLine(string.Format($" after {_gameService.Rounds} rounds"));
        }
    }
}
