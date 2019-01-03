using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services
{
    public interface IInterfaceService
    {
        void ShowIntroduction(IMastermindGame gameService);
        string GetCurrentAnswer();
        void ShowAnswerCheck(string currentAnswer, IAnswerCheckDto answerCheck);
        void ShowGameScore(IAnswerCheckDto answerCheck);
    }
}
