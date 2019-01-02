using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services
{
    public class MastermindGameplayService
    {
        private readonly string _correctAnswer;
        private readonly IMastermindGame _game;
        private readonly IInterfaceService _interface;

        public MastermindGameplayService(string answerToGuess, 
            IMastermindGame mastermindGame, 
            IInterfaceService gameInterface)
        {
            _correctAnswer = answerToGuess;
            _game = mastermindGame;
            _interface = gameInterface;
        }

        public static MastermindGameplayService CreateTerminalGame(string answerToGuess)
        {
            var game = new MastermindGameService(answerToGuess, new AnswerCheckService());

            return new MastermindGameplayService(answerToGuess, game, new TerminalInterfaceService(game));
        }

        public int Start() //int rounds = -1)
        {
            _interface.ShowIntroduction();

            while (!_game.LastCheck.IsCorrect)// && rounds != 0)
            {
                string currentAnswer = _interface.GetCurrentAnswer();
                var answerCheck = _game.PlayRound(currentAnswer);
                _interface.ShowAnswerCheck(currentAnswer, answerCheck);
                //--rounds;
            }
            _interface.ShowGameScore(_game.LastCheck);

            return _game.Rounds;
        }
    }
}
