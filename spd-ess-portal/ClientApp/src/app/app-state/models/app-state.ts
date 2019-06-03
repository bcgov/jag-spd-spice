import { User } from '../../models/user.model';
import { ScreeningRequest } from '../../models/screening-request.model';
import { Ministry } from '../../models/ministry.model';
import { ScreeningReason } from '../../models/screening-reason.model';
import { FileUploadSet } from '../../models/file-upload-set.model';

export interface AppState {
    currentUserState: CurrentUserState;
    currentScreeningRequestState: CurrentScreeningRequestState;
    ministryScreeningTypesState: MinistryScreeningTypesState;
    screeningReasonsState: ScreeningReasonsState;
    fileUploadsState: FileUploadsState;
}

export interface CurrentUserState {
    currentUser: User;
}

export interface CurrentScreeningRequestState {
    currentScreeningRequest: ScreeningRequest;
}

export interface MinistryScreeningTypesState {
    ministryScreeningTypes: Ministry[];
}

export interface ScreeningReasonsState {
    screeningReasons: ScreeningReason[];
}

export interface FileUploadsState {
    fileUploads: FileUploadSet[];
}
