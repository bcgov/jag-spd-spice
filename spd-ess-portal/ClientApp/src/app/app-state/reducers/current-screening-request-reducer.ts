import { Action } from '@ngrx/store';
import { ScreeningRequest } from '../../models/screening-request.model';
import * as CurrentScreeningRequestActions from '../actions/current-screening-request.action';
import { CurrentScreeningRequestState } from '../models/app-state';

// Section 1
const initialState: CurrentScreeningRequestState = { currentScreeningRequest: null };

// Section 2
export function reducer(state: CurrentScreeningRequestState = initialState, action: CurrentScreeningRequestActions.Actions): CurrentScreeningRequestState {

    // Section 3
    switch (action.type) {
        case CurrentScreeningRequestActions.CURRENT_SCREENING_REQUEST:
            return { ...state };
        case CurrentScreeningRequestActions.SET_CURRENT_SCREENING_REQUEST:
            return { currentScreeningRequest: action.payload };
        default:
            return state;
    }
}
