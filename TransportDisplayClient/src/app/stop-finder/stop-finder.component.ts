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
    console.log("search text changed!")
    this.getStops(search);
  }

  getStops(search: string): void {
    if (search && search.length > 0) {
      this.stopfinderService.getStops(search)
        .subscribe(stops => this.stops = stops);
    }
  }

  constructor(private stopfinderService: StopFinderService) { }

  ngOnInit() {
  }

}
