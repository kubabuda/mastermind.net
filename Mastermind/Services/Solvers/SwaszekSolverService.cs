using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services.Solvers
{
    /*
        Swaszek heuristic
     */
    public class SwaszekSolverService : ASolverService
    {
        public SwaszekSolverService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator, new AnswerCheckService())
        {
        
        }

        public override IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            var dto = BuildInitialState(mastermindGame);

            for (dto.Round = 0; !IsGameFinished(dto); ++dto.Round)
            {
                dto.LastCheck = dto.MastermindGame.PlayRound(dto.Answer);

                if (!dto.LastCheck.IsCorrect)
                {
                    PruneKeysLeft(dto.KeysLeft, dto);
                    dto.Answer = GetFirstKeyGuess(dto.KeysLeft);
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }

        protected ISolvingRoundStateDto BuildInitialState(IMastermindGame mastermindGame)
        {
            var dto = new SolvingRoundStateDto()
            {
                Answer = GetInitialKeyGuess(mastermindGame.Settings.Digits),
                Round = 0,
                MastermindGame = mastermindGame,
                KeysLeft = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList(),
            };

            return dto;
        }
    }
}
