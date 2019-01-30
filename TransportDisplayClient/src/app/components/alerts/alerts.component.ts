import { Component, OnInit } from '@angular/core';
import { AlertsService } from './../../services/alerts.service';

@Component({
  selector: 'app-alerts',
  templateUrl: './alerts.component.html',
  styleUrls: ['./alerts.component.less']
})
export class AlertsComponent implements OnInit {

  constructor(public alertsService: AlertsService) {}

  ngOnInit() {
    this.alertsService.startConnection();
    this.alertsService.addAlertListener();
  }

}
