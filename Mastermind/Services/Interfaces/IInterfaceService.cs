using Mastermind.Models;

namespace Mastermind.Services
{
    public interface IInterfaceService
    {
        void ShowIntroduction();
        string GetCurrentAnswer();
        void ShowAnswerCheck(string currentAnswer, IAnswerCheckDto answerCheck);
        void ShowGameScore(IAnswerCheckDto answerCheck);
    }
}
