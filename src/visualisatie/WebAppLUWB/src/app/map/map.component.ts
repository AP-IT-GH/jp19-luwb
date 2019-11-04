import { Component, OnInit } from '@angular/core';
import { CallapiService, TagAnchor } from '../common/callapi.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

  constructor(private apiService: CallapiService) { }

  width: number[] = [250,80,100,150];
  height: number[] = [100,60,120,350];
  tags: TagAnchor[] = [];
  anchors: TagAnchor[] = [];
  timer: number = 1000;
  interval;
  async ngOnInit() {
    await this.GetTags();
    await this.GetAnchors();
    this.interval = window.setInterval(() => { this.GetTags(); }, this.timer);
  }
  async GetTags(){
    this.tags = await this.apiService.GetTags();
  }
  async GetAnchors(){
    this.anchors = await this.apiService.GetAnchors();
  }
  ngOnDestroy(){
    clearInterval(this.interval);
  }
}