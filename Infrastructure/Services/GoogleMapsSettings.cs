using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    /// <summary>
    /// Configuration object for Google Maps integration.
    /// Values are populated from User Secrets or Environment Variables.
    /// </summary>
    public class GoogleMapsSettings
    {
        /// <summary>The restricted API key for Google Routes API.</summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
