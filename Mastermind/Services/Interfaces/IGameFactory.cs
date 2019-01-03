namespace Mastermind.Services.Interfaces
{
    public interface IGameFactory
    {
        IMastermindGame PrepareGame(string answer, int colors, int roundsLimit = -1);
        ISolveMastermindService PrepareDefaultGameplay();
    }
}
