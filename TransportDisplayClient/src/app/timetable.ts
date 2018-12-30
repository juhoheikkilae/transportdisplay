export class Timetable {
    stop: Stop;
    departures: Departure[];
    arrivalEstimates: ArrivalEstimate[];
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

export class ArrivalEstimate {}
