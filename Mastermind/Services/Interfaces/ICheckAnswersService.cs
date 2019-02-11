using Mastermind.Models;

namespace Mastermind.Services.Interfaces
{
    public interface ICheckAnswersService
    {
        IAnswerCheckDto CheckAnswer(byte[] correctAnswer, byte[] answer);
        IAnswerCheckDto CheckAnswer(string correctAnswer, string answer);
        IAnswerCheckDto BuildAnswerCheck(int correctAnswerLength, int correctValueAndPosition, int correctValueOnWrongPosition);
        bool IsAnswerValid(string answer, IGameSettings gameSettings);
    }
}
