import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

import { User } from '../models/user.model';

import { DataService } from './data.service';

@Injectable()
export class UserDataService extends DataService {
  constructor(private http: HttpClient) {
    super();
  }

  getCurrentUser() {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    const path = `${this.apiPath}/user/current`;
    return this.http.get<User>(path, {
      headers: headers
    }).pipe(catchError(this.handleError));
  }
}
