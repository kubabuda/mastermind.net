namespace Mastermind.Models
{
    public interface IGameResultDto
    {
        bool IsAnswerFound { get; }
        int Rounds { get; }
    }
}