namespace Mastermind.Models
{
    public class GameSettings : IGameSettings
    {
        public int Colors { get; }
        public int Digits { get; }

        public GameSettings(int colors, int digits)
        {
            Colors = colors;
            Digits = digits;
        }
    }
}
