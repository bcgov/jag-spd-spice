import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { ScreeningRequest } from '../models/screening-request.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
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
   * Submit a Screening Request to Dynamics
   * @param data - screening request data
   */
  createScreeningRequest(data: any) {
    return this.http.post<ScreeningRequest>('api/ScreeningRequest/', data, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }
}
