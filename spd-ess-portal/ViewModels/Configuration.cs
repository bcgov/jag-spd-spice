using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Gov.Jag.Spice.Public.ViewModels
{
    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum NotificationBannerSeverity
    {
        Info,
        Warning,
        Danger,
    }

    public class Configuration
    {
        /// <summary>
        /// The message to display in the notification banner.
        /// </summary>
        public string NotificationBannerMessage { get; set; }

        /// <summary>
        /// The start date for the notification banner to be displayed. The banner will not be displayed before this date.
        /// </summary>
        public string NotificationBannerStartDate { get; set; }

        /// <summary>
        /// The end date for the notification banner to be displayed. The banner will not be displayed after this date.
        /// </summary>
        public string NotificationBannerEndDate { get; set; }

        /// <summary>
        /// Indicates if the banner should restrict access to the application (outage) or just display a message and allow
        /// users to continue to the use application as normal (notification).
        /// </summary>
        public bool? NotificationBannerIsOutage { get; set; }

        /// <summary>
        /// The severity of the notification banner. Defaults to <see cref="NotificationBannerSeverity.Warning"/>.
        /// </summary>
        public NotificationBannerSeverity NotificationBannerSeverity { get; set; } = NotificationBannerSeverity.Warning;
    }
}
