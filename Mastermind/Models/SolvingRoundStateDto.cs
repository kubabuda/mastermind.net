using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using System.Collections.Generic;

namespace Mastermind.Services.Models
{
    public class SolvingRoundStateDto : ISolvingRoundStateDto
    {
        public string Answer { get; set; }
        public int Round { get; set; }
        public IMastermindGame MastermindGame { get; set; }
        public List<string> KeySpace { get; set; }
        public IGameSettings Settings { get => MastermindGame.Settings; }
    }
}
