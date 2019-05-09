
import { filter } from 'rxjs/operators';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AppState } from '../app-state/models/app-state';
import { Store } from '@ngrx/store';
import { FormBuilder, FormGroup, Validators, FormArray, ValidatorFn, AbstractControl, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ScreeningRequest } from '../models/screening-request.model';
import * as CurrentScreeningRequestActions from '../app-state/actions/current-screening-request.action';
import { ScreeningRequestDataService } from '../services/screening-request-data.service';

import { User } from '../models/user.model';
import { UserDataService } from '../services/user-data.service';

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

  loaded = false;
  maxDate = new Date();

  constructor(private store: Store<AppState>,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private screeningRequestDataService: ScreeningRequestDataService,
    private userDataService: UserDataService,
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
    
    // initialize form values from store
    this.store.select(state => state.currentScreeningRequestState).pipe(
      filter(state => !!state))
      .subscribe(state => {
        if (state.currentScreeningRequest) {
          this.form.setValue(state.currentScreeningRequest);
        }
      });

    this.getCurrentUser();
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

  getCurrentUser() {
    this.userDataService.getCurrentUser()
      .subscribe((data: User) => {
        this.currentUser = data;
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
