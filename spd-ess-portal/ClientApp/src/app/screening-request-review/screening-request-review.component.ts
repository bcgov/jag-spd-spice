
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

import { Ministry } from '../models/ministry.model';
import { ProgramArea } from '../models/program-area.model';
import { ScreeningType } from '../models/screening-type.model';
import { ScreeningReason } from '../models/screening-reason.model';

@Component({
  selector: 'app-screening-request-review',
  templateUrl: './screening-request-review.component.html',
  styleUrls: ['./screening-request-review.component.scss']
})
export class ScreeningRequestReviewComponent extends FormBase implements OnInit {
  screeningRequest: ScreeningRequest;
  ministryScreeningTypes: Ministry[];
  screeningReasons: ScreeningReason[];

  loaded = false;
  valid = false;

  constructor(private store: Store<AppState>,
    private router: Router,
    private route: ActivatedRoute,
    private screeningRequestDataService: ScreeningRequestDataService,
  ) {
    super();
  }

  ngOnInit() {
    this.store.select(state => state).pipe(
      filter(state => !!state))
      .subscribe(state => {
        // retrieve screening request from store
        this.screeningRequest = state.currentScreeningRequestState.currentScreeningRequest || new ScreeningRequest();
        this.valid = Boolean(state.currentScreeningRequestState.currentScreeningRequest);

        // retrieve dropdown data from store
        this.ministryScreeningTypes = state.ministryScreeningTypesState.ministryScreeningTypes;
        this.screeningReasons = state.screeningReasonsState.screeningReasons;

        if (this.ministryScreeningTypes && this.screeningReasons) {
          this.loaded = true;
        }
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

  getMinistryName() {
    const ministry = this.ministryScreeningTypes.filter(m => m.value === this.screeningRequest.clientMinistry);
    if (ministry.length === 1) {
      return ministry[0].name;
    }
    
    return '';
  }

  getProgramAreaName() {
    const ministry = this.ministryScreeningTypes.filter(m => m.value === this.screeningRequest.clientMinistry);
    if (ministry.length === 1) {
      const programArea = ministry[0].programAreas.filter(p => p.value === this.screeningRequest.programArea);
      if (programArea.length === 1) {
        return programArea[0].name;
      }
    }
    
    return '';
  }

  getScreeningTypeName() {
    const ministry = this.ministryScreeningTypes.filter(m => m.value === this.screeningRequest.clientMinistry);
    if (ministry.length === 1) {
      const programArea = ministry[0].programAreas.filter(p => p.value === this.screeningRequest.programArea);
      if (programArea.length === 1) {
        const screeningType = programArea[0].screeningTypes.filter(t => t.value === this.screeningRequest.screeningType);
        if (screeningType.length === 1)
        {
          return screeningType[0].name;
        }
      }
    }
    
    return '';
  }

  getScreeningReasonName() {
    const reason = this.screeningReasons.filter(m => m.value === this.screeningRequest.reason);
    if (reason.length === 1) {
      return reason[0].name;
    }
    
    return '';
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
