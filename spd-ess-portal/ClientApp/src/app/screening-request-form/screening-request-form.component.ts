
import { filter } from 'rxjs/operators';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AppState } from '../app-state/models/app-state';
import { Store } from '@ngrx/store';
import { FormBuilder, FormGroup, Validators, FormArray, ValidatorFn, AbstractControl, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ScreeningRequest } from '../models/screening-request.model';
import * as CurrentScreeningRequestActions from '../app-state/actions/current-screening-request.action';

import { User } from '../models/user.model';
import { Ministry } from '../models/ministry.model';
import { ProgramArea } from '../models/program-area.model';
import { ScreeningType } from '../models/screening-type.model';
import { ScreeningReason } from '../models/screening-reason.model';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { FormBase } from '../shared/form-base';
const moment = _rollupMoment || _moment;

// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
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
  styleUrls: ['./screening-request-form.component.scss']
})
export class ScreeningRequestFormComponent extends FormBase implements OnInit {
  currentScreeningRequest: ScreeningRequest; // TODO: remove from here, add to confirm page
  form: FormGroup;
  currentUser: User;
  ministryScreeningTypes: Ministry[];
  screeningReasons: ScreeningReason[];

  loaded = false;
  maxDate = new Date();

  constructor(private store: Store<AppState>,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
  ) {
    super();
  }

  ngOnInit() {
    this.form = this.fb.group({
      clientMinistry: ['', Validators.required],
      programArea: ['', Validators.required],
      screeningType: ['', Validators.required],
      reason: ['', Validators.required],
      otherReason: [''],
      candidateFirstName: ['', Validators.required],
      candidateMiddleName: [''],
      candidateLastName: ['', Validators.required],
      candidateDateOfBirth: ['', Validators.required],
      candidateEmail: ['', [Validators.required, Validators.email]],
      candidatePosition: ['', Validators.required],
      contactName: ['', Validators.required],
      contactEmail: ['', [Validators.required, Validators.email]],
      photoIdConfirmation: [false, Validators.requiredTrue],
    });

    this.setOtherReasonValidator();

    this.store.select(state => state).pipe(
      filter(state => !!state))
      .subscribe(state => {
        // retrieve current user from store
        this.currentUser = state.currentUserState.currentUser;

        // retrieve dropdown data from store
        this.ministryScreeningTypes = state.ministryScreeningTypesState.ministryScreeningTypes;
        this.screeningReasons = state.screeningReasonsState.screeningReasons;

        // initialize form values from store
        if (state.currentScreeningRequestState.currentScreeningRequest) {
          this.form.setValue(state.currentScreeningRequestState.currentScreeningRequest);
        }

        if (this.currentUser && this.ministryScreeningTypes && this.screeningReasons) {
          this.loaded = true;
        }
      });
  }

  setOtherReasonValidator() {
    const otherReasonControl = this.form.get('otherReason');

    this.form.get('reason').valueChanges
      .subscribe(reason => {
        if (reason === 'other') {
          otherReasonControl.setValidators([Validators.required]);
        } else {
          otherReasonControl.setValidators(null);
        }

        otherReasonControl.updateValueAndValidity();
      });
  }

  gotoReview() {
    if (this.form.valid) {
      const value = <ScreeningRequest>{
        ...this.form.value
      };
      this.store.dispatch(new CurrentScreeningRequestActions.SetCurrentScreeningRequestAction(value));

      this.router.navigate(['/review-submission']);
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
}
