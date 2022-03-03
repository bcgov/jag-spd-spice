import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subject, Subscription, combineLatest } from 'rxjs';
import { filter, take, takeUntil } from 'rxjs/operators';
import * as moment from 'moment';
import { Moment } from 'moment';

import * as CurrentScreeningRequestActions from '../app-state/actions/current-screening-request.action';
import * as FileUploadsActions from '../app-state/actions/file-uploads.action';
import { AppState } from '../app-state/models/app-state';

import { Ministry } from '../models/ministry.model';
import { ScreeningReason } from '../models/screening-reason.model';
import { ScreeningRequest } from '../models/screening-request.model';
import { FormBase } from '../shared/form-base';
import { FileUploaderComponent } from '../shared/file-uploader/file-uploader.component';
import { StrictMomentDateAdapter } from '../shared/strict-moment-date-adapter/strict-moment-date-adapter';

// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
export const MY_FORMATS = {
  parse: {
    dateInput: 'YYYY-MM-DD',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'YYYY-MM-DD',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-screening-request-form',
  templateUrl: './screening-request-form.component.html',
  styleUrls: ['./screening-request-form.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: StrictMomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class ScreeningRequestFormComponent extends FormBase implements OnInit, OnDestroy {
  @ViewChild('documentUploader') documentUploader: FileUploaderComponent;
  form: FormGroup;
  minDate: Moment;
  maxDate: Moment;
  startDate: Moment;
  ministryScreeningTypes: Ministry[];
  screeningReasons: ScreeningReason[];
  otherScreeningReasonValue: string;
  existingScreeningRequestSubscription: Subscription;

  unsubscribe: Subject<void> = new Subject();
  fileUploaderId = 'screeningRequestFiles';
  loaded = false;

  constructor(private store: Store<AppState>,
    private router: Router,
    private fb: FormBuilder,
  ) {
    super();
  }

  ngOnInit() {
    this.minDate = moment().startOf('day').subtract(100, 'years');
    this.maxDate = moment().startOf('day').subtract(10, 'years');
    this.startDate = moment().startOf('day').subtract(18, 'years');

    this.form = this.fb.group({
      clientMinistry: ['', Validators.required],
      programArea: ['', Validators.required],
      screeningType: ['', Validators.required],
      reason: ['', Validators.required],
      otherReason: [''],
      candidate: this.fb.group({
        firstName: ['', Validators.required],
        middleName: [''],
        lastName: ['', Validators.required],
        dateOfBirth: ['', [Validators.required, this.dateRangeValidator(this.minDate, this.maxDate)]],
        email: ['', [Validators.required, Validators.email]],
        position: ['', Validators.required],
      }),
      contact: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email, this.notEqualFieldValidator('candidate.email')]],
      }),
      photoIdConfirmation: [false, Validators.requiredTrue],
    });

    this.setOtherReasonValidator();

    // retrieve dropdown data from store
    combineLatest(
      this.store.select(state => state.ministryScreeningTypesState.ministryScreeningTypes)
        .pipe(filter<Ministry[]>((m): m is Ministry[] => !!m)),
      this.store.select(state => state.screeningReasonsState.screeningReasons)
        .pipe(filter<ScreeningReason[]>((r): r is ScreeningReason[] => !!r)),
    ).pipe(
      takeUntil(this.unsubscribe),
    ).subscribe(([ ministryScreeningTypes, screeningReasons ]) => {
      this.ministryScreeningTypes = ministryScreeningTypes;
      this.screeningReasons = screeningReasons;

      const otherScreeningReason = screeningReasons.find(r => r.name === 'Other');
      this.otherScreeningReasonValue = otherScreeningReason ? otherScreeningReason.value : '';

      // initialize dropdown selections
      const clientMinistryControl = this.form.get('clientMinistry');
      if (this.ministryScreeningTypes.length > 0 && clientMinistryControl) {
        clientMinistryControl.setValue(this.ministryScreeningTypes[0].value);
        this.loaded = true;
      } else {
        this.router.navigate(['/access-denied'], { skipLocationChange: true });
      }
    });

    // if there is an existing screening request in the store, retrieve it so it can be edited
    this.existingScreeningRequestSubscription = this.store.select(state => state.currentScreeningRequestState.currentScreeningRequest)
      .pipe(
        filter<ScreeningRequest>((r): r is ScreeningRequest => !!r),
        takeUntil(this.unsubscribe),
      ).subscribe(request => {
        const { files, ...formValues } = request;
        this.form.setValue(formValues);
        this.store.dispatch(new FileUploadsActions.SetFileUploadsAction({ id: this.fileUploaderId, files: files }));
      }
    );
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  setOtherReasonValidator() {
    const reasonControl = this.form.get('reason');
    const otherReasonControl = this.form.get('otherReason');

    if (reasonControl && otherReasonControl) {
      reasonControl.valueChanges.subscribe(reason => {
        if (reason === this.otherScreeningReasonValue) {
          otherReasonControl.setValidators([Validators.required]);
        } else {
          otherReasonControl.setValidators(null);
        }

        otherReasonControl.updateValueAndValidity();
      });
    }
  }

  getCandidateDateOfBirthValidity() {
    return this.getCandidateDateOfBirthErrorMessage() === '';
  }

  getCandidateDateOfBirthErrorMessage() {
    const control = this.form.get('candidate.dateOfBirth');

    if (!control || control.valid || !control.touched || !control.errors) {
      return '';
    } else if (control.errors.required) {
      return 'Please provide the candidate\'s date of birth in the format yyyy-mm-dd';
    } else if (control.errors.beforeStart) {
      return 'Candidate\'s date of birth must be within the last 100 years';
    } else if (control.errors.afterEnd) {
      return 'Candidate\'s date of birth must be at least 10 years ago';
    } else {
      return '';
    }
  }

  getContactEmailErrorMessage() {
    const control = this.form.get('contact.email');

    if (!control || control.valid || !control.touched || !control.errors) {
      return '';
    } else if (control.errors.required || control.errors.email) {
      return 'Email address must be provided in a valid format';
    } else if (control.errors.equal) {
      return 'Email address cannot be the same as the candidate email address';
    } else {
      return '';
    }
  }

  getProgramAreas() {
    const clientMinistryControl = this.form.get('clientMinistry');
    if (!clientMinistryControl) {
      return [];
    }

    const ministry = this.ministryScreeningTypes.find(m => m.value === clientMinistryControl.value);
    return ministry ? ministry.programAreas : [];
  }

  getScreeningTypes() {
    const programAreaControl = this.form.get('programArea');
    if (!programAreaControl) {
      return [];
    }

    const programArea = this.getProgramAreas().find(m => m.value === programAreaControl.value);
    return programArea ? programArea.screeningTypes : [];
  }

  gotoReview() {
    if (this.form.valid) {
      this.store.select(state => state.fileUploadsState.fileUploads).pipe(take(1)).subscribe(fileUploads => {
        const fileUploadSet = fileUploads.find(f => f.id === this.fileUploaderId);

        const value = <ScreeningRequest>{
          ...this.form.getRawValue(),
          files: fileUploadSet ? fileUploadSet.files : [],
        };

        this.existingScreeningRequestSubscription.unsubscribe();

        this.store.dispatch(new CurrentScreeningRequestActions.SetCurrentScreeningRequestAction(value));
        this.store.dispatch(new FileUploadsActions.ClearFileUploadsAction(this.fileUploaderId));

        this.router.navigate(['/review-submission'], { skipLocationChange: true });
      });
    } else {
      this.markAsTouched();
    }
  }

  // marking the form as touched makes the validation messages show
  markAsTouched() {
    this.form.markAsTouched();

    const screeningRequestControls = this.form.controls;
    for (const c in screeningRequestControls) {
      if (typeof (screeningRequestControls[c].markAsTouched) === 'function') {
        screeningRequestControls[c].markAsTouched();
      }
    }
  }

  onMinistryChange() {
    const programAreaControl = this.form.get('programArea');
    if (programAreaControl) {
      programAreaControl.setValue('');
    }

    const screeningTypeControl = this.form.get('screeningType');
    if (screeningTypeControl) {
      screeningTypeControl.setValue('');
    }
  }

  onProgramAreaChange() {
    const screeningTypeControl = this.form.get('screeningType');
    if (screeningTypeControl) {
      screeningTypeControl.setValue('');
    }
  }

  onReasonChange() {
    const reasonControl = this.form.get('reason');
    const otherReasonControl = this.form.get('otherReason');
    if (reasonControl && otherReasonControl && reasonControl.value !== this.otherScreeningReasonValue) {
      otherReasonControl.setValue('');
    }
  }

  onCandidateEmailChange() {
    const contactEmailControl = this.form.get('contact.email');
    if (contactEmailControl) {
      contactEmailControl.updateValueAndValidity();
    }
  }
}
