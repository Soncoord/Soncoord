namespace Soncoord.Infrastructure.Configuration
{
    public class AppSettings
    {
        public const string Providers = "Providers";

        public TwitchConfig Twitch { get; set; } = new TwitchConfig();
    }
}
