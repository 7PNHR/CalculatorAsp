using CalculatorAsp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAsp.Controllers;

public class MainController : Controller
{
    private readonly IRequestHandlerService _requestHandlerService;
    public MainController(IRequestHandlerService requestHandlerService)
    {
        _requestHandlerService = requestHandlerService;
    }
    [HttpPost]
    public IActionResult Calculate(string line)
    {
        return View(_requestHandlerService.GetViewModel(line));
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

}