import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DataService } from './data.service';
import { Configuration } from '@appmodels/configuration';

@Injectable()
export class ConfigService extends DataService {
    headers: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(private http: HttpClient) {
        super();
    }

    public async load(): Promise<Configuration> {
        const path = `${this.apiPath}/Configuration`;
        try {
            return await lastValueFrom(
                this.http.get<Configuration>(path, { headers: this.headers })
                    .pipe(catchError(this.handleError))
            );
        } catch (error) {
            this.handleError(error);
            throw error;
        }
    }
}
