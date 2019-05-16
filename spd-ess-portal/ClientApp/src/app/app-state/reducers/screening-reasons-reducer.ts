import * as ScreeningReasonsActions from '../actions/screening-reasons.action';
import { ScreeningReasonsState } from '../models/app-state';

// Section 1
const initialState: ScreeningReasonsState = { screeningReasons: null };

// Section 2
export function reducer(state: ScreeningReasonsState = initialState, action: ScreeningReasonsActions.Actions): ScreeningReasonsState {

    // Section 3
    switch (action.type) {
        case ScreeningReasonsActions.SET_SCREENING_REASONS:
            return { screeningReasons: action.payload };
        default:
            return state;
    }
}
