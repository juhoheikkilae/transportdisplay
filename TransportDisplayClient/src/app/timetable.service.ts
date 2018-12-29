import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Timetable } from './timetable';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {

  private timetableUrl = 'https://localhost:5001/api/timetable?id=';

  getTimetable(id: string): Observable<Timetable> {
    return this.http.get<Timetable>(encodeURI(this.timetableUrl + id));
  }
  constructor(private http: HttpClient) {}
}
