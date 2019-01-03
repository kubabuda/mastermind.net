using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services
{
    public class GameFactory : IGameFactory
    {
        public IMastermindGame PrepareGame(string answer, int colors)
        {
            var gameSettings = new GameSettings(colors, answer.Length);
            var checkAnswers = new AnswerCheckService();
            var mastermindGame = new MastermindGameService(answer, checkAnswers, gameSettings);

            return mastermindGame;
        }
    }
}
