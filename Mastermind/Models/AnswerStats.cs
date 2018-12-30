using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Models
{
    public class AnswerStats
    {
        // bull: correct value and position
        public int White { get; private set; }
        // cow: value in incorrect position
        public int Black { get; private set; }

        public AnswerStats(int white, int black)
        {
            White = white;
            Black = black;
        }
    }
}
