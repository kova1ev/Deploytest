using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("env")]
        public Dictionary<string, string> GetT()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            var env1 = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            string env1Name = env1.EnvironmentName;
            string env1App = env1.ApplicationName;
            dict[env1.GetType().ToString()] = $"envName: {env1Name} | appNAme: {env1App}";


            var env3 = HttpContext.RequestServices.GetRequiredService<IHostEnvironment>();
            string env3Name = env3.EnvironmentName;
            string env3App = env3.ApplicationName;
            dict[env3.GetType().ToString()] = $"envName: {env3Name} | appNAme: {env3App}";

            return dict;
        }

        [HttpGet("app")]
        public ActionResult<string> GetApp()
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            string str = configuration["Test"] ?? "Undefined";
            return Ok($"\"{str}\"");
        }
    }
}