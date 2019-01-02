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

        public GameResultDto Start(int roundsLeft = -1)
        {
            int rounds = 0;
            var answerCheck = _game.LastCheck;
            _interface.ShowIntroduction();

            while (!answerCheck.IsCorrect && roundsLeft != 0)
            {
                string currentAnswer = _interface.GetCurrentAnswer();
                answerCheck = _game.PlayRound(currentAnswer);
                _interface.ShowAnswerCheck(currentAnswer, answerCheck);
                ++rounds;
                --roundsLeft;
            }
            _interface.ShowGameScore(answerCheck);

            return new GameResultDto(rounds, answerCheck.IsCorrect);
        }
    }
}
