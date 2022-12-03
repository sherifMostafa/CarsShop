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

  put(model: VehicleModel): Observable<any> {
    console.log(model);
    return this.http.put<any>(
      this.urlPart + `/${model.id}`,
      model,
      this.httpOptions
    );
  }

  // delete(id: number): Observable<any> {

  //   return this.http.put(this.urlPart + `/${id}`, null, this.httpOptions);
  // }

  get(filter: any): Observable<any[]> {
    return this.http.get<any[]>(
      this.urlPart + '?' + this.ToQueryString(filter)
    );
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(this.urlPart + `/${id}`);
  }

  ToQueryString(obj: any) {
    let parts = [];
    for (let property in obj) {
      let value = obj[property];
      if (value != null && value != undefined)
        parts.push(
          encodeURIComponent(property) + '=' + encodeURIComponent(value)
        );
    }
    return parts.join('&');
  }
}
