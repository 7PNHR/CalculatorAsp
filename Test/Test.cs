using System;
using System.Linq;
using NUnit.Framework;
using CalculatorAsp;
using CalculatorAsp.Services;

namespace Test
{
    public class Tests
    {
        private IDeterminerService _determinerService;
        private ITokenizerService _tokenizerService;
        private IParserService _parserService;
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _determinerService = new DeterminerService(new Operations());
            _tokenizerService = new TokenizerServiceService(_determinerService);
            _parserService = new ParserServiceService(new Operations());
            _calculatorService = new CalculatorServiceService(new Operations());
        }

        [Test]
        public void TestParse()
        {
            var tokens = _tokenizerService.GetTokens("1+2").ToList();
            Assert.AreEqual(3, tokens.Count);
        }

        [Test]
        public void TestParseWithParentheses()
        {
            var tokens = _tokenizerService.GetTokens("1+2*(4+5+(14*60))").ToList();
            Assert.AreEqual(15, tokens.Count);
        }

        [Test]
        public void TestParseWithSpaces()
        {
            var tokens = _tokenizerService.GetTokens("1+     5 -   14    + 13   /   1").ToList();
            Assert.AreEqual(9, tokens.Count);
        }

        [Test]
        public void TestParseWithCommas()
        {
            var tokens = _tokenizerService.GetTokens("1,25 - 1,43 + 19,5555").ToList();
            Assert.AreEqual(5, tokens.Count);
        }

        [Test]
        public void TestParserException()
        {
            Assert.Throws<Exception>(() => _tokenizerService.GetTokens("1.25 - 1,43 + 19,5555").ToList());
        }

        [Test]
        public void TestGetPolishNotation()
        {
            var tokens = _tokenizerService.GetTokens("1+2");
            var polishNotation = _parserService.ParseToReversePolishNotation(tokens)
                .Select(x => x.Content).ToList();
            var str = string.Join("", polishNotation);
            Assert.AreEqual("12+", str);
        }

        [Test]
        public void TestGetPolishNotationWithCommas()
        {
            var tokens = _tokenizerService.GetTokens("1+2,13");
            var polishNotation = _parserService.ParseToReversePolishNotation(tokens)
                .Select(x => x.Content).ToList();
            var str = string.Join("", polishNotation);
            Assert.AreEqual("12,13+", str);
        }

        [Test]
        public void TestPolishNotationGetWithParentheses()
        {
            var tokens = _tokenizerService.GetTokens("1+2+(4*5)");
            var polishNotation = _parserService.ParseToReversePolishNotation(tokens)
                .Select(x => x.Content).ToList();
            var str = string.Join("", polishNotation);
            Assert.AreEqual("12+45*+", str);
        }

        [Test]
        public void TestCalculator()
        {
            var result = _calculatorService.Calculate(_parserService.ParseToReversePolishNotation(_tokenizerService.GetTokens("2+2")));
            Assert.AreEqual("4", result);
        }

        [Test]
        public void TestCalculatorWithMinus()
        {
            var result = _calculatorService.Calculate(_parserService.ParseToReversePolishNotation(_tokenizerService.GetTokens("2-2")));
            Assert.AreEqual("0", result);
        }

        [Test]
        public void TestCalculatorWithMultiplication()
        {
            var result = _calculatorService.Calculate(_parserService.ParseToReversePolishNotation(_tokenizerService.GetTokens("2*3+2")));
            Assert.AreEqual("8", result);
        }

        [Test]
        public void TestCalculatorWithDivision()
        {
            var result = _calculatorService.Calculate(_parserService.ParseToReversePolishNotation(_tokenizerService.GetTokens("5/2,5")));
            Assert.AreEqual("2", result);
        }

        [Test]
        public void TestCalculatorWithDivisionZero()
        {
            var result = _calculatorService.Calculate(_parserService.ParseToReversePolishNotation(_tokenizerService.GetTokens("4/(2-2)")));
            Assert.AreEqual(double.PositiveInfinity.ToString(), result);
        }
    }
}