using Mastermind.Models;

namespace Mastermind.Services.Interfaces
{
    interface ISolveMastermindService
    {
        IGameResultDto SolveGame(IMastermindGame mastermindGame);
    }
}
