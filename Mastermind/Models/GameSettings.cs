namespace Mastermind.Models
{
    public class GameSettings : IGameSettings
    {
        public int Colors { get; }
        public int Digits { get; }
        public int RoundLimit { get; }

        public GameSettings(int colors, int digits, int roundsLimit = -1)
        {
            Colors = colors;
            Digits = digits;
            RoundLimit = roundsLimit;
        }
    }
}
