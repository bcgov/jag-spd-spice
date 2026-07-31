export type AlertSeverity = "info" | "warning" | "danger";

/**
 * Notification Banner Configuration.
 *
 * @export
 * @interface Configuration
 */
export interface Configuration {
  /**
   * The date when the banner should start being visible.
   *
   * @type {string}
   */
  notificationBannerStartDate?: string;
  /**
   * The date when the banner should stop being visible.
   *
   * @type {string}
   */
  notificationBannerEndDate?: string;
  /**
   * The banner message to display.
   *
   * @type {string}
   */
  notificationBannerMessage?: string;
  /**
   * Indicates if the banner should restrict access to the application (outage) or just display a message and allow
   * users to continue to the use application as normal (notification).
   *
   * @type {AlertSeverity}
   */
  notificationBannerSeverity?: AlertSeverity;
  /**
   * Indicates if the banner should restrict access to the application (outage) or just display a message and allow
   * users to continue to the use application as normal (notification).
   *
   * @type {boolean}
   */
  notificationBannerIsOutage?: boolean;
}
