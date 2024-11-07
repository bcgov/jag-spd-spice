import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription, forkJoin } from 'rxjs';

import * as CurrentUserActions from './app-state/actions/current-user.action';
import * as MinistryScreeningTypesActions from './app-state/actions/ministry-screening-types.action';
import * as ScreeningReasonsActions from './app-state/actions/screening-reasons.action';
import { AppState } from './app-state/models/app-state';

import { User } from './models/user.model';
import { ScreeningRequestDataService } from './services/screening-request-data.service';
import { UserDataService } from './services/user-data.service';
import { ConfigService } from '@appservices/config.service';
import { Configuration } from '@appmodels/configuration';
import * as moment from 'moment-timezone';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  currentUser: User;
  configuration: Configuration;
  busy: Subscription;

  error = false;

  constructor(
    private screeningRequestDataService: ScreeningRequestDataService,
    private userDataService: UserDataService,
    private configService: ConfigService,
    private store: Store<AppState>
  ) { }

  ngOnInit(): void {
    this.configService.load()
      .then((configuration) => {
        console.log("Fetched Configuration:", configuration);
        this.configuration = configuration;
      })
      .catch((error) => {
        console.error("Failed to fetch configuration:", error);
        this.error = error;
      });

    this.busy = forkJoin(
      this.userDataService.getCurrentUser(),
      this.screeningRequestDataService.getMinistryScreeningTypes(),
      this.screeningRequestDataService.getScreeningReasons(),
    ).subscribe(([user, ministryScreeningTypes, screeningReasons]) => {
      this.currentUser = user;
      this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(user));
      this.store.dispatch(new MinistryScreeningTypesActions.SetMinistryScreeningTypesAction(ministryScreeningTypes));
      this.store.dispatch(new ScreeningReasonsActions.SetScreeningReasonsAction(screeningReasons));
    }, error => {
      this.error = error;
    });
  }

  isOutage() {
    if (!this.configuration?.outageEndDate || !this.configuration?.outageStartDate || !this.configuration?.outageMessage) {
      return false;
    }
    const currentDate = moment().tz("America/Vancouver");
    const outageStartDate = moment(this.configuration.outageStartDate).tz("America/Vancouver");
    const outageEndDate = moment(this.configuration.outageEndDate).tz("America/Vancouver");
    return currentDate.isBetween(outageStartDate, outageEndDate, null, '[]');
  }


  generateOutageDateMessage(): string {
    const startDate = moment(this.configuration.outageStartDate).tz("America/Vancouver").format("MMMM Do YYYY, h:mm a");
    const endDate = moment(this.configuration.outageEndDate).tz("America/Vancouver").format("MMMM Do YYYY, h:mm a");
    return "The system will be unavailable from " + startDate + " to " + endDate;
  }

  isIE10orLower() {
    let result, jscriptVersion;
    result = false;

    jscriptVersion = new Function('/*@cc_on return @_jscript_version; @*/')();

    if (jscriptVersion !== undefined) {
      result = true;
    }
    return result;
  }
}
