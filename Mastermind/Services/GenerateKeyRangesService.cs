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
            { 0, '1' },
            { 1, '2' },
            { 2, '3' },
            { 3, '4' },
            { 4, '5' },
            { 5, '6' },
            { 6, '7' },
            { 7, '8' },
            { 8, '9' },
            // { 9, '' },
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
