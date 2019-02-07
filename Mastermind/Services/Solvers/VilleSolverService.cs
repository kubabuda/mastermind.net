
using Mastermind.Models;
using Mastermind.Services.Interfaces;

namespace Mastermind.Services.Solvers 
{

    /*
    https://arxiv.org/pdf/1305.1010.pdf
     */
    public class VilleSolverService : ASolverService
    {
        public VilleSolverService(IGenerateKeyRangesService keyRangesGenerator)
            : base(keyRangesGenerator, new AnswerCheckService())
        {
        
        }

        public override IGameResultDto SolveGame(IMastermindGame mastermindGame)
        {
            throw new System.NotImplementedException();
        }
    }
}