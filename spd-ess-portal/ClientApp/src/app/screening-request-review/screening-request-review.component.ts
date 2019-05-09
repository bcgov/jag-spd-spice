
import { filter } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { AppState } from '../app-state/models/app-state';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

import { ScreeningRequest } from '../models/screening-request.model';
import * as CurrentScreeningRequestActions from '../app-state/actions/current-screening-request.action';
import { ScreeningRequestDataService } from '../services/screening-request-data.service';

import { FormBase } from '../shared/form-base';

@Component({
  selector: 'app-screening-request-review',
  templateUrl: './screening-request-review.component.html',
  styleUrls: ['./screening-request-review.component.scss']
})
export class ScreeningRequestReviewComponent extends FormBase implements OnInit {
  screeningRequest: ScreeningRequest;
  valid = false;

  constructor(private store: Store<AppState>,
    private router: Router,
    private route: ActivatedRoute,
    private screeningRequestDataService: ScreeningRequestDataService,
  ) {
    super();
  }

  ngOnInit() {
    this.store.select(state => state.currentScreeningRequestState).pipe(
    filter(state => !!state))
    .subscribe(state => {
      this.screeningRequest = state.currentScreeningRequest || new ScreeningRequest();
      this.valid = Boolean(state.currentScreeningRequest);
    });
  }

  save(): Subject<boolean> {
    const subResult = new Subject<boolean>();

    this.screeningRequestDataService.createScreeningRequest(this.screeningRequest).subscribe(
      res => {
        this.store.dispatch(new CurrentScreeningRequestActions.ClearCurrentScreeningRequestAction());
        subResult.next(true);
      },
      err => {
        subResult.next(false);
      });

    return subResult;
  }

  gotoForm() {
    this.router.navigate(['/']);
  }

  gotoSubmit() {
    this.save().subscribe(data => {
      // TODO: stay on page and display error message when unsuccessful
      this.router.navigate(['/request-submitted']);
    });
  }
}
