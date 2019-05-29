import { Injectable } from '@angular/core';
import { Ministry } from '../models/ministry.model';
import { ScreeningReason } from '../models/screening-reason.model';
import { ScreeningRequest } from '../models/screening-request.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
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
   * Submit a screening request to Dynamics
   * @param data - screening request data
   */
  createScreeningRequest(data: any) {
    return this.http.post<any>('api/ScreeningRequest/', data, { headers: this.headers })
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

    return this.http.post(`api/file/upload/${requestId}`, formData, { headers: headers })
      .pipe(catchError(this.handleError));
  }

  /**
   * Get screening types for all ministries
   */
  getMinistryScreeningTypes(): Observable<Ministry[]> {
    const apiPath = 'api/screeningRequest/ministryScreeningTypes';
    return this.http.get<Ministry[]>(apiPath, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  /**
   * Get screening reasons
   */
  getScreeningReasons(): Observable<ScreeningReason[]> {
    const apiPath = 'api/screeningRequest/screeningReasons';
    return this.http.get<ScreeningReason[]>(apiPath, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }
}
