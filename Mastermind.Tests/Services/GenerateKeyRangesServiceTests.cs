using System.Collections.Generic;
using System.Linq;
using Mastermind.Models;
using Mastermind.Services;
using Mastermind.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Mastermind.Tests.Services
{
    public class GenerateKeyRangesServiceTests
    {
        IGenerateKeyRangesService _serviceUnderTests;

        IGameSettings gameSettings;

        [SetUp]
        public void Setup()
        {
            _serviceUnderTests = new GenerateKeyRangesService();
            gameSettings = Substitute.For<IGameSettings>();
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        public void GenerateCodes_returnsArrayWithA_ForCxx0Game(int c)
        {
            gameSettings.Colors.Returns(c);
            gameSettings.Digits.Returns(0);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GenerateCodes_returnsAbcCodes_For3xx1Game()
        {
            gameSettings.Colors.Returns(3);
            gameSettings.Digits.Returns(1);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(new List<string>() { "A", "B", "C" }, result);
        }


        [Test]
        public void GenerateCodes_returnsExpectedCodes_For3xx2Game()
        {
            gameSettings.Colors.Returns(3);
            gameSettings.Digits.Returns(2);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(new List<string>() {
                "AA", "BA", "CA",
                "AB", "BB", "CB",
                "AC", "BC", "CC",
            }, result);
        }

        [Test]
        public void GenerateCodes_returns1296Codes_For6xx4Game()
        {
            gameSettings.Colors.Returns(6);
            gameSettings.Digits.Returns(4);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(1296, result.Count());
        }

        [Test]
        public void GenerateCodes_returnsAbcdeCodes_For5xx1Game()
        {
            gameSettings.Colors.Returns(5);
            gameSettings.Digits.Returns(1);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(new List<string>() { "A", "B", "C", "D", "E" }, result);
        }

        [TestCase(0, 1, 1, "A")]
        [TestCase(0, 6, 1, "A")]
        [TestCase(3, 6, 1, "D")]
        [TestCase(3, 6, 2, "DA")]
        [TestCase(0, 6, 2, "AA")]
        [TestCase(0, 6, 3, "AAA")]
        [TestCase(1, 6, 3, "BAA")]
        [TestCase(545, 6, 4, "FADC")]
        public void ConvertToCode(int value, int colors, int digits, string expectedCode)
        {
            gameSettings.Colors.Returns(colors);
            gameSettings.Digits.Returns(digits);
            
            var result = _serviceUnderTests.ConvertToCode(value, gameSettings);

            Assert.AreEqual(expectedCode, result);
        }
    }   
}