using System.Runtime.Caching;
using DIPoC.Greetings;
using DIPoC.Services;

namespace DIPoC
{
    using Carter;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCarter();
            services.AddSingleton<ObjectCache>(MemoryCache.Default);
            
            //-- USING https://github.com/khellang/Scrutor --//
            services.AddScoped<IGreetingService, GreetingService>();
            services.Decorate<IGreetingService, CachedGreetingService>();
            services.Scan(scan => 
                scan.FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes.AssignableTo<IGreeting>())
                    .As<IGreeting>()
                    .WithScopedLifetime()
                );
            
            //-- USING just Microsoft.Extensions.DependencyInjection --//
            /*services.AddScoped<IGreeting, English>();
            services.AddScoped<IGreeting, French>();
            services.AddScoped<IGreeting, German>();
            services.AddScoped<IGreeting, Spanish>();

            services.AddScoped<GreetingService>();
            
            services.AddSingleton<ObjectCache>(MemoryCache.Default);
            services.AddScoped<IGreetingService>(provider => new CachedGreetingService(provider.GetService<GreetingService>(), provider.GetService<ObjectCache>()));*/
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseCarter();
        }
    }
}
