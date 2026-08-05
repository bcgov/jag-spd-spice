import { Component, Input } from "@angular/core";
import { Configuration } from "@appmodels/configuration";
import moment from "moment-timezone";

const AMERICA_VANCOUVER_TIMEZONE = "America/Vancouver";

/**
 * A generic notification banner.
 *
 * @export
 * @class NotificationBannerComponent
 */
@Component({
  selector: "app-notification-banner",
  templateUrl: "./notification-banner.component.html",
  styleUrls: ["./notification-banner.component.scss"],
})
export class NotificationBannerComponent {
  @Input() configuration?: Configuration;

  /**
   * Indicates if the notification banner should be visible.
   *
   * @readonly
   * @type {*}  {boolean} `true` if the banner should be visible, `false` otherwise.
   */
  get isVisibile(): boolean {
    return (
      this.isNotificationBannerConfigurationValid() &&
      this.isNotificationBannerActive()
    );
  }

  /**
   * Indicates if the banner notification is an outage, and access to the application should be restricted while the
   * banner is active.
   *
   * @readonly
   * @type {boolean}
   */
  get isOutage(): boolean {
    console.log(this.isVisibile);
    console.log(this.configuration);
    return (
      this.isVisibile && this.configuration?.notificationBannerIsOutage === true
    );
  }

  /**
   * Get the alert severity class.
   *
   * @readonly
   * @type {string}
   */
  get severityClass(): string {
    return `alert-${this.configuration?.notificationBannerSeverity ?? "warning"}`;
  }

  /**
   * Indicates if the notification banner configuration is valid.
   *
   * @return {*}  {boolean} `true` if banner configuration is valid, `false` otherwise.
   */
  isNotificationBannerConfigurationValid(): boolean {
    return (
      !!this.configuration?.notificationBannerStartDate &&
      !!this.configuration?.notificationBannerEndDate &&
      !!this.configuration?.notificationBannerMessage
    );
  }

  /**
   * Indicates if the notification banner is active based on the current date and the banner start and end dates.
   *
   * @return {*}  {boolean} `true` if the banner is active, `false` otherwise.
   */
  isNotificationBannerActive(): boolean {
    if (!this.isNotificationBannerConfigurationValid()) {
      return false;
    }

    const currentDate = moment().tz(AMERICA_VANCOUVER_TIMEZONE);

    const notificationBannerStartDate = moment(
      this.configuration?.notificationBannerStartDate,
    ).tz(AMERICA_VANCOUVER_TIMEZONE);

    const notificationBannerEndDate = moment(
      this.configuration?.notificationBannerEndDate,
    ).tz(AMERICA_VANCOUVER_TIMEZONE);

    return currentDate.isBetween(
      notificationBannerStartDate,
      notificationBannerEndDate,
      null,
      "[]",
    );
  }
}
