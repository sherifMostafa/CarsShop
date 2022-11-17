import { KeyValuePair } from './../Models/KeyValuePairModel';
import { environment } from './../../environments/environment';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VehicleModel } from '../Models/VehicleModel';

@Injectable()
export class VehicleService {
  urlPart = environment.apiBaseUrl + 'api/Vehicle';
  httpOptions: any;
  constructor(private http: HttpClient) {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'my-auth-token',
      }),
    };
  }

  post(model: VehicleModel): Observable<any> {
    console.log(model);
    return this.http.post<any>(this.urlPart, model, this.httpOptions);
  }

  get(): Observable<any[]> {
    return this.http.get<any[]>(this.urlPart);
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(this.urlPart + `/${id}`);
  }
}
