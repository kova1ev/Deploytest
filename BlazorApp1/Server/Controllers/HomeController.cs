using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        string[] urls =
        {
            "https://www.google.ru/",
            "https://ya.ru/",
             "https://www.ozon.ru/",
             "https://github.com/kova1ev",
             "https://metanit.com/"
        };

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
            return Ok(new { str });
        }

        [HttpGet("key")]
        public IActionResult Getkey()
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            string envKey = configuration["Keys:envKey"] ?? "Undefined";
            string userKey = configuration["Keys:userKey"] ?? "Undefined";
            return Ok(new { envKey, userKey });
        }

        [HttpGet("route")]
        public IActionResult GetRoute()
        {

            var basepath = HttpContext.Request.Host;
            var path = HttpContext.Request.Path;

            var full = HttpContext.Request.GetDisplayUrl();

            var prot = HttpContext.Request.Protocol;
            var sch = HttpContext.Request.Scheme;
            return Ok(new { prot, sch, basepath, path, full });
        }


        [HttpGet("red")]
        public IActionResult GetUrl()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, urls.Length);

            return Ok(new { link = urls[index] });
        }



        [HttpGet("ip")]
        public IActionResult GetIp()
        {
            var ipRemote = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Undefined";
            var ipLocal = HttpContext.Connection.LocalIpAddress?.ToString() ?? "Undefined";

            var CF_Connecting_IP = HttpContext.Request.Headers["CF-Connecting-IP"];
            var X_Forwarded_For = HttpContext.Request.Headers["X-Forwarded-For"];
            var True_Client_IP = HttpContext.Request.Headers["True-Client-IP"];


            return Ok(new { ipRemote, ipLocal, CF_Connecting_IP, X_Forwarded_For, True_Client_IP });
        }


        [HttpGet("headers")]
        public IActionResult GetHeadres()
        {
            var headers = HttpContext.Request.Headers;
            return Ok(headers);
        }

        [HttpGet("geo")]
        public async Task<ActionResult> GetGeo()
        {
            string ipAddress = HttpContext.Request.Headers["CF-Connecting-IP"];
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://ip-api.com/json/");

                HttpResponseMessage httpResponse = await client.GetAsync(ipAddress);
                httpResponse.EnsureSuccessStatusCode();

                result = await httpResponse.Content.ReadAsStringAsync();

            }
            return Ok(result);
        }
    }
}
