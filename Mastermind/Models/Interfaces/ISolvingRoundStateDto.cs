using System.Collections.Generic;
using Mastermind.Services.Interfaces;

namespace Mastermind.Models.Interfaces
{
    public interface ISolvingRoundStateDto
    {
        string Answer { get; set; }
        List<string> KeySpace { get; }
        IMastermindGame MastermindGame { get; }
        IGameSettings Settings { get; }
        int Round { get; set; }
    }
}