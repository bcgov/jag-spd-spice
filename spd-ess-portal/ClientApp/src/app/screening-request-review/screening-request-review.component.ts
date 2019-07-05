
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Store } from '@ngrx/store';
import { Observable, Subject, Subscription, combineLatest, forkJoin, throwError } from 'rxjs';
import { filter, flatMap, takeUntil } from 'rxjs/operators';

import * as CurrentScreeningRequestActions from '../app-state/actions/current-screening-request.action';
import { AppState } from '../app-state/models/app-state';

import { Ministry } from '../models/ministry.model';
import { ScreeningReason } from '../models/screening-reason.model';
import { ScreeningRequest } from '../models/screening-request.model';
import { ScreeningRequestDataService } from '../services/screening-request-data.service';
import { FormBase } from '../shared/form-base';

@Component({
  selector: 'app-screening-request-review',
  templateUrl: './screening-request-review.component.html',
  styleUrls: ['./screening-request-review.component.scss']
})
export class ScreeningRequestReviewComponent extends FormBase implements OnInit, OnDestroy {
  screeningRequest: ScreeningRequest = new ScreeningRequest();
  submittingForm: Subscription;
  submissionResult: Subject<boolean>;

  unsubscribe: Subject<void> = new Subject();
  valid = false;
  clientMinistryName: string | null = null;
  programAreaName: string | null = null;
  screeningTypeName: string | null = null;
  screeningReasonName: string | null = null;

  constructor(private store: Store<AppState>,
    private router: Router,
    private screeningRequestDataService: ScreeningRequestDataService,
    private _snackBar: MatSnackBar,
  ) {
    super();
  }

  ngOnInit() {
    this.store.select(state => state.currentScreeningRequestState.currentScreeningRequest).pipe(
      takeUntil(this.unsubscribe),
    ).subscribe(currentScreeningRequest => {
        if (currentScreeningRequest) {
          // retrieve screening request from store
          this.screeningRequest = currentScreeningRequest;
        } else {
          // when there is no screening request in the store
          // (because this page has been refreshed or accessed directly via /review-submission)
          // redirect to the screening request form page
          this.router.navigate(['/'], { replaceUrl: true });
        }
        this.valid = Boolean(currentScreeningRequest);
      });

    // retrieve names for dropdown values
    combineLatest(
      this.store.select(state => state.ministryScreeningTypesState.ministryScreeningTypes)
        .pipe(filter<Ministry[]>((m): m is Ministry[] => !!m)),
      this.store.select(state => state.screeningReasonsState.screeningReasons)
        .pipe(filter<ScreeningReason[]>((r): r is ScreeningReason[] => !!r)),
    ).pipe(
      takeUntil(this.unsubscribe),
    ).subscribe(([ ministryScreeningTypes, screeningReasons ]) => {
      const clientMinistry = ministryScreeningTypes.find(m => m.value === this.screeningRequest.clientMinistry);

      if (clientMinistry) {
        this.clientMinistryName = clientMinistry.name;
        const programArea = clientMinistry.programAreas.find(a => a.value === this.screeningRequest.programArea);

        if (programArea) {
          this.programAreaName = programArea.name;
          const screeningType = programArea.screeningTypes.find(t => t.value === this.screeningRequest.screeningType);

          if (screeningType) {
            this.screeningTypeName = screeningType.name;
          }
        }
      }

      const screeningReason = screeningReasons.find(r => r.value === this.screeningRequest.reason);

      if (screeningReason) {
        this.screeningReasonName = screeningReason.name;
      }
    });
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  save(): Subject<boolean> {
    this.submissionResult = new Subject<boolean>();

    this.submittingForm = this.screeningRequestDataService.createScreeningRequest(this.screeningRequest).pipe(
      flatMap(result => {
        if (result.screeningId) {
          return this.uploadDocuments(result.screeningId);
        } else {
          return throwError('The screening request was submitted, but no screeningId was returned');
        }
      })
    ).subscribe(
      undefined,
      err => this.submissionResult.error(err)
    );

    return this.submissionResult;
  }

  uploadDocuments(screeningId: number): Observable<any> {
    return forkJoin(
      this.screeningRequest.files.map(f => this.screeningRequestDataService.uploadDocument(screeningId, f.file))
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
