import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { Subscription, forkJoin } from "rxjs";

import * as CurrentUserActions from "./app-state/actions/current-user.action";
import * as MinistryScreeningTypesActions from "./app-state/actions/ministry-screening-types.action";
import * as ScreeningReasonsActions from "./app-state/actions/screening-reasons.action";
import { AppState } from "./app-state/models/app-state";

import { User } from "./models/user.model";
import { ScreeningRequestDataService } from "./services/screening-request-data.service";
import { UserDataService } from "./services/user-data.service";
import { ConfigService } from "@appservices/config.service";
import { Configuration } from "@appmodels/configuration";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  currentUser?: User;
  configuration?: Configuration;
  busy?: Subscription;

  appError?: Error;

  constructor(
    private screeningRequestDataService: ScreeningRequestDataService,
    private userDataService: UserDataService,
    private configService: ConfigService,
    private store: Store<AppState>,
  ) {}

  ngOnInit(): void {
    this.configService
      .load()
      .then((configuration) => {
        this.configuration = configuration;
      })
      .catch((error: Error) => {
        console.error("Failed to fetch configuration:", error);
        this.appError = error;
      });

    this.busy = forkJoin([
      this.userDataService.getCurrentUser(),
      this.screeningRequestDataService.getMinistryScreeningTypes(),
      this.screeningRequestDataService.getScreeningReasons(),
    ]).subscribe({
      next: ([user, ministryScreeningTypes, screeningReasons]) => {
        this.currentUser = user;
        this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(user));
        this.store.dispatch(
          new MinistryScreeningTypesActions.SetMinistryScreeningTypesAction(
            ministryScreeningTypes,
          ),
        );
        this.store.dispatch(
          new ScreeningReasonsActions.SetScreeningReasonsAction(
            screeningReasons,
          ),
        );
      },
      error: (error: Error) => {
        this.appError = error;
      },
    });
  }

  isIE10orLower() {
    let result = false;
    let jscriptVersion = new Function(
      "/*@cc_on return @_jscript_version; @*/",
    )();

    if (jscriptVersion !== undefined) {
      result = true;
    }

    return result;
  }
}
