import { KeyValuePair } from '../Models/KeyValuePairModel';
import { environment } from '../../environments/environment';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ModelService {
  urlPart = environment.apiBaseUrl + 'api/Models';

  constructor(private http: HttpClient) {}

  getModels(): Observable<any[]> {
    return this.http.get<any[]>(this.urlPart);
  }
}
