import { Component, OnInit } from '@angular/core';
import { MsalService } from '../services/msal.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  constructor(private msalService: MsalService) { }

  ngOnInit() {
  }

  login() {
    this.msalService.login();
  }

  logout() {
    this.msalService.logout();
  }

  isUserLoggedIn() {
    return this.msalService.isLoggedIn();
  }

  userfirstname() {
    return this.msalService.getUserFirstName();
  }

}
