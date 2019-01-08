using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services
{
    public class GameFactory : IGameFactory
    {
        public ISolveMastermindService PrepareDefaultGameplay()
        {
            var terminalUi = new TerminalInterfaceService(new AnswerCheckService());
            return new MastermindGameplayService(terminalUi);
        }

        public IMastermindGame PrepareGame(string answer, int colors, int roundsLimit = -1)
        {
            var gameSettings = new GameSettings(colors, answer.Length, roundsLimit);
            
            return PrepareGame(answer, gameSettings);
        }

        public IMastermindGame PrepareGame(string answer, IGameSettings gameSettings)
        {
            var checkAnswers = new AnswerCheckService();
            var mastermindGame = new MastermindGameService(answer, checkAnswers, gameSettings);

            return mastermindGame;
        }
    }
}
