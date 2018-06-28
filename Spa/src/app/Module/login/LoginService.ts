import { Http, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
 
 
 import * as ApiAddress from '../../core/services/ApiAddress';
import { UserRegisterDto } from './register/UserRegisterDto';
import { useridintity } from '../../core/services/ApiAddress';
@Injectable()
export class IdentityService{


constructor(private _http:Http){}
 
login(username:string,password:string): any {

    let urlSearchParams = new URLSearchParams();
    urlSearchParams.append('username', username);
    urlSearchParams.append('password', password);
    urlSearchParams.append('grant_type', 'password');
    // let body = urlSearchParams.toString();
    const body = {
        username, password
    }
    let headers = new Headers();
    headers.append('Content-Type', 'application/json; charset=utf-8');
    return this._http.post(ApiAddress.api+'account/login', body, { headers: headers })
        .map(res => res.json());
}

RregisterUser(user_egister:UserRegisterDto): any {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers });
    const body = JSON.stringify(user_egister); 
    return this._http.post(ApiAddress.api+"User/Create",body,options).map(res=> res.json());

}
LogOut(){

    return this._http.get(ApiAddress.api+"Account/Logout").map(res=> res.json())
}

GetIdintity(){
 

    let headers = new Headers();
    headers.append('Authorization', 'bearer ' + useridintity.token);
    return this._http.get('http://localhost:49223//api/MyProtectedAdminApi', { headers: headers })
        .map(res => res.json());


}
} 