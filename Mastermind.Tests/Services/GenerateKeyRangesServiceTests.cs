﻿using System.Collections.Generic;
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

            Assert.AreEqual(new List<string>() { "1", "2", "3" }, result);
        }


        [Test]
        public void GenerateCodes_returnsExpectedCodes_For3xx2Game()
        {
            gameSettings.Colors.Returns(3);
            gameSettings.Digits.Returns(2);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(new List<string>() {
                "11", "21", "31",
                "12", "22", "32",
                "13", "23", "33",
            }, result);
        }

        [TestCase(6, 4, 1296)]
        [TestCase(4, 4, 256)]
        [TestCase(2, 4, 16)]
        public void GenerateCodes_returnsColorsPowDigits_GivenColorsDigits(int colors, int digits, int codeSpaceSize)
        {
            // Arrange
            // mock is slower, to see actual performance on larger set use DAO instance
            var gameSettings = new GameSettings(colors, digits);

            // Act
            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            // Assert
            Assert.AreEqual(codeSpaceSize, result.Count());
        }

        [Test]
        public void GenerateCodes_returns12345Codes_For5xx1Game()
        {
            gameSettings.Colors.Returns(5);
            gameSettings.Digits.Returns(1);

            var result = _serviceUnderTests.GenerateCodes(gameSettings);

            Assert.AreEqual(new List<string>() { "1", "2", "3", "4", "5" }, result);
        }

        [Test]
        public void GenerateByteCodes_returnsExpectedCodes_For3xx2Game()
        {
            gameSettings.Colors.Returns(3);
            gameSettings.Digits.Returns(2);

            var result = _serviceUnderTests.GenerateByteCodes(gameSettings);

            Assert.AreEqual(new List<byte[]>() {
                new byte[] { 1, 1 }, 
                new byte[] { 2, 1 }, 
                new byte[] { 3, 1 },
                new byte[] { 1, 2 }, 
                new byte[] { 2, 2 },
                new byte[] { 3, 2 },
                new byte[] { 1, 3 }, 
                new byte[] { 2, 3 },
                new byte[] { 3, 3 },
            }, result);
        }

        [TestCase(0, 1, 1, "1")]
        [TestCase(0, 6, 1, "1")]
        [TestCase(3, 6, 1, "4")]
        [TestCase(3, 6, 2, "41")]
        [TestCase(0, 6, 2, "11")]
        [TestCase(0, 6, 3, "111")]
        [TestCase(1, 6, 3, "211")]
        [TestCase(545, 6, 4, "6143")]
        [TestCase(100, 8, 5, "55211")]
        [TestCase(1000, 8, 5, "16821")]
        public void ConvertToCode(int value, int colors, int digits, string expectedCode)
        {
            gameSettings.Colors.Returns(colors);
            gameSettings.Digits.Returns(digits);
            
            var result = _serviceUnderTests.ConvertToCode(value, gameSettings);

            Assert.AreEqual(expectedCode, result);
        }
    }   
}