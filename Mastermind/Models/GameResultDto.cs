using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Models
{
    public class GameResultDto
    {
        public int Rounds { get; }
        public bool IsAnswerFound { get; }

        public GameResultDto(int rounds, bool isGameWon)
        {
            Rounds = rounds;
            IsAnswerFound = isGameWon;
        }
    }
}
