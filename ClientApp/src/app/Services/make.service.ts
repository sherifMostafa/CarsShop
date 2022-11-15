import { KeyValuePair } from './../Models/KeyValuePairModel';
import { environment } from './../../environments/environment';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class MakeService {
  urlPart = environment.apiBaseUrl + 'api/Makes';

  constructor(private http: HttpClient) {}

  getMakes(): Observable<any[]> {
    return this.http.get<any[]>(this.urlPart);
  }
}
