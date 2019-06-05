import { Action } from '@ngrx/store';
import { ScreeningRequest } from '../../models/screening-request.model';

export const SET_CURRENT_SCREENING_REQUEST = 'SET_CURRENT_SCREENING_REQUEST';
export const CLEAR_CURRENT_SCREENING_REQUEST = 'CLEAR_CURRENT_SCREENING_REQUEST';

export class SetCurrentScreeningRequestAction implements Action {
  readonly type = SET_CURRENT_SCREENING_REQUEST;

  constructor(public payload: ScreeningRequest) { }
}

export class ClearCurrentScreeningRequestAction implements Action {
  readonly type = CLEAR_CURRENT_SCREENING_REQUEST;
}

export type Actions =
SetCurrentScreeningRequestAction
| ClearCurrentScreeningRequestAction;
