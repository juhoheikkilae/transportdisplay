export class Timetable {
    stop: Stop;
    departures: Departure[];
}

export class Stop {
    name: string;
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
