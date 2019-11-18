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
  visualListTags: TagAnchorMap[] = [];
  visualListAnchors: TagAnchorMap[] = [];
  timer: number = 1000;
  coordinatesVisible: boolean = true;
  interval;
  async ngOnInit() {
    await this.GetTags();
    await this.GetAnchors();
    this.ChangeMap();
    this.interval = window.setInterval(() => { this.GetTags(); }, this.timer);
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
    this.interval = window.setInterval(() => { this.GetTags(); }, this.timer);
  }
  ChangeMap(){
    if(this.visualListTags.length != this.tags.length){
      var tempTags = this.visualListTags;
      this.visualListTags = [];
      this.tags.forEach((element, index) => {
        element.type = 'Tag';
        if(tempTags.length == 0){
          this.visualListTags.push({tagAnchor: element, visible: true});
        }
        else{
          var previousTags = this.visualListTags;
          this.visualListTags = [];
          if(previousTags[index])
            this.visualListTags.push({tagAnchor: element, visible: previousTags[index].visible});
          else
            this.visualListAnchors.push({tagAnchor: element, visible: true});
        }
      
      });
    }
    
    if(this.visualListAnchors.length != this.anchors.length){
      var tempAnchors = this.visualListAnchors;
      this.visualListAnchors = [];
      this.anchors.forEach((element, index) => {
        element.type = 'Anchor';
        if(tempAnchors.length == 0){
          this.visualListAnchors.push({tagAnchor: element, visible: true});
        }
        else{
          var previousAnchors = this.visualListAnchors;
          this.visualListAnchors = [];
          if(previousAnchors[index])
            this.visualListAnchors.push({tagAnchor: element, visible: previousAnchors[index].visible});
          else
            this.visualListAnchors.push({tagAnchor: element, visible: true});
        }
      
      });
    }
    
  }
  ChangeMapItems(item){
    item.visible = !item.visible;
  }
}
export interface TagAnchorMap {
  tagAnchor: TagAnchor;
  visible: boolean;
}
