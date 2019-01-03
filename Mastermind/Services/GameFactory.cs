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

        public IMastermindGame PrepareGame(string answer, int colors)
        {
            var gameSettings = new GameSettings(colors, answer.Length);
            var checkAnswers = new AnswerCheckService();
            var mastermindGame = new MastermindGameService(answer, checkAnswers, gameSettings);

            return mastermindGame;
        }
    }
}
