using Mastermind.Services.Interfaces;
using Mastermind.Services.Solvers;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services.Solvers
{
    public class BruteforceSolverServiceTests
    {
        IGenerateKeyRangesService _keyRangesGenerator;
        BruteforceSolverService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _keyRangesGenerator = Substitute.For<IGenerateKeyRangesService>();
            _serviceUnderTests = new BruteforceSolverService(_keyRangesGenerator);
        }
    }
}
