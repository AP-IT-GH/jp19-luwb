import { Injectable } from '@angular/core';
import { TagAnchor } from './callapi.service';

@Injectable({
  providedIn: 'root'
})
export class MapService {

  constructor() { }

  timer: number = 1000;
  coordinatesVisible: boolean = true;
  visualListTags: TagAnchorMap[] = [];
  visualListAnchors: TagAnchorMap[] = [];

}

export interface TagAnchorMap {
  tagAnchor: TagAnchor;
  visible: boolean;
}
