using Mastermind.Models;

namespace Mastermind.Services
{
    public interface ITerminalInterfaceService
    {
        void ShowIntroduction();
        string GetCurrentAnswer();
        void ShowAnswerCheck(string currentAnswer, IAnswerCheckDto answerCheck);
        void ShowGameScore(IAnswerCheckDto answerCheck);
    }
}
