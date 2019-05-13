import { MetaReducer, ActionReducer, ActionReducerMap } from '@ngrx/store';
import { AppState } from '../models/app-state';
import { environment } from '../../../environments/environment';
import * as legalEnityReducer from './legal-entities-reducer';
import * as currentAccountReducer from './current-account-reducer';
import * as currentLegalEnityReducer from './current-legal-entity-reducer';
import * as applicationsStateReducer from './applications-reducer';
import * as currentApplicationStateReducer from './current-application-reducer';
import * as currentUserStateReducer from './current-user-reducer';
import * as currentScreeningRequestStateReducer from './current-screening-request-reducer';
import * as ministryScreeningTypesReducer from './ministry-screening-types-reducer';
import * as screeningReasonsReducer from './screening-reasons-reducer';

export const metaReducers: MetaReducer<AppState>[] = !environment.production
    ? [logger]
    : [];

export const reducers: ActionReducerMap<AppState> = {
    legalEntitiesState: legalEnityReducer.reducer,
    currentLegalEntityState: currentLegalEnityReducer.reducer,
    currentAccountState: currentAccountReducer.reducer,
    applicationsState:  applicationsStateReducer.reducer,
    currentApplicaitonState: currentApplicationStateReducer.reducer,
    currentUserState: currentUserStateReducer.reducer,
    currentScreeningRequestState: currentScreeningRequestStateReducer.reducer,
    ministryScreeningTypesState: ministryScreeningTypesReducer.reducer,
    screeningReasonsState: screeningReasonsReducer.reducer
};

export function logger(reducer: ActionReducer<AppState>): ActionReducer<AppState> {
    return function (state: AppState, action: any): AppState {
        //console.log('state', state);
        //console.log('action', action);
        return reducer(state, action);
    };
}
