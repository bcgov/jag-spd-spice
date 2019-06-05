
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Store } from '@ngrx/store';
import { Subject, Subscription, forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';

import * as CurrentScreeningRequestActions from '../app-state/actions/current-screening-request.action';
import { AppState } from '../app-state/models/app-state';

import { ScreeningRequest } from '../models/screening-request.model';
import { ScreeningRequestDataService } from '../services/screening-request-data.service';
import { FormBase } from '../shared/form-base';

@Component({
  selector: 'app-screening-request-review',
  templateUrl: './screening-request-review.component.html',
  styleUrls: ['./screening-request-review.component.scss']
})
export class ScreeningRequestReviewComponent extends FormBase implements OnInit {
  screeningRequest: ScreeningRequest = new ScreeningRequest();
  submittingForm: Subscription;
  uploadingDocuments: Subscription;
  submissionResult: Subject<boolean>;

  valid = false;

  constructor(private store: Store<AppState>,
    private router: Router,
    private screeningRequestDataService: ScreeningRequestDataService,
    private _snackBar: MatSnackBar,
  ) {
    super();
  }

  ngOnInit() {
    this.store.select(state => state).pipe(
      filter(state => !!state))
      .subscribe(state => {
        if (state.currentScreeningRequestState.currentScreeningRequest) {
          // retrieve screening request from store
          this.screeningRequest = state.currentScreeningRequestState.currentScreeningRequest;
        } else {
          // when there is no screening request in the store
          // (because this page has been refreshed or accessed directly via /review-submission)
          // redirect to the screening request form page
          this.router.navigate(['/'], { replaceUrl: true });
        }
        this.valid = Boolean(state.currentScreeningRequestState.currentScreeningRequest);
      });
  }

  save(): Subject<boolean> {
    this.submissionResult = new Subject<boolean>();

    this.submittingForm = this.screeningRequestDataService.createScreeningRequest(this.screeningRequest).subscribe(
      result => {
        if (result.requestId) {
          this.uploadDocuments(result.requestId);
        } else {
          this.submissionResult.error(new Error('requestId '));
        }
      },
      err => this.submissionResult.error(err));

    return this.submissionResult;
  }

  uploadDocuments(screeningRequestId: number) {
    this.uploadingDocuments = forkJoin(
      this.screeningRequest.files.map(f => this.screeningRequestDataService.uploadDocument(screeningRequestId, f.file))
    ).subscribe(
      undefined,
      (err: any) => this.submissionResult.error(err),
      () => this.submissionResult.next(true)
    );
  }

  onBusyStop() {
    this.submissionResult.complete();
  }

  gotoForm() {
    this.router.navigate(['/']);
  }

  gotoSubmit() {
    this.save().subscribe(
      undefined,
      (err: any) => {
        console.error(err);

        const ref = this._snackBar.open('Form Submission Failed', 'RETRY', {
          duration: 10000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          panelClass: 'snackbar-error'
        });

        ref.onAction().subscribe(() => {
          this.gotoSubmit();
        });
      },
      () => {
        this.store.dispatch(new CurrentScreeningRequestActions.ClearCurrentScreeningRequestAction());
        this.router.navigate(['/request-submitted']);
      }
    );
  }
}
