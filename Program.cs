// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Serilog;

Console.WriteLine("Hello, World!");
var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
/*Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(config)  // ← loads settings from JSON
               .CreateLogger();*/
