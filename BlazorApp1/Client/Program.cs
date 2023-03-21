using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp1.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var s = builder.HostEnvironment.Environment;
            await Console.Out.WriteLineAsync($"> {s}");
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            Console.WriteLine(builder.HostEnvironment.BaseAddress);
            var s1 = builder.HostEnvironment.Environment;
            await Console.Out.WriteLineAsync($"Environment > {s1}");
            await builder.Build().RunAsync();
        }
    }
}