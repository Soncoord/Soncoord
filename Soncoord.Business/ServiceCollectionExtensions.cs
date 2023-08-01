// https://learn.microsoft.com/en-us/dotnet/core/extensions/options-library-authors

using Microsoft.Extensions.DependencyInjection;
using Soncoord.Business.Services.Database;
using Soncoord.Business.Services.Twitch;
using Soncoord.Infrastructure;

namespace Soncoord.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSoncoordBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddHttpClient<ITwitchService, TwitchService>();

            return services;
        }
    }
}
