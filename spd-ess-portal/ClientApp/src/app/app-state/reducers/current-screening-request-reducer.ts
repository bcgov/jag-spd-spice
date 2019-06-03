import * as CurrentScreeningRequestActions from '../actions/current-screening-request.action';

import { CurrentScreeningRequestState } from '../models/app-state';

// Section 1
const initialState: CurrentScreeningRequestState = { currentScreeningRequest: null };

// Section 2
export function reducer(state: CurrentScreeningRequestState = initialState, action: CurrentScreeningRequestActions.Actions): CurrentScreeningRequestState {

    // Section 3
    switch (action.type) {
        case CurrentScreeningRequestActions.SET_CURRENT_SCREENING_REQUEST:
            return { currentScreeningRequest: action.payload };
        case CurrentScreeningRequestActions.CLEAR_CURRENT_SCREENING_REQUEST:
            return { currentScreeningRequest: null };
        default:
            return state;
    }
}
