using Mastermind.Models;
using Mastermind.Models.Interfaces;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services.Solvers
{
    public class EduinfSolverService : ASolverService
    {
        public EduinfSolverService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator, new AnswerCheckService())
        {
        
        }

        public override IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            // simplified Knuth five-guess algorithm from EduInf page 
            var dto = GetInitialState(mastermindGame);

            for (dto.Round = 0; !IsGameFinished(dto); ++dto.Round)
            {
                string keyGuess = GetKeyGuess(dto);

                dto.LastCheck = dto.MastermindGame.PlayRound(keyGuess);
                dto.Answer = keyGuess;

                if (!dto.LastCheck.IsCorrect)
                {
                    dto.KeySpace.Remove(keyGuess);
                    dto.KeySpace.RemoveAll(key => IsKeyToBeRemoved(key, keyGuess, dto.LastCheck));
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }
    }
}
