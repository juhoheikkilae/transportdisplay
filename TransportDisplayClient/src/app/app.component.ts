import { Component, OnInit } from '@angular/core';
import { DisplayType } from './timetable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'Placeholder title';
  config: AppConfig = {
    timetableConfigs: [
      {
        stopId: 'HSL:2314601',
        displayType: DisplayType.DEPARTURES
      },
      {
        stopId: 'HSL:2311220',
        displayType: DisplayType.ARRIVALS
      }
    ]
  };
}

class AppConfig {
  timetableConfigs: TimetableConfig[];
}

class TimetableConfig {
  stopId: string;
  displayType: DisplayType;
}
