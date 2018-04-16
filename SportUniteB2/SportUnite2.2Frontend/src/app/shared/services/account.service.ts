import {Http, Headers} from '@angular/http';
import {Injectable} from '@angular/core';
import {User} from '../models/user.model';
import {UrlConfig} from "../config/url.config";
import {Subject} from "rxjs/Subject";

@Injectable()
export class AccountService {
  get username(): string {
    return this._username;
  }

  set username(value: string) {
    this._username = value;
  }

  get token(): string {
    return this._token;
  }

  set token(value: string) {
    this._token = value;
    this.tokenSubject.next(this._token);
  }

  get loggedIn(): boolean {
    return this._loggedIn;
  }

  set loggedIn(value: boolean) {
    this._loggedIn = value;
    this.loginEvent.next(this.loggedIn);
  }

  get socialUser(): any {
    return this._socialUser;
  }


  set socialUser(value: any) {
    this._socialUser = value;
  }
  private _loggedIn: boolean = false;
  private _token: string;
  tokenSubject = new Subject<string>();
  loginEvent = new Subject<boolean>();
  private _username: string;
  private baseUrl: string
  private _socialUser: any;

  constructor(private http: Http, private config: UrlConfig) {
    this.baseUrl = config.nodeUrl;
  }

  private headers = new Headers({'Content-Type': 'application/json', 'token': this.token});

  login(username: string, password: string) {
    return this.http.post(this.baseUrl + 'account/login', {username: username, password: password});
  }

  getAccount(username: string) {
    return this.http.get(this.baseUrl + 'account/' + username, {
      headers: new Headers({
        'Content-Type': 'application/json',
        'token': this.token
      })
    });
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'account', user, {headers: this.headers})
      .toPromise();
  }

  updateAccount(username: string, user) {
    return this.http.put(this.baseUrl + 'account/' + username, user, {
      headers: new Headers({
        'Content-Type': 'application/json',
        'token': this.token
      })
    });
  }

  logOut() {
    this.socialUser = {};
    this.username = '';
    this.token = '';
    this.loggedIn = false;
  }

  socialLogin() {
    const header = new Headers({
      'Content-Type': 'application/json',
      'token': this.token
    });

    if (this.socialUser.provider === 'facebook') {
      console.log('facebook');
      return this.http.post(this.baseUrl + 'account/facebooklogin', {}, {
        headers: header
      });
    } else if (this.socialUser.provider === 'google') {
      console.log('google');
      return this.http.post(this.baseUrl + 'account/googlelogin', {}, {
        headers: header
      });
    }
  }

  socialRegister(user: User) {
    const header = new Headers({
      'Content-Type': 'application/json',
      'token': this.token
    });

    if (this.socialUser.provider === 'facebook') {
      return this.http.post(this.baseUrl + 'account/facebookregister', user, {
        headers: header
      });
    } else if (this.socialUser.provider === 'google') {
      return this.http.post(this.baseUrl + 'account/googleregister', user, {
        headers: header
      });
    }
  }
}
