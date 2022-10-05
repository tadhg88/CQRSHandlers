using CommandHandlersMapping.Commands;
using CommandHandlersMapping.Dispatchers;
using CommandHandlersMapping.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handlers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        private readonly ITest _test;

        //public WeatherForecastController(ITest test)
        //{
        //    _test = test;
        //}

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ITest test, ICommandDispatcher commandDispatcher)
        {
            _test = test;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _test.WriteToOutput("tes blaaaaaa");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("[action]")]
        public async Task TestMyHandler(TestCommand command)
        {
            var des = "fgdfg";
            await _commandDispatcher.ExecuteAsync(command);
        }
    }
}
