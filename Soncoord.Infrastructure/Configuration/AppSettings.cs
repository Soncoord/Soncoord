namespace Soncoord.Infrastructure.Configuration
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; } = string.Empty;
        public ProvidersConfig Providers { get; set; } = new ProvidersConfig();
    }
}
