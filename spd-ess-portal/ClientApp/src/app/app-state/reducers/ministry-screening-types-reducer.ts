import * as MinistryScreeningTypesActions from '../actions/ministry-screening-types.action';
import { MinistryScreeningTypesState } from '../models/app-state';

// Section 1
const initialState: MinistryScreeningTypesState = { ministryScreeningTypes: null };

// Section 2
export function reducer(state: MinistryScreeningTypesState = initialState, action: MinistryScreeningTypesActions.Actions): MinistryScreeningTypesState {

    // Section 3
    switch (action.type) {
        case MinistryScreeningTypesActions.SET_MINISTRY_SCREENING_TYPES:
            return { ministryScreeningTypes: action.payload };
        default:
            return state;
    }
}
