using Mastermind.Services;
using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class EduinfSolverServiceTests
    {
        IGenerateKeyRangesService _keyRangesGenerator;
        EduinfSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = Substitute.For<IGenerateKeyRangesService>();
            _serviceUnderTests = new EduinfSolverService(_keyRangesGenerator);
        }
    }

    public class EduinfSolverServiceIntegrationTests
    {
        EduinfSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            var generator = new GenerateKeyRangesService();
            _serviceUnderTests = new EduinfSolverService(generator);
        }

        [Test]
        public void SolveGame_()
        {

        }
    }
}
