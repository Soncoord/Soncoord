using Microsoft.Extensions.Logging;

namespace Soncoord.Bot
{
    public class Bot : IBot
    {
        private readonly ILogger<Bot> _logger;

        public Bot(ILogger<Bot> logger)
        {
            _logger = logger;
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