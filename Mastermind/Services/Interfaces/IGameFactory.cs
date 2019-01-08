using Mastermind.Models;

namespace Mastermind.Services.Interfaces
{
    public interface IGameFactory
    {
        IMastermindGame PrepareGame(string answer, int colors, int roundsLimit = -1);
        IMastermindGame PrepareGame(string answer, IGameSettings gameSettings);
        ISolveMastermindService PrepareDefaultGameplay();
    }
}
