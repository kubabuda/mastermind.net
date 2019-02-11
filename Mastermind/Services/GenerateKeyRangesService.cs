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
        };

        public IEnumerable<string> GenerateCodes(IGameSettings gameSettings)
        {
            int codesCount = (int)Math.Pow(gameSettings.Colors, gameSettings.Digits);

            return Enumerable.Range(0, codesCount)
                .Select(c => ConvertToCode(c, gameSettings))
                .ToList();
        }

        public IEnumerable<byte[]> GenerateByteCodes(IGameSettings gameSettings)
        {
            int codesCount = (int)Math.Pow(gameSettings.Colors, gameSettings.Digits);

            return Enumerable.Range(0, codesCount)
                .Select(c => ConvertToByteCode(c, gameSettings))
                .ToList();
            throw new NotImplementedException();
        }

        public string ConvertToCode(int value, IGameSettings gameSettings)
        {
            var _temp = new List<char>();
            for (int i = 0; i < gameSettings.Digits; ++i)
            {
                _temp.Add(_charToCodeLetterMappings[value % gameSettings.Colors]);
                value /= gameSettings.Colors;
            }
            var result = new string(_temp.ToArray());

            return result;
        }

        public byte[] ConvertToByteCode(int value, IGameSettings gameSettings)
        {
            var result = new List<byte>();
            for (int i = 0; i < gameSettings.Digits; ++i)
            {
                var k = (value % gameSettings.Colors) + 1;
                result.Add((byte)k);
                value /= gameSettings.Colors;
            }

            return result.ToArray();
        }
    }
}
