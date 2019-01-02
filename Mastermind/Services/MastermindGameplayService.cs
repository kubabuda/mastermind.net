using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services
{
    public class MastermindGameplayService
    {
        private readonly string _correctAnswer;
        private readonly IMastermindGame _gameService;
        private readonly ITerminalInterfaceService _terminalInterface;

        public MastermindGameplayService(string answerToGuess, 
            IMastermindGame mastermindGame, 
            ITerminalInterfaceService terminal)
        {
            _correctAnswer = answerToGuess;
            _gameService = new MastermindGameService(_correctAnswer, new AnswerCheckService());
            _terminalInterface = new TerminalInterfaceService(_gameService);
        }

        public static MastermindGameplayService Create(string answerToGuess)
        {
            var game = new MastermindGameService(answerToGuess, new AnswerCheckService());

            return new MastermindGameplayService(answerToGuess, game, new TerminalInterfaceService(game));
        }

        public void Play() //int rounds = -1)
        {
            IAnswerCheckDto answerCheck = null;
            _terminalInterface.ShowIntroduction();

            while (!_gameService.IsFinished)// && rounds != 0)
            {
                string currentAnswer = _terminalInterface.GetCurrentAnswer();
                answerCheck = _gameService.PlayRound(currentAnswer);

                if (!_gameService.IsFinished)
                {
                    _terminalInterface.ShowAnswerCheck(currentAnswer, answerCheck);
                }
                //--rounds;
            }
            _terminalInterface.ShowGameScore(answerCheck);
        }
    }
}
