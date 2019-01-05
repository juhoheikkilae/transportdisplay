import { Component, OnInit } from '@angular/core';
import { Timetable } from '../timetable';
import { TimetableService } from '../timetable.service';

@Component({
  selector: 'app-timetable',
  templateUrl: './timetable.component.html',
  styleUrls: ['./timetable.component.less']
})
export class TimetableComponent implements OnInit {

  timetable: Timetable;
  arrivalEltimates: Timetable;
  stopId = 'HSL:2314601';

  getTimetable(stopId: string): void {
    this.timetableService.getTimetable(stopId)
      .subscribe(timetable => this.timetable = timetable);
  }

  getArrivalEstimates(stopId: string): void {
    this.timetableService.getArrivals(stopId)
      .subscribe(timetable => this.arrivalEltimates = timetable);
  }

  constructor(private timetableService: TimetableService) { }

  ngOnInit() {
    this.getTimetable(this.stopId);
  }

}
