namespace Soncoord.Infrastructure.Configuration
{
    public class TwitchConfig
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string BotId { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public EndpointsConfig Endpoints { get; set; } = new EndpointsConfig();
        public CallbacksConfig Callbacks { get; set; } = new CallbacksConfig();
    }
}
