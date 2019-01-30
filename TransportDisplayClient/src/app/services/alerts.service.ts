import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Alert } from '../models/timetable';
import { signalRevents } from '../../config';

@Injectable({
  providedIn: 'root'
})
export class AlertsService implements OnInit {

  public alert: Alert;
  private hubConnection: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/alerthub')
      .build();

    this.hubConnection.start()
      .then(() => console.log('Started AlertHub connection'))
      .catch(err => console.log(`Failed to start AlertHub connection: ${err}`));
  }

  public addAlertListener = () => {
    this.hubConnection.on(signalRevents.ALERT, (alert: Alert) => {
      console.log(alert);
      this.alert = alert;
    });
  }

  ngOnInit() { }

  constructor() { }
}
