import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule} from "@angular/router"


import {ToolbarModule} from 'primeng/toolbar';
import {ButtonModule} from 'primeng/button';


import { AppComponent } from './app.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TagsAnchorsComponent } from './tags-anchors/tags-anchors.component';

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    HomeComponent,
    PageNotFoundComponent,
    TagsAnchorsComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path:"home", component: HomeComponent},
      {path:"tags_anchors", component: TagsAnchorsComponent},
      {path: "", redirectTo: "home", pathMatch:"full"},
      {path: "**", component: PageNotFoundComponent}
    ]),
    ToolbarModule,
    ButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
