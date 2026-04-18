namespace Infrastructure.Services
{
    /// <summary>
    /// Configuration object for Google Maps integration.
    /// Values are populated from User Secrets or Environment Variables.
    /// </summary>
    public class GoogleRoutesSettings
    {
        /// <summary>The restricted API key for Google Routes API.</summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>The travel mode, like "DRIVING" used for transit calculations </summary>
        public string TravelMode { get; init; } = "DRIVING";
    }
}
