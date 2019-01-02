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
        public void GenerateCodes_returnsAbcdeCodes_For5AndOne()
        {
            var result = _serviceUnderTests.GenerateCodes(5, 1);

            Assert.AreEqual(new List<string>() { "A", "B", "C", "D", "E" }, result);
        }

        //[TestCase(0, 1, 1, "A")]
        //public void ConvertToCode()
        //{

        //}
    }

    public class GenerateKeyRangesService : IGenerateKeyRangesService
    {
        private Dictionary<int, string> _charToCodeLetterMappings = new Dictionary<int, string>
        {
            { 0, "A" },
            { 1, "B" },
            { 2, "C" },
            { 3, "D" },
            { 4, "E" },
            { 5, "F" },
            { 6, "G" },
            { 7, "H" },
            { 8, "I" },
            { 9, "I" },
        };

        public IEnumerable<string> GenerateCodes(int colors, int digits)
        {
            int codesCount = (int)System.Math.Pow(colors, digits);

            return Enumerable.Range(0, codesCount)
                .Select(c => ConvertToCode(c, colors, digits))
                .ToList();
        }

        public string ConvertToCode(int value, int colors, int digits)
        {
            return _charToCodeLetterMappings[value];
        }
    }
}