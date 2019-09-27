import { Component, OnInit } from '@angular/core';
import { CallapiService } from '../common/callapi.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  pageSizeList: number[] = [2,4,6,8,10,20,30,40,50];

  constructor(private apiService: CallapiService) { }

  ngOnInit() {
  }

}
