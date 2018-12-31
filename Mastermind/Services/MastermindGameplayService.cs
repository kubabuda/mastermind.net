using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using System;

namespace Mastermind
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
            Console.WriteLine(string.Format($"Code length: {mastermindGameService.AnswerLength}"));

            while(!mastermindGameService.IsFinished)
            {
                string currentAnswer = Console.ReadLine();
                IAnswerCheckDto answerCheck = mastermindGameService.PlayRound(currentAnswer);

                if(!mastermindGameService.IsFinished)
                {
                    Console.WriteLine(string.Format($"#{mastermindGameService.Rounds} {currentAnswer} W:{answerCheck.WhitePoints} B: W:{answerCheck.BlackPoints}"));
                }
            }

            Console.WriteLine(string.Format($"Win after {mastermindGameService.Rounds} rounds"));
        }
    }
}
