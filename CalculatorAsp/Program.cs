using CalculatorAsp.Models;
using CalculatorAsp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ITokenizerService, TokenizerServiceService>();
builder.Services.AddTransient<IParserService, ParserServiceService>();
builder.Services.AddTransient<ICalculatorService, CalculatorServiceService>();
builder.Services.AddTransient<IRequestHandlerService, RequestHandlerService>();
builder.Services.AddTransient<IDeterminerService, DeterminerService>();
builder.Services.AddSingleton<Operations>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}");

app.Run();
