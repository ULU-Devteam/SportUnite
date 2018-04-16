import {Http, Headers} from '@angular/http';
import {Injectable} from "@angular/core";
import {AccountService} from "./account.service";
import {Event} from "../models/event.model";
import {LocationService} from "./location.service";
import {UrlConfig} from "../config/url.config";


@Injectable()
export class EventService {

  private headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});

  private server: string;
  // private server = 'https://sportunitenodeapi.azurewebsites.net/api/event';

  constructor(private Http: Http, private accountService: AccountService, private locationService: LocationService,
              private config: UrlConfig) {
    this.accountService.tokenSubject.subscribe((newToken) => {
      this.headers = new Headers({'Content-Type': 'application/json', 'token': newToken});
    });
    this.server = this.config.nodeUrl + 'event';
  }

  getEvents() {
    return this.Http.get(this.server, {headers: this.headers});
  }

  getEvent(id: number) {
    return this.Http.get(this.server + '/' + id, {headers: this.headers});
  }

  getEventUsersById(id: number) {
    return this.Http.get(this.server + '/' + id + '/users', {headers: this.headers});
  }

  joinEvent(id: number, event: Event) {
    let headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});
    return this.Http.post(this.server + '/' + id + '/join', event, {headers: headers});
  }

  getComplex() {
    return this.Http.get('https://sportuniteapi.azurewebsites.net/api/sportcomplex');
  }

  getSports() {
    return this.Http.get('https://sportuniteapi.azurewebsites.net/api/sport');
  }

  createEvent(event) {
    return this.Http.post('https://sportuniteapi.azurewebsites.net/api/Event', event);
  }

  createEventMongo(event) {
    return this.locationService.getAddress(event.sportcomplex.addressId).toPromise()
      .then((res) => {
        return this.locationService.geoCode(res.json().city + '+' + res.json().street).toPromise();
      })
      .then((res) => {
        event.long = res.json().results[0].geometry.location.lng;
        event.lat = res.json().results[0].geometry.location.lat;
        let headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});
        return this.Http.post(this.server, event, {headers: headers}).toPromise();
      });

  }

}
