import { Action } from '@ngrx/store';
import { Ministry } from '../../models/ministry.model';

export const SET_MINISTRY_SCREENING_TYPES = 'SET_MINISTRY_SCREENING_TYPES';

export class SetMinistryScreeningTypesAction implements Action {
  readonly type = SET_MINISTRY_SCREENING_TYPES;

  constructor(public payload: Ministry[]) { }
}

export type Actions =
SetMinistryScreeningTypesAction;
