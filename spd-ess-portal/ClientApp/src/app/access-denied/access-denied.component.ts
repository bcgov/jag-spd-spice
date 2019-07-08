import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';

import { AppState } from '../app-state/models/app-state';

import { User } from '../models/user.model';

@Component({
  selector: 'app-access-denied',
  templateUrl: './access-denied.component.html',
  styleUrls: ['./access-denied.component.scss'],
})
export class AccessDeniedComponent implements OnInit, OnDestroy {
  user: User;

  unsubscribe: Subject<void> = new Subject();

  constructor(private store: Store<AppState>,
  ) { }

  ngOnInit() {
    this.store.select(state => state.currentUserState.currentUser)
      .pipe(
        filter<User>((u): u is User => !!u),
        takeUntil(this.unsubscribe),
      ).subscribe(user => {
        this.user = user;
      }
    );
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }
}
