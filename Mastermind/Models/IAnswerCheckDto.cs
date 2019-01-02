namespace Mastermind.Models
{
    public interface IAnswerCheckDto
    {
        bool IsCorrect { get; }
        // bull: correct value and position
        int WhitePoints { get; }
        // cow: value in incorrect position
        int BlackPoints { get; }
    }
}
