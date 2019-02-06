using System.Collections.Generic;

namespace Mastermind.Models.Interfaces
{
    public interface IKnuthRoundStateDto: ISolvingRoundStateDto {
        List<string> PossibleKeys { get; }
    }
}
