using Mastermind.Models;
using System;
using System.Collections.Generic;

namespace Mastermind.Services
{
    public interface ICheckAnswersService
    {
        AnswerStats CheckAnswer(string correctAnswer, string answer);
    }

    public class AnswerCheckService: ICheckAnswersService
    {
        public AnswerStats CheckAnswer(string correctAnswer, string answer)
        {
            if(correctAnswer.Length != answer.Length)
            {
                throw new ArgumentException("Answer and correct answer lengths are diffrent!");
            }

            int correctValueAndPosition = 0;
            var incorrectAnwers = new List<char>();
            var correctsAnswers = new List<char>();

            for (int i = 0; i < answer.Length; ++i)
            {
                if(answer[i] == correctAnswer[i])
                {
                    correctValueAndPosition++;
                }
                else
                {
                    incorrectAnwers.Add(answer[i]);
                    correctsAnswers.Add(correctAnswer[i]);
                }
            }

            int correctValueOnWrongPosition = 0;
            foreach(var c in incorrectAnwers)
            {
                if (correctsAnswers.Contains(c))
                {
                    correctValueOnWrongPosition++;
                    correctsAnswers.Remove(c);
                }
            }

            return new AnswerStats(correctValueAndPosition, correctValueOnWrongPosition);
        }
    }
}
