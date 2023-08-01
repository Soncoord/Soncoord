namespace Soncoord.Infrastructure.Configuration
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; } = string.Empty;
        public Providers Providers { get; set; } = new Providers();
    }
}
