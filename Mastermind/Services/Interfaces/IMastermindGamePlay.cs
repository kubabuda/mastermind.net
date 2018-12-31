using Mastermind.Models;
using System.Collections.Generic;

namespace Mastermind.Services.Interfaces
{
    public interface IMastermindGamePlay
    {
        IEnumerable<string> Answers { get; }
        Dictionary<string, IAnswerCheckDto> AnswerChecks { get; }
        int Rounds { get; }
        int AnswerLength { get; }
        bool IsFinished { get; }

        IAnswerCheckDto PlayRound(string answerToCheck);
    }
}
