import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Timetable } from './timetable';
import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {

  getTimetable(id: string): Observable<Timetable> {
    return this.http.get<Timetable>(encodeURI(`${environment.apiUrl}/timetable/scheduleddepartures?stop=${id}`));
  }

  getArrivals(id: string): Observable<Timetable> {
    return this.http.get<Timetable>(encodeURI(`${environment.apiUrl}/timetable/arrivals?stop=${id}`));
  }

  getStops(search: string): Observable<Timetable> {
    return this.http.get<Timetable>(encodeURI(`${environment.apiUrl}/timetable/stops?search=${search}`));
  }

  constructor(private http: HttpClient) {}
}
