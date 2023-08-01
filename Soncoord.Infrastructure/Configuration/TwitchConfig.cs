namespace Soncoord.Infrastructure.Configuration
{
    public class TwitchConfig
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string BotId { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public Endpoints Endpoints { get; set; } = new Endpoints();
        public Callbacks Callbacks { get; set; } = new Callbacks();
    }
}
