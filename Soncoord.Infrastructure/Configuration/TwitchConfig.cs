namespace Soncoord.Infrastructure.Configuration
{
    public class TwitchConfig
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClienbtSecret { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public Endpoints Endpoints { get; set; } = new Endpoints();
        public Callbacks Callbacks { get; set; } = new Callbacks();
    }
}
