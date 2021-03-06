﻿using Mastermind.Models;
using System.Collections.Generic;

namespace Mastermind.Services.Interfaces
{
    public interface IGenerateKeyRangesService
    {
        // Generates allcaps alphanumeric codes for given code space
        // colors - number of allowed variants per socket, eg. 3 for [ A, B, C ]
        // length - code length, eg. 4 for ABCD
        IEnumerable<string> GenerateCodes(IGameSettings gameSettings);
        IEnumerable<byte[]> GenerateByteCodes(IGameSettings gameSettings);
        string ConvertToCode(int value, IGameSettings gameSettings);
    }
}
