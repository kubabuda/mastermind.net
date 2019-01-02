using Mastermind.Models;

namespace Mastermind.Services.Interfaces
{
    public interface ICheckAnswersService
    {
        IAnswerCheckDto CheckAnswer(string correctAnswer, string answer);
        IAnswerCheckDto BuildAnswerCheck(string correctAnswer, int correctValueAndPosition, int correctValueOnWrongPosition);
    }
}
