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

        public IGameResultDto SolveGame(IMastermindGame mastermindGame, int roundsLeft = -1)
        {
            int rounds = 0;
            var answerCheck = mastermindGame.LastCheck;
            _interface.ShowIntroduction(mastermindGame);

            while (!answerCheck.IsCorrect && roundsLeft != 0)
            {
                string currentAnswer = _interface.GetCurrentAnswer();
                answerCheck = mastermindGame.PlayRound(currentAnswer);
                _interface.ShowAnswerCheck(currentAnswer, answerCheck);
                ++rounds;
                --roundsLeft;
            }
            _interface.ShowGameScore(answerCheck);

            return new GameResultDto(rounds, answerCheck.IsCorrect);
        }
    }
}
