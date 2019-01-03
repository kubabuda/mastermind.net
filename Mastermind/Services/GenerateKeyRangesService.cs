using Mastermind.Models;
using Mastermind.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Services
{
    public class GenerateKeyRangesService : IGenerateKeyRangesService
    {
        private readonly Dictionary<int, char> _charToCodeLetterMappings = new Dictionary<int, char>
        {
            { 0, 'A' },
            { 1, 'B' },
            { 2, 'C' },
            { 3, 'D' },
            { 4, 'E' },
            { 5, 'F' },
            { 6, 'G' },
            { 7, 'H' },
            { 8, 'I' },
            { 9, 'J' },
        };

        public IEnumerable<string> GenerateCodes(IGameSettings gameSettings)
        {
            int codesCount = (int)Math.Pow(gameSettings.Colors, gameSettings.Digits);

            return Enumerable.Range(0, codesCount)
                .Select(c => ConvertToCode(c, gameSettings))
                .ToList();
        }

        public string ConvertToCode(int value, IGameSettings gameSettings)
        {
            var _temp = new List<char>();
            for (int i = 0; i < gameSettings.Digits; ++i)
            {
                _temp.Add(_charToCodeLetterMappings[value % gameSettings.Colors]);
                //_temp.Add(Convert.ToChar(A_numeric + value % gameSettings.Colors));

                value /= gameSettings.Colors;
            }
            var result = new string(_temp.ToArray());

            return result;
        }
    }
}
