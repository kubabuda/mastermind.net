namespace Mastermind.Models
{
    public class GameResultDto : IGameResultDto
    {
        public int Rounds { get; }
        public bool IsAnswerFound { get; }

        public GameResultDto(int rounds, bool isGameWon)
        {
            Rounds = rounds;
            IsAnswerFound = isGameWon;
        }
    }
}
