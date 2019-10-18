import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

  constructor() { }

  width: number[] = [250,80,100,150];
  height: number[] = [100,60,120,350];
  ngOnInit() {
  }

}