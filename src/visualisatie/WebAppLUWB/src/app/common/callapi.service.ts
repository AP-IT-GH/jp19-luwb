import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class CallapiService {

  constructor(private http: HttpClient) {}

  domain:string = 'https://luwb-api.azurewebsites.net/api/';
  //domain:string = 'https://localhost:44321/api/';
  sortBy: string = 'id';
  pageSize: number = 10;
  pageSizeDivider: number = 2;
  pageNumber:number = 1;
  public GetTags(){
    return this.http
    .get<TagAnchor[]>(`${this.domain}tags?sortBy=${this.sortBy}&pageSize=${this.pageSize/this.pageSizeDivider}&pageNumber=${this.pageNumber-1}`)
    .toPromise();
    //return this.http.get<TagAnchor[]>(`${this.domain}tags`);
  }
  public GetAnchors(){
    return this.http
    .get<TagAnchor[]>(`${this.domain}anchors?sortBy=${this.sortBy}&pageSize=${this.pageSize/this.pageSizeDivider}&pageNumber=${this.pageNumber-1}`)
    .toPromise();
    //return this.http.get<TagAnchor[]>(`${this.domain}anchors`);
  }
  public AddTag(addTagAnchor: TagAnchor){
    return this.http.post<TagAnchor>(`${this.domain}tags`,addTagAnchor);
  }
  public AddAnchor(addTagAnchor: TagAnchor){
    return this.http.post<TagAnchor>(`${this.domain}anchors`,addTagAnchor);
  }
  public DeleteTag(id: number){
    return this.http.delete(`${this.domain}tags/${id}`);
  }
  public DeleteAnchor(id: number){
    return this.http.delete(`${this.domain}anchors/${id}`);
  }
  public EditTag(editTag: TagAnchor){
    return this.http.put(`${this.domain}tags`,editTag);
  }
  public EditAnchor(editAnchor: TagAnchor){
    return this.http.put(`${this.domain}anchors`,editAnchor);
  }
}

export interface TagAnchor {
  id?: number;
  mac: string;
  description: string;
  xPos: number;
  yPos: number;
  type: string;
}
