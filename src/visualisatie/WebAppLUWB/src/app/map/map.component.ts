import { Component, OnInit, OnDestroy } from '@angular/core';
import { CallapiService, TagAnchor } from '../common/callapi.service';
import { MapService } from '../common/map.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, OnDestroy {

  constructor(private apiService: CallapiService, private mapService: MapService) { }

  width: number[] = [250,80,100,150];
  height: number[] = [100,60,120,350];
  tags: TagAnchor[] = [];
  anchors: TagAnchor[] = [];
  
  interval;

  async ngOnInit() {
    await this.GetTags();
    await this.GetAnchors();
    this.ChangeMap();
    this.interval = window.setInterval(() => { this.GetTags(); }, this.mapService.timer);
  }
  ngOnDestroy(){
    clearInterval(this.interval);
  }
  async GetTags(){
    this.tags = await this.apiService.GetTags();
    
  }
  async GetAnchors(){
    this.anchors = await this.apiService.GetAnchors();
    
  }
  ChangeRefreshTimer(){
    clearInterval(this.interval);
    this.interval = window.setInterval(() => { this.GetTags(); }, this.mapService.timer);
  }
  ChangeMap(){
    if(this.mapService.visualListTags.length != this.tags.length){
      var tempTags = this.mapService.visualListTags;
      this.mapService.visualListTags = [];
      this.tags.forEach((element, index) => {
        element.type = 'Tag';
        if(tempTags.length == 0){
          this.mapService.visualListTags.push({tagAnchor: element, visible: true});
        }
        else{
          var previousTags = this.mapService.visualListTags;
          this.mapService.visualListTags = [];
          if(previousTags[index])
            this.mapService.visualListTags.push({tagAnchor: element, visible: previousTags[index].visible});
          else
            this.mapService.visualListAnchors.push({tagAnchor: element, visible: true});
        }
      
      });
    }
    
    if(this.mapService.visualListAnchors.length != this.anchors.length){
      var tempAnchors = this.mapService.visualListAnchors;
      this.mapService.visualListAnchors = [];
      this.anchors.forEach((element, index) => {
        element.type = 'Anchor';
        if(tempAnchors.length == 0){
          this.mapService.visualListAnchors.push({tagAnchor: element, visible: true});
        }
        else{
          var previousAnchors = this.mapService.visualListAnchors;
          this.mapService.visualListAnchors = [];
          if(previousAnchors[index])
            this.mapService.visualListAnchors.push({tagAnchor: element, visible: previousAnchors[index].visible});
          else
            this.mapService.visualListAnchors.push({tagAnchor: element, visible: true});
        }
      
      });
    }
    
  }
  ChangeMapItems(item){
    item.visible = !item.visible;
  }

  get GetTimer() {
    return this.mapService.timer;
  }
  set GetTimer(timer){
    this.mapService.timer = timer;
  }
  get GetVisualListTags(){
    return this.mapService.visualListTags
  }
  get GetVisualListAnchors(){
    return this.mapService.visualListAnchors
  }
  
}