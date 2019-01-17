export class Timetable {
  stop: Stop;
  departures: Departure[];
  arrivalEstimates: ArrivalEstimate[];
  displayType: DisplayType;
}

export class Stop {
  name: string;
  lines: Line[];
}

export class Line {
  id: string;
  origin: string;
  destination: string;
  direction: number;
}

export class Departure {
  line: Line;
  time: Date;
}

export class ArrivalEstimate {
  line: Line;
  arrivesIn: number;
  isRealTimeEstimate: boolean;
}

export enum DisplayType {
  DEPARTURES = 0,
  ARRIVALS = 1
}
