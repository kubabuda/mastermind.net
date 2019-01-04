namespace Mastermind.Models
{
    public class GameResultDto : IGameResultDto
    {
        public int Rounds { get; }
        public bool IsAnswerFound { get; }
        public string Answer { get; }

        internal GameResultDto(bool isGameWon, string answer, int rounds)
        {
            Answer = answer;
            Rounds = rounds;
            IsAnswerFound = isGameWon;
        }
    }
}
