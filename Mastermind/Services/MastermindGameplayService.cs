using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using System;

namespace Mastermind.Services
{
    public class MastermindGameplayService
    {
        private readonly string correctAnswer;
        private readonly ICheckAnswersService checkAnswersService;
        private readonly IMastermindGamePlay mastermindGameService;

        public MastermindGameplayService(string answerToGuess)
        {
            correctAnswer = answerToGuess;
            checkAnswersService = new AnswerCheckService();
            mastermindGameService = new MastermindGameService(correctAnswer, checkAnswersService);
        }

        
        public void Play()
        {
            ShowIntroduction();

            while (!mastermindGameService.IsFinished)
            {
                string currentAnswer = Console.ReadLine();
                IAnswerCheckDto answerCheck = mastermindGameService.PlayRound(currentAnswer);

                if (!mastermindGameService.IsFinished)
                {
                    ShowAnswerCheck(currentAnswer, answerCheck);
                }
            }

            ShowGameScore();
        }

        private void ShowIntroduction()
        {
            Console.WriteLine(string.Format($"Code length: {mastermindGameService.AnswerLength}"));
            Console.WriteLine(string.Format($"Round\tGuess\tWhite,black points{mastermindGameService.AnswerLength}"));
        }


        private void ShowAnswerCheck(string currentAnswer, IAnswerCheckDto answerCheck)
        {
            Console.WriteLine(string.Format($"#{mastermindGameService.Rounds}\t{currentAnswer}\t{answerCheck.WhitePoints}W{answerCheck.BlackPoints}B"));
        }

        private void ShowGameScore()
        {
            Console.WriteLine(string.Format($"Win after {mastermindGameService.Rounds} rounds"));
        }
    }
}
