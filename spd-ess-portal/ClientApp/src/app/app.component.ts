import { Component, OnInit, Renderer2 } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { UserDataService } from './services/user-data.service';
import { User } from './models/user.model';
import { Store } from '@ngrx/store';
import { AppState } from './app-state/models/app-state';
import * as CurrentUserActions from './app-state/actions/current-user.action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  previousUrl: string;
  public currentUser: User;

  constructor(
    private renderer: Renderer2,
    private router: Router,
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
    this.reloadUser();
  }

  reloadUser() {
    this.userDataService.getCurrentUser()
      .subscribe((data: User) => {
        this.currentUser = data;
        this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(data));
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
