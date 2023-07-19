using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Soncoord.Infrastructure.Configuration;

namespace Soncoord.Bot
{
    public class Bot : IBot
    {
        private readonly ILogger<Bot> _logger;
        private readonly AppSettings _options;

        public Bot(ILogger<Bot> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void Send()
        {
            _logger.LogCritical("Log CRITICAL");

            // No output in the console
            _logger.LogDebug("Log DEBUG"); 

            _logger.LogError("Log ERROR");

            _logger.LogInformation("Log INFORMATION");

            // No output in the console
            _logger.LogTrace("Log TRACE");

            _logger.LogWarning("Log WARNING");
        }
    }
}