using System.Collections.Generic;
using System.Text;
using DIPoC.Greetings;

namespace DIPoC.Services
{
    public class GreetingService : IGreetingService
    {
        private readonly IEnumerable<IGreeting> _greetings;

        public GreetingService(IEnumerable<IGreeting> greetings)
        {
            _greetings = greetings;
        }
        
        public string GetGreetings()
        {
            var sb = new StringBuilder();
            foreach (var greeting in _greetings)
            {
                sb.AppendLine(greeting.Salutation);
            }
            return sb.ToString();
        }
    }
}