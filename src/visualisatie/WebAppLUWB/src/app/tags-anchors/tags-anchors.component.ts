import { Component, OnInit } from '@angular/core';
import { CallapiService, TagAnchor} from '../common/callapi.service';
import { SelectItem } from 'primeng/components/common/selectitem';

@Component({
  selector: 'app-tags-anchors',
  templateUrl: './tags-anchors.component.html',
  styleUrls: ['./tags-anchors.component.scss']
})
export class TagsAnchorsComponent implements OnInit {

  constructor(private apiService: CallapiService) { }
  visualList: TagAnchor[] = [];
  tags: TagAnchor[] = [];
  anchors: TagAnchor[] = [];
  listTypes: SelectItem[] = [{label:"Tags",value:1},{label:"Anchors",value:2}];
  TypeSelected: string[] = ["1","2"];
  selected: string = "id";
  OrderByList: string[] = ["id","mac","xPos","yPos"];
  addTagAnchor: TagAnchor = {mac:null,description:null,xPos:null,yPos:null,type:"Choose..."};
  errorAdd: boolean = false;
  successAdd: boolean = false;
  pageSizeList: number[] = [2,4,6,8,10,20,30,40,50];
  deleteTagAnchor: TagAnchor;
  errorDelete: boolean = false;
  succesDelete: boolean = false;
  ngOnInit() {
    this.GetTagsAndAnchors();
  }

  FilterBy(event){
    var element = this.TypeSelected.find(element => element == `${event.option.value}`);
    if(element == "1" && this.TypeSelected.length == 2) this.TypeSelected = ["2"];
    else if(element == "2" && this.TypeSelected.length == 2) this.TypeSelected = ["1"];
    else if (element == "1" || element == "2") this.TypeSelected = [];
    else this.TypeSelected.push(`${event.option.value}`);
    this.GetSelectetObjects();
  }
  GetSelectetObjects(paging?:boolean){
    this.visualList = [];
    if(!paging)this.apiService.pageNumber = 1;
    if(this.TypeSelected.length != 2) this.apiService.pageSizeDivider = 1;
    else this.apiService.pageSizeDivider = 2;
    if (this.TypeSelected.length == 2) this.GetTagsAndAnchors();
    else if (this.TypeSelected[0] == "1") this.GetTags();
    else if (this.TypeSelected[0] == "2") this.GetAnchors();
  }

  async GetTagsAndAnchors(){
    this.visualList = [];
    await this.GetTags();
    await this.GetAnchors(); 
  }

  async GetTags(){
    this.tags = await this.apiService.GetTags();
    this.tags.forEach(element => {
      element.type = "Tag";
      this.visualList.push(element);
    });
  }

  async GetAnchors(){
    this.anchors = await this.apiService.GetAnchors();
    this.anchors.forEach(element => {
      element.type = "Anchor";
      this.visualList.push(element);
    });
  }
  AddTagAnchor(){
    if(this.addTagAnchor.type == "Tag")
      this.apiService.AddTag(this.addTagAnchor).subscribe(
        (val) => {
          this.successAdd = true;
          this.GetSelectetObjects(true);
          this.addTagAnchor = {mac:null,description:null,xPos:null,yPos:null,type:"Choose..."};
        },
        response => {
          this.errorAdd = true;
        }
      );
    else if(this.addTagAnchor.type == "Anchor")
      this.apiService.AddAnchor(this.addTagAnchor).subscribe(
        (val) => {
          this.successAdd = true;
          this.GetSelectetObjects(true);
          this.addTagAnchor = {mac:null,description:null,xPos:null,yPos:null,type:"Choose..."};
        },
        response => {
          this.errorAdd = true;
        }
      );
  }

  PreviousPage(){
    if(this.apiService.pageNumber > 1){
      this.apiService.pageNumber--;
      this.GetSelectetObjects(true);
    }
  }
  NextPage(){
    var aantal;
    if(this.TypeSelected.length == 2)
      aantal = this.apiService.pageSize-1;
    else aantal = this.apiService.pageSize/this.apiService.pageSizeDivider-1;
    if(this.visualList[aantal]){
      this.apiService.pageNumber++;
      this.GetSelectetObjects(true);
    }
  }
  DeleteItem(object: TagAnchor){
    this.deleteTagAnchor = object;
  }
  ConfirmedDelete(){
    if(this.deleteTagAnchor.type == "Anchor"){
      this.apiService.DeleteAnchor(this.deleteTagAnchor.id).subscribe(
        (val) => {
          this.succesDelete = true;
          this.GetSelectetObjects(true);
        },
        response => {
          this.errorDelete = true;
        }
      );
    }
    else{
      this.apiService.DeleteTag(this.deleteTagAnchor.id).subscribe(
        (val) => {
          this.succesDelete = true;
          this.GetSelectetObjects(true);
        },
        response => {
          this.errorDelete = true;
        }
      );
    }
    this.deleteTagAnchor = null;
  }
}
