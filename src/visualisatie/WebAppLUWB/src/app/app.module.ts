import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule} from '@angular/router'
import {FormsModule} from '@angular/forms';

import {ToolbarModule} from 'primeng/toolbar';
import {ButtonModule} from 'primeng/button';
import {SelectButtonModule} from 'primeng/selectbutton';


import { AppComponent } from './app.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TagsAnchorsComponent } from './tags-anchors/tags-anchors.component';
import { CallapiService } from './common/callapi.service';
import { HttpClientModule } from '@angular/common/http';
import { SettingsComponent } from './settings/settings.component';
import { MapComponent } from './map/map.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MsalService } from './services/msal.service';
import { MsalGuard } from './guard/msal.guard';

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    HomeComponent,
    PageNotFoundComponent,
    TagsAnchorsComponent,
    SettingsComponent,
    MapComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path: 'home', component: HomeComponent},
      {path: 'tags_anchors', component: TagsAnchorsComponent},
      {path: 'settings', component: SettingsComponent, canActivate: [MsalGuard]},
      {path: 'map', component: MapComponent},
      {path: '', redirectTo: 'home', pathMatch: 'full'},
      {path: '**', component: PageNotFoundComponent}
    ]),
    ToolbarModule,
    FormsModule,
    ButtonModule,
    HttpClientModule,
    SelectButtonModule,
    DragDropModule
  ],
  providers: [
    CallapiService,
    MsalService,
    MsalGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
