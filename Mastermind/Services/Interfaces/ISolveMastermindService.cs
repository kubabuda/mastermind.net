using Mastermind.Models;

namespace Mastermind.Services.Interfaces
{
    public interface ISolveMastermindService
    {
        IGameResultDto SolveGame(IMastermindGame mastermindGame);
    }
}
