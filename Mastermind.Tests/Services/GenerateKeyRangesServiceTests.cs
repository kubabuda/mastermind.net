using System.Collections.Generic;
using System.Linq;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services
{
    public class GenerateKeyRangesServiceTests
    {
        IGenerateKeyRangesService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _serviceUnderTests = new GenerateKeyRangesService();
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        public void GenerateCodes_returnsArrayWithA_ForCAndZero(int c)
        {
            var result = _serviceUnderTests.GenerateCodes(c, 0);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GenerateCodes_returnsAbcCodes_For3AndOne()
        {
            var result = _serviceUnderTests.GenerateCodes(3, 1);

            Assert.AreEqual(new List<string>() { "A", "B", "C" }, result);
        }


        [Test]
        public void GenerateCodes_returnsExpectedCodes_For3And2()
        {
            var result = _serviceUnderTests.GenerateCodes(3, 2);

            Assert.AreEqual(new List<string>() {
                "AA", "BA", "CA",
                "AB", "BB", "CB",
                "AC", "BC", "CC",
            }, result);
        }

        [Test]
        public void GenerateCodes_returnsAbcdeCodes_For5AndOne()
        {
            var result = _serviceUnderTests.GenerateCodes(5, 1);

            Assert.AreEqual(new List<string>() { "A", "B", "C", "D", "E" }, result);
        }

        [TestCase(0, 1, 1, "A")]
        [TestCase(0, 6, 1, "A")]
        [TestCase(3, 6, 1, "D")]
        [TestCase(3, 6, 2, "DA")]
        [TestCase(0, 6, 2, "AA")]
        [TestCase(0, 6, 3, "AAA")]
        [TestCase(1, 6, 3, "BAA")]
        public void ConvertToCode(int value, int colors, int digits, string expectedCode)
        {
            var result = _serviceUnderTests.ConvertToCode(value, colors, digits);

            Assert.AreEqual(expectedCode, result);
        }
    }

    
}