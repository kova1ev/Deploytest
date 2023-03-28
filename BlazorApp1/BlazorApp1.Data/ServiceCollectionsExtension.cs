using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp1.Data;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddDb(this IServiceCollection services)
    {
        return services.AddDbContext<AppDbContext>(options =>
       {
           options.UseInMemoryDatabase("myDb");
       });
    }
}
