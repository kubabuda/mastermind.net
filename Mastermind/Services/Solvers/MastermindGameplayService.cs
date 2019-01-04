using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services
{
    public class MastermindGameplayService: ISolveMastermindService
    {
        private readonly IInterfaceService _interface;

        public MastermindGameplayService(IInterfaceService gameInterface)
        {
            _interface = gameInterface;
        }

        public IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            int rounds = 0;
            int roundsLeft = mastermindGame.Settings.RoundLimit;
            var answerCheck = mastermindGame.LastCheck;
            _interface.ShowIntroduction(mastermindGame);
            string currentAnswer = "";

            while (!answerCheck.IsCorrect && roundsLeft != 0)
            {
                currentAnswer = _interface.GetCurrentAnswer();
                answerCheck = mastermindGame.PlayRound(currentAnswer);
                _interface.ShowAnswerCheck(currentAnswer, answerCheck);
                ++rounds;
                --roundsLeft;
            }
            _interface.ShowGameScore(answerCheck);

            return new GameResultDto(answerCheck.IsCorrect, currentAnswer, rounds);
        }
    }
}
