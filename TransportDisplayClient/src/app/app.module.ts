import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TimetableComponent } from './components/timetable/timetable.component';
import { WeatherComponent } from './components/weather/weather.component';
import { StopFinderComponent } from './components/stop-finder/stop-finder.component';
import { AlertsComponent } from './components/alerts/alerts.component';

@NgModule({
  declarations: [
    AppComponent,
    TimetableComponent,
    WeatherComponent,
    StopFinderComponent,
    AlertsComponent
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
