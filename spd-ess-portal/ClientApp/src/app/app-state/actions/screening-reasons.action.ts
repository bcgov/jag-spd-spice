import { Action } from '@ngrx/store';
import { ScreeningReason } from '../../models/screening-reason.model';

export const SET_SCREENING_REASONS = 'SET_SCREENING_REASONS';

export class SetScreeningReasonsAction implements Action {
  readonly type = SET_SCREENING_REASONS;

  constructor(public payload: ScreeningReason[]) { }
}

export type Actions =
SetScreeningReasonsAction;
