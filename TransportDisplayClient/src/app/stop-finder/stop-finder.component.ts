import { Component, OnInit } from '@angular/core';
import { StopFinderService } from '../stop-finder.service';
import { Stop } from '../timetable';

@Component({
  selector: 'app-stop-finder',
  templateUrl: './stop-finder.component.html',
  styleUrls: ['./stop-finder.component.less']
})
export class StopFinderComponent implements OnInit {

  stops: Stop[];
  search: string;

  onTextChange(search: string): void {
    this.getStops(search);
  }

  getStops(search: string): void {
    this.stopfinderService.getStops(search)
      .subscribe(stops => this.stops = stops);
  }

  clearStops(): void {
    const stops: number = this.stops.length;
    for (let i = 0; i < stops; i++) {
      this.stops.pop();
    }
  }

  constructor(private stopfinderService: StopFinderService) { }

  ngOnInit() {
  }

}
