import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { CallapiService, TagAnchor } from '../common/callapi.service';
import { MapService } from '../common/map.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, OnDestroy, AfterViewInit {

  constructor(private apiService: CallapiService, private mapService: MapService) { }

  width: number[] = [250,80,100,150];
  height: number[] = [100,60,120,350];
  tags: TagAnchor[] = [];
  anchors: TagAnchor[] = [];

  interval;

  widthMap: number = 0;
  heightMap: number = 0;
  heightScaler: number = 1;
  widthScaler: number = 1;
  maxWidthTagAnchor: number = 0;
  maxHeightTagAnchor: number = 0;
  onInitCalculate: boolean = true;
  

  async ngOnInit() {
    await this.GetTags();
    await this.GetAnchors();
    this.ChangeMap();
    this.interval = window.setInterval(() => { this.GetTags(); }, this.mapService.timer);
  }

  async ngAfterViewInit()	{
    await this.delay(300);
    this.onResize();
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }
  ngOnDestroy(){
    clearInterval(this.interval);
  }
  async GetTags(){
    this.tags = await this.apiService.GetTags();
    this.onResize();
  }
  async GetAnchors(){
    this.anchors = await this.apiService.GetAnchors();
    this.onResize();
    
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

  get Timer() {
    return this.mapService.timer;
  }
  set Timer(timer){
    this.mapService.timer = timer;
  }
  get GetVisualListTags(){
    return this.mapService.visualListTags
  }
  get GetVisualListAnchors(){
    return this.mapService.visualListAnchors
  }
  get CoordinatesVisible(){
    return this.mapService.coordinatesVisible;
  }
  set CoordinatesVisible(bool){
    this.mapService.coordinatesVisible = bool;
  }

  onResize(event = null) {
    //console.log(event.target.outerWidth);
    this.GetWidthAndHeightOfMap();
    this.ResizeMap();
  }

  GetWidthAndHeightOfMap(){
      var map = document.getElementById("map");
      if(map != null){
        this.widthMap = map.scrollWidth;
        this.heightMap = map.scrollHeight;
      }
  }
  
  ResizeMap(){
    var maxWidth = 0;
    var maxHeight = 0;
    this.anchors.map(anchor =>{
      if(anchor.xPos > maxWidth){
        maxWidth = anchor.xPos;
        
      }
      if(anchor.yPos > maxHeight){
        maxHeight = anchor.yPos;
      }
    });
    this.tags.map(tag => {
      if(tag.xPos > maxWidth){
        maxWidth = tag.xPos;
      }
      if(tag.yPos > maxHeight){
        maxHeight = tag.yPos;
      }
    });
    if(maxWidth - this.maxWidthTagAnchor >= 20 || maxHeight - this.maxHeightTagAnchor >= 20 || this.onInitCalculate || maxWidth - this.maxWidthTagAnchor <= 20 || maxHeight - this.maxHeightTagAnchor <= 20){
      this.maxWidthTagAnchor = maxWidth;
      this.maxHeightTagAnchor = maxHeight;
      this.onInitCalculate = false;
      this.CalculateMapFactor();
    }
    
  }
  CalculateMapFactor(){
    this.widthScaler = (this.widthMap - 100) / this.maxWidthTagAnchor;
    this.heightScaler = (this.heightMap - 100) / this.maxHeightTagAnchor;
    //console.log(this.widthScaler);
  }

}