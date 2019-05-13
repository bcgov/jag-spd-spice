import { Component, OnInit, Renderer2 } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { ScreeningRequestDataService } from './services/screening-request-data.service';
import { UserDataService } from './services/user-data.service';
import { User } from './models/user.model';
import { Store } from '@ngrx/store';
import { Subscription, forkJoin } from 'rxjs';
import { AppState } from './app-state/models/app-state';
import * as CurrentUserActions from './app-state/actions/current-user.action';
import * as MinistryScreeningTypesActions from './app-state/actions/ministry-screening-types.action';
import * as ScreeningReasonsActions from './app-state/actions/screening-reasons.action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  previousUrl: string;
  public currentUser: User;

  busy: Subscription;

  constructor(
    private renderer: Renderer2,
    private router: Router,
    private screeningRequestDataService: ScreeningRequestDataService,
    private userDataService: UserDataService,
    private store: Store<AppState>
  ) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const prevSlug = this.previousUrl;
        let nextSlug = event.url.slice(1);
        if (!nextSlug) { nextSlug = 'home'; }
        if (prevSlug) {
          this.renderer.removeClass(document.body, 'ctx-' + prevSlug);
        }
        if (nextSlug) {
          this.renderer.addClass(document.body, 'ctx-' + nextSlug);
        }
        this.previousUrl = nextSlug;
      }
    });
  }

  ngOnInit(): void {
    this.busy = forkJoin(
        this.userDataService.getCurrentUser(),
        this.screeningRequestDataService.getMinistryScreeningTypes(),
        this.screeningRequestDataService.getScreeningReasons())
      .subscribe(([ user, ministryScreeningTypes, screeningReasons ]) => {
        this.currentUser = user;
        this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(user));
        this.store.dispatch(new MinistryScreeningTypesActions.SetMinistryScreeningTypesAction(ministryScreeningTypes));
        this.store.dispatch(new ScreeningReasonsActions.SetScreeningReasonsAction(screeningReasons));
      });
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
