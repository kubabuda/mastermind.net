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
            IInterfaceService terminalInterface)
        {
            _correctAnswer = answerToGuess;
            _game = mastermindGame;
            _interface = terminalInterface;
        }

        public static MastermindGameplayService CreateTerminalGame(string answerToGuess)
        {
            var game = new MastermindGameService(answerToGuess, new AnswerCheckService());

            return new MastermindGameplayService(answerToGuess, game, new TerminalInterfaceService(game));
        }

        public void Start() //int rounds = -1)
        {
            IAnswerCheckDto answerCheck = _game.InitialCheckState;
            _interface.ShowIntroduction();

            while (!answerCheck.IsCorrect)// && rounds != 0)
            {
                string currentAnswer = _interface.GetCurrentAnswer();
                answerCheck = _game.PlayRound(currentAnswer);
                _interface.ShowAnswerCheck(currentAnswer, answerCheck);
                //--rounds;
            }
            _interface.ShowGameScore(answerCheck);
        }
    }
}
