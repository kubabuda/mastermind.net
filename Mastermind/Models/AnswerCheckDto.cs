using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Models
{
    public interface IAnswerCheckDto
    {
        // bull: correct value and position
        int WhitePoints { get; }
        // cow: value in incorrect position
        int BlackPoints { get; }
        // game is finished when white points == answer length & black points == 0, but DTO doesnt know correct answer or its length
        bool IsFinished { get; }
    }

    public class AnswerCheckDto: IAnswerCheckDto
    {
        public int WhitePoints { get; private set; }
        public int BlackPoints { get; private set; }
        public bool IsFinished { get; private set; }

        public AnswerCheckDto(int white, int black, bool isFinished)
        {
            WhitePoints = white;
            BlackPoints = black;
            IsFinished = isFinished;
        }
    }
}
