using Mastermind.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services
{
    public class GenerateKeyRangesService : IGenerateKeyRangesService
    {
        private readonly Dictionary<int, string> _charToCodeLetterMappings = new Dictionary<int, string>
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
            var codeLetters = new List<string>();

            var quotient = value;
            for (int i = 0; i < digits; ++i)
            {
                codeLetters.Add(_charToCodeLetterMappings[quotient % colors]);
                quotient = value / colors;
            }

            return string.Join("", codeLetters);
        }
    }
}
