﻿using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Mastermind.Services
{
    public class AnswerCheckService: ICheckAnswersService
    {
        public IAnswerCheckDto CheckAnswer(byte[] correctAnswer, byte[] answer) 
        {
            if (correctAnswer.Length != answer.Length)
            {
                throw new ArgumentException("Answer and correct answer lengths are diffrent!");
            }

            int correctValueAndPosition = 0;
            var incorrectAnwers = new List<byte>();
            var correctsAnswers = new List<byte>();

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

            return BuildAnswerCheck(correctAnswer.Length, correctValueAndPosition, correctValueOnWrongPosition);
        }

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

            return BuildAnswerCheck(correctAnswer.Length, correctValueAndPosition, correctValueOnWrongPosition);
        }

        public IAnswerCheckDto BuildAnswerCheck(int correctAnswerLength, int whitePts, int blackPts)
        {
            var answerLength = correctAnswerLength;
            if(answerLength < whitePts || answerLength < blackPts
                || answerLength < whitePts + blackPts 
                || blackPts < 0 || whitePts < 0 || answerLength < 1)
            {
                throw new ArgumentException();
            }

            return new AnswerCheckDto(whitePts, blackPts, answerLength == whitePts);
        }

        public bool IsAnswerValid(string answer, IGameSettings gameSettings)
        {
            if (string.IsNullOrEmpty(answer))
            {
                return false;
            }
            if (answer.Length != gameSettings.Digits)
            {
                return false;
            }
            var A_numeric = Convert.ToInt32('1'); // this depends on mappings from GenerateKeyRangesService
            var max_c_numeric = A_numeric + gameSettings.Colors - 1; 

            foreach(char c in answer)
            {
                var c_numeric = Convert.ToInt32(c);
                if(c_numeric < A_numeric || c_numeric > max_c_numeric)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
