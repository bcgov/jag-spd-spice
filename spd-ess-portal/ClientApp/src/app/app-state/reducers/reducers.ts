import { MetaReducer, ActionReducer, ActionReducerMap } from '@ngrx/store';
import { AppState } from '../models/app-state';
import { environment } from '../../../environments/environment';
import * as currentUserStateReducer from './current-user-reducer';
import * as currentScreeningRequestStateReducer from './current-screening-request-reducer';
import * as ministryScreeningTypesReducer from './ministry-screening-types-reducer';
import * as screeningReasonsReducer from './screening-reasons-reducer';
import * as fileUploadsReducer from './file-uploads.reducer';

export const metaReducers: MetaReducer<AppState>[] = !environment.production
    ? [logger]
    : [];

export const reducers: ActionReducerMap<AppState> = {
    currentUserState: currentUserStateReducer.reducer,
    currentScreeningRequestState: currentScreeningRequestStateReducer.reducer,
    ministryScreeningTypesState: ministryScreeningTypesReducer.reducer,
    screeningReasonsState: screeningReasonsReducer.reducer,
    fileUploadsState: fileUploadsReducer.reducer,
};

export function logger(reducer: ActionReducer<AppState>): ActionReducer<AppState> {
    return function (state: AppState, action: any): AppState {
        return reducer(state, action);
    };
}
