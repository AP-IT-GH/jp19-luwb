import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TagsAnchorsComponent } from './tags-anchors.component';

describe('TagsAnchorsComponent', () => {
  let component: TagsAnchorsComponent;
  let fixture: ComponentFixture<TagsAnchorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TagsAnchorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TagsAnchorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
