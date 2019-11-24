import { Injectable } from '@angular/core';
import * as Msal from 'msal';

@Injectable({
  providedIn: 'root'
})
export class MsalService {

  constructor() { }

  B2CTodoAccessTokenKey = 'b2c.access.token';

    tenantConfig = {
        domain: 'https://luwbAP.b2clogin.com/tfp/luwbAP.onmicrosoft.com/',
        // Replace this with your client id
        clientID: '46a44e1e-47f9-42f5-a067-5e8801d9020d',
        signInPolicy: 'B2C_1_signin',
        resetPasswordPolicy: 'B2C_1_resetpassword',
        redirectUri: 'https://luwb.azurewebsites.net',
        b2cScopes: ['https://luwbAP.onmicrosoft.com/access-api/user_impersonation']
    };

    // Configure the authority for Azure AD B2C
    authority = this.tenantConfig.domain + this.tenantConfig.signInPolicy;

    /*
     * B2C SignIn SignUp Policy Configuration
     */
    clientApplication = new Msal.UserAgentApplication(
        this.tenantConfig.clientID, this.authority,
        function(errorDesc: any, token: any, error: any, tokenType: any) {
        },
        {
          validateAuthority: false
        }
    );

    public login(): void {
      this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.signInPolicy;
      this.authenticate();
    }

    public resetPassword(): void {
        this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.resetPasswordPolicy;
        this.authenticate();
    }

    public authenticate(): void {
        const THIS = this;
        this.clientApplication.loginPopup(this.tenantConfig.b2cScopes).then(function(idToken: any) {
            THIS.clientApplication.acquireTokenSilent(THIS.tenantConfig.b2cScopes).then(
                function(accessToken: any) {
                    THIS.saveAccessTokenToCache(accessToken);
                }, function(error: any) {
                    THIS.clientApplication.acquireTokenPopup(THIS.tenantConfig.b2cScopes).then(
                        function(accessToken: any) {
                            THIS.saveAccessTokenToCache(accessToken);
                        }, function(error: any) {
                            console.log('error: ', error);
                        });
                });
        }, function(errorDesc: any) {
            console.log('error: ', errorDesc);
            if (errorDesc.indexOf('AADB2C90118') > -1) {
                THIS.resetPassword();
            } else if (errorDesc.indexOf('AADB2C90077') > -1) {
                // Expired Token
                THIS.logout();
            }
        });
    }

    saveAccessTokenToCache(accessToken: string): void {
        sessionStorage.setItem(this.B2CTodoAccessTokenKey, accessToken);
    }

    logout(): void {
        this.clientApplication.logout();
    }

    getUser() {
        return this.clientApplication.getUser();
    }

    isLoggedIn(): boolean {
        return this.getUser() != null;
    }

    getUserObjectId() {
        return this.getUser().idToken['oid'];
    }

    getUserFirstName() {
        return this.getUser().idToken['given_name'];
    }
}
