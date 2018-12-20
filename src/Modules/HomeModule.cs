using Carter;
using DIPoC.Services;
using Microsoft.AspNetCore.Http;

namespace DIPoC.Modules
{
    public class HomeModule : CarterModule
    {
        public HomeModule(IGreetingService greetingService)
        {
            Get("/", async (req, res, routeData) => await res.WriteAsync(greetingService.GetGreetings()));
        }
    }
}
