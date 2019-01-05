import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Timetable } from './timetable';
import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {

  getTimetable(id: string): Observable<Timetable> {
    const response = this.http.get<Timetable>(encodeURI(environment.apiUrl + '/timetable/scheduleddepartures?stop=' + id));
    return response;
  }

  getArrivals(id: string): Observable<Timetable> {
    const response = this.http.get<Timetable>(encodeURI(environment.apiUrl + '/timetable/scheduleddepartures?stop=' + id));
    return response;
  }

  getStops(search: string): Observable<Timetable> {
    const response = this.http.get<Timetable>(encodeURI(environment.apiUrl + '/timetable/stops?search=' + search));
    return response;
  }

  constructor(private http: HttpClient) {}
}
