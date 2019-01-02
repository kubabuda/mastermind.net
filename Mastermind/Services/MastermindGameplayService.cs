using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;

namespace Mastermind.Services
{
    public class MastermindGameplayService
    {
        private readonly string correctAnswer;
        private readonly ICheckAnswersService checkAnswersService;
        private readonly IMastermindGamePlay gameService;

        public MastermindGameplayService(string answerToGuess)
        {
            correctAnswer = answerToGuess;
            checkAnswersService = new AnswerCheckService();
            gameService = new MastermindGameService(correctAnswer, checkAnswersService);
        }

        
        public void Play(int rounds = -1)
        {
            ShowIntroduction();

            while (!gameService.IsFinished && rounds != 0)
            {
                string currentAnswer = GetCurrentAnswer();
                IAnswerCheckDto answerCheck = gameService.PlayRound(currentAnswer);

                if (!gameService.IsFinished)
                {
                    ShowAnswerCheck(currentAnswer, answerCheck);
                }
                --rounds;
            }

            ShowGameScore();
        }

        private string GetCurrentAnswer()
        {
            string result = null;
            while(!IsAnswerValid(result))
            {
                try
                {
                    result = Console.ReadLine().ToUpper().Substring(0, gameService.AnswerLength);
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

        private bool IsAnswerValid(string answer)
        {
            return !string.IsNullOrEmpty(answer) && answer.Length == gameService.AnswerLength;
        }

        private void ShowIntroduction()
        {
            Console.WriteLine(string.Format($"Code length: {gameService.AnswerLength}"));
            Console.WriteLine(string.Format($"Round\tGuess\tWhite,black points"));
        }


        private void ShowAnswerCheck(string currentAnswer, IAnswerCheckDto answerCheck)
        {
            var white = new String('x', answerCheck.WhitePoints);
            var black = new String('o', answerCheck.BlackPoints);

            Console.WriteLine(string.Format($"#{gameService.Rounds}\t{currentAnswer}\t{white}{black}"));
        }

        private void ShowGameScore()
        {
            Console.WriteLine(string.Format($"Game end after {gameService.Rounds} rounds"));
        }
    }
}
