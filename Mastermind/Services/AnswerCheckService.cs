using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Mastermind.Services
{
    public class AnswerCheckService: ICheckAnswersService
    {
        public IAnswerCheckDto CheckAnswer(string correctAnswer, string answer)
        {
            if (correctAnswer.Length != answer.Length)
            {
                throw new ArgumentException("Answer and correct answer lengths are diffrent!");
            }

            int correctValueAndPosition = 0;
            var incorrectAnwers = new List<char>();
            var correctsAnswers = new List<char>();

            for (int i = 0; i < answer.Length; ++i)
            {
                if (answer[i] == correctAnswer[i])
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
            foreach (var c in incorrectAnwers)
            {
                if (correctsAnswers.Contains(c))
                {
                    correctValueOnWrongPosition++;
                    correctsAnswers.Remove(c);
                }
            }

            return BuildAnswerCheck(correctAnswer, correctValueAndPosition, correctValueOnWrongPosition);
        }

        public IAnswerCheckDto BuildAnswerCheck(string correctAnswer, int whitePts, int correctValueOnWrongPosition)
        {
            var answerLength = correctAnswer.Length;
            if(answerLength < whitePts)
            {
                throw new ArgumentException();
            }

            return new AnswerCheckDto(whitePts, correctValueOnWrongPosition, answerLength == whitePts);
        }
    }
}
