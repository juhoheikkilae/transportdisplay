import { Component, OnInit, Input } from '@angular/core';
import { WeatherService } from '../../services/weather.service';
import { Conditions } from '../../models/weather';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.less']
})
export class WeatherComponent implements OnInit {

  @Input()
  locationId: string;

  conditions: Conditions;

  getConditions(locationId: string): void {
    this.weatherService.getConditions(locationId)
      .subscribe(conditions => this.conditions = conditions);
  }

  constructor(private weatherService: WeatherService) { }

  ngOnInit() {
    this.getConditions(this.locationId);
  }

}
