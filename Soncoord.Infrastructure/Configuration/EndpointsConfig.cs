﻿namespace Soncoord.Infrastructure.Configuration
{
    public class EndpointsConfig
    {
        public string Token { get; set; } = string.Empty;
        public string Authorize { get; set; } = string.Empty;
        public string Validate { get; set;} = string.Empty;
        public string Revoke { get; set; } = string.Empty;
    }
}
