import { Component, OnInit, Input } from '@angular/core';
import { Timetable, DisplayType } from '../../models/timetable';
import { TimetableService } from '../../services/timetable.service';

@Component({
  selector: 'app-timetable',
  templateUrl: './timetable.component.html',
  styleUrls: ['./timetable.component.less']
})
export class TimetableComponent implements OnInit {

  @Input()
  stopId: string;

  @Input()
  type: DisplayType;

  timetable: Timetable;

  onTypeChange(): void {
    if (this.type === DisplayType.DEPARTURES) {
      this.getData(this.stopId);
    } else if (this.type === DisplayType.ARRIVALS) {
      this.getData(this.stopId);
    }
  }

  getData(stopId: string): void {
    if (this.type === DisplayType.DEPARTURES) {
      this.timetableService.getTimetable(stopId)
        .subscribe(timetable => {
          this.timetable = timetable;
          this.timetable.displayType = DisplayType.DEPARTURES;
        });
    } else if (this.type === DisplayType.ARRIVALS) {
      this.timetableService.getArrivals(stopId)
        .subscribe(timetable => {
          this.timetable = timetable;
          this.timetable.displayType = DisplayType.ARRIVALS;
        });
    }
  }

  constructor(private timetableService: TimetableService) { }

  ngOnInit() {
    this.getData(this.stopId);
  }

}
