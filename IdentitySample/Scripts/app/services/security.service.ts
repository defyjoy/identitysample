import { Injectable } from '@angular/core';

@Injectable()

export default class SecurityService {

    storage: any = null;

    constructor() {

    }

    public authorize(): void {
        this.ResetAuthorizationData();

        console.log("BEGIN Authorize, no auth data");

        var authorizationUrl = 'https://identity-dev.com/connect/authorize';
        var client_id = 'angular2client';
        var redirect_uri = 'https://identity-dev.com';
        var response_type = "id_token token";
        var scope = "api";

        var url =
            authorizationUrl + "?" +
            "response_type=" + encodeURI(response_type) + "&" +
            "client_id=" + encodeURI(client_id) + "&" +
            "redirect_uri=" + encodeURI(redirect_uri) + "&" +
            "scope=" + encodeURI(scope);

        window.location.href = url;
    }

    public ResetAuthorizationData() {
        this.store("authorizationData", "");
        this.store("authorizationDataIdToken", "");

        this.store("HasAdminRole", false);
        this.store("IsAuthorized", false);
    }

    private retrieve(key: string): any {
        var item = this.storage.getItem(key);

        if (item && item !== 'undefined') {
            return JSON.parse(this.storage.getItem(key));
        }

        return;
    }

    private store(key: string, value: any) {
        this.storage.setItem(key, JSON.stringify(value));
    }

}