import { KeyValuePair } from '../Models/KeyValuePairModel';
import { environment } from '../../environments/environment';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class PhotoService {
  urlPart = environment.apiBaseUrl + 'api/vehicles';

  constructor(private http: HttpClient) {}

  upload(vehicleId: number, file: File): Observable<any> {
    var formData = new FormData();
    formData.append('file', file);
    return this.http.post<any>(
      this.urlPart + `/${vehicleId}/photos`,
      formData,
      {
        reportProgress: true,
        observe: 'events',
      }
    );
  }

  getPhotos(vehicleId: number): Observable<any[]> {
    return this.http.get<any[]>(this.urlPart + `/${vehicleId}/photos`);
  }
}
