using CalculatorAsp.Models;

namespace CalculatorAsp.Services
{
    public interface IRequestHandlerService
    {
        CalculateViewModel GetViewModel(string line);
    }

    public class RequestHandlerService : IRequestHandlerService
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IParserService _parserService;
        private readonly ITokenizerService _tokenizerService;

        public RequestHandlerService(ICalculatorService calculatorService, IParserService parserService,
            ITokenizerService tokenizerService)
        {
            _calculatorService = calculatorService;
            _parserService = parserService;
            _tokenizerService = tokenizerService;
        }

        public CalculateViewModel GetViewModel(string line)
        {
            return new CalculateViewModel()
            {
                Result = _calculatorService.Calculate(
                    _parserService.ParseToReversePolishNotation(_tokenizerService.GetTokens(line)))
            };
        }
    }
}