import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { MsalService } from '../services/msal.service';


@Injectable({
  providedIn: 'root'
})
export class MsalGuard implements CanActivate {

  constructor(private msalService: MsalService) {}

  canActivate() {
    if (this.msalService.isLoggedIn()) {
      return true;
    }
    this.msalService.login();
    return false;
  }
  
}
