using Mastermind.Models.Interfaces;
using Mastermind.Services.Models;
using System.Collections.Generic;

namespace Mastermind.Models
{
    public class KnuthSolvingRoundStateDto : SolvingRoundStateDto, IKnuthRoundStateDto
    {
        public List<string> PossibleKeys { get; internal set; }
    }
}
