using Mastermind.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Services
{
    public class AnswerCheckService
    {
        public AnswerStats CheckAnswer(string correctAnswer, string answer)
        {
            if(correctAnswer.Length != answer.Length)
            {
                throw new ArgumentException("Answer and correct answer lengths are diffrent!");
            }

            int correctValueAndPosition = 0;
            var incorrects = new List<char>();
            var corrects = new List<char>();

            for (int i = 0; i < answer.Length; ++i)
            {
                if(answer[i] == correctAnswer[i])
                {
                    correctValueAndPosition++;
                }
                else
                {
                    incorrects.Add(answer[i]);
                    corrects.Add(correctAnswer[i]);
                }
            }

            return new AnswerStats(correctValueAndPosition, 0);
        }
    }
}
