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
            var dto = BuildInitialState(mastermindGame);
            var keysLeft = _keyRangesGenerator.GenerateCodes(mastermindGame.Settings).ToList();
            
            string keyGuess = GetInitialKeyGuess(dto.Settings.Digits);

            for (dto.Round = 0; !IsGameFinished(dto); ++dto.Round)
            {
                dto.LastCheck = dto.MastermindGame.PlayRound(keyGuess);
                dto.Answer = keyGuess;

                if (!dto.LastCheck.IsCorrect)
                {
                    PruneKeys(keysLeft, dto.LastCheck, keyGuess);
                    keyGuess = GetFirstKeyGuess(keysLeft);
                }
            }

            return new GameResultDto(dto.MastermindGame.LastCheck.IsCorrect, dto.Answer, dto.MastermindGame.RoundsPlayed);
        }
    }
}
