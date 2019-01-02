using Mastermind.Models;
using System.Collections.Generic;

namespace Mastermind.Services.Interfaces
{
    public interface IMastermindGame
    {
        IEnumerable<string> Answers { get; }
        Dictionary<string, IAnswerCheckDto> AnswerChecks { get; }
        IAnswerCheckDto LastCheck { get; }

        int Rounds { get; }
        int AnswerLength { get; }

        IAnswerCheckDto PlayRound(string answerToCheck);
    }
}
