import { Component, OnInit } from '@angular/core';
import { CallapiService } from '../common/callapi.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {


  constructor(private apiService: CallapiService) { }

  ngOnInit() {
  }

}
