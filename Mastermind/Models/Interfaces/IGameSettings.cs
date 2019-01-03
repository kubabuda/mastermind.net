using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.Models
{
    public interface IGameSettings
    {
        int Colors { get; }
        int Digits { get; }
    }
}
