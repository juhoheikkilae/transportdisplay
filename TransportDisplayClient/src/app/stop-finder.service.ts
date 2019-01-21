import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Stop } from './timetable';
import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StopFinderService {

  getStops(search: string): Observable<Stop[]> {
    if (search !== undefined && search.length > 0) {
      return this.http.get<Stop[]>(encodeURI(`${environment.apiUrl}/timetable/stops?search=${search}`));
    } else {
      const response: Stop[] = [];
      return of(response);
    }
  }

  constructor(private http: HttpClient) { }
}
