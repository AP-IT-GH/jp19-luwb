import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TagsAnchorsComponent } from './tags-anchors.component';
import { RouterModule } from '@angular/router';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

describe('TagsAnchorsComponent', () => {
  let component: TagsAnchorsComponent;
  let fixture: ComponentFixture<TagsAnchorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterModule.forRoot([]), SelectButtonModule, ButtonModule, FormsModule, HttpClientModule],
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
