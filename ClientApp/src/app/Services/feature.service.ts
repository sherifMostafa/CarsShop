import { KeyValuePair } from '../Models/KeyValuePairModel';
import { environment } from '../../environments/environment';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class FeatureSerice {
  urlPart = environment.apiBaseUrl + 'api/Features';

  constructor(private http: HttpClient) {}

  getFeatures(): Observable<any[]> {
    return this.http.get<any[]>(this.urlPart);
  }
}
