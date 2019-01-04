namespace Mastermind.Models
{
    public interface IGameResultDto
    {
        bool IsAnswerFound { get; }
        string Answer { get; }
        int Rounds { get; }
    }
}