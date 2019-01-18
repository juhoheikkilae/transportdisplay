import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TimetableComponent } from './timetable/timetable.component';
import { WeatherComponent } from './weather/weather.component';
import { StopFinderComponent } from './stop-finder/stop-finder.component';

@NgModule({
  declarations: [
    AppComponent,
    TimetableComponent,
    WeatherComponent,
    StopFinderComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
