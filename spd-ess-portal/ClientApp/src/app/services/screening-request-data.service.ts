import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Ministry } from '../models/ministry.model';
import { ScreeningReason } from '../models/screening-reason.model';

import { DataService } from './data.service';

@Injectable()
export class ScreeningRequestDataService extends DataService {

  headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) {
    super();
  }

  /**
   * Get screening types for all ministries
   */
  getMinistryScreeningTypes(): Observable<Ministry[]> {
    const path = `${this.apiPath}/screeningRequest/ministryScreeningTypes`;
    return this.http.get<Ministry[]>(path, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  /**
   * Get screening reasons
   */
  getScreeningReasons(): Observable<ScreeningReason[]> {
    const path = `${this.apiPath}/screeningRequest/screeningReasons`;
    return this.http.get<ScreeningReason[]>(path, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  /**
   * Submit a screening request to Dynamics
   * @param data - screening request data
   */
  createScreeningRequest(data: any) {
    const path = `${this.apiPath}/screeningRequest/`;
    return this.http.post<any>(path, data, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  /**
   * Upload a document associated with a screening request
   * @param requestId - id of the screening request
   * @param file - document
   */
  uploadDocument(requestId: any, file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);
    const headers: HttpHeaders = new HttpHeaders();

    const path = `${this.apiPath}/file/upload/${requestId}`;
    return this.http.post<any>(path, formData, { headers: headers })
      .pipe(catchError(this.handleError));
  }
}
