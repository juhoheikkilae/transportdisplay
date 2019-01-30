import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Conditions } from '../models/weather';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  getConditions(id: string): Observable<Conditions> {
    const response = this.http.get<Conditions>(encodeURI(`${environment.apiUrl}/weather/conditions?id=${id}`));
    return response;
  }

  constructor(private http: HttpClient) { }
}
