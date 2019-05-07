import { Action } from '@ngrx/store';
import { ScreeningRequest } from '../../models/screening-request.model';

export const CURRENT_SCREENING_REQUEST = 'CURRENT_SCREENING_REQUEST';
export const SET_CURRENT_SCREENING_REQUEST = 'SET_CURRENT_SCREENING_REQUEST';

export class CurrentScreeningRequestAction implements Action {
  readonly type = CURRENT_SCREENING_REQUEST;
}

export class SetCurrentScreeningRequestAction implements Action {
  readonly type = SET_CURRENT_SCREENING_REQUEST;

  constructor(public payload: ScreeningRequest) {}
}

export type Actions =
CurrentScreeningRequestAction
| SetCurrentScreeningRequestAction;
