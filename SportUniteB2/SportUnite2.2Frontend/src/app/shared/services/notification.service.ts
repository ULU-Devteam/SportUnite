import {Injectable} from '@angular/core';
import {AccountService} from './account.service';
import {Http, Headers} from '@angular/http';
import {Subject} from 'rxjs/Subject';
import {NotificationList} from '../models/notificationlist.model';
import {User} from '../models/user.model';
import {UrlConfig} from "../config/url.config";

@Injectable()
export class NotificationService {
  // private api = 'https://sportunitenodeapi.azurewebsites.net/api';
  private api;
  private notificationsList: NotificationList;
  private notificationsChanged = new Subject<NotificationList>();

  constructor(private http: Http, private accountService: AccountService, private config: UrlConfig) {
    this.api = this.config.nodeUrl;
  }

  getNotifySub() {
    return this.notificationsChanged;
  }

  setNotifySub() {
    if (this.notificationsList.notifications.length > 0) {
      this.notificationsChanged.next(this.notificationsList);
    }
  }

  getUserNotifications(username: string) {
    const headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});

    this.http.get(
      this.api + '/account/' + username + '/notifications',
      {headers: headers})
      .toPromise()
      .then((result) => {
        console.log(result.json());
        this.notificationsList = result.json();
        this.setNotifySub();
      })
      .catch((error) => {
        console.log(error);
      });
  }

  updateAllUserNotifications(username: string, update) {
    const headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});

    return this.http.put(
      this.api + '/account/' + username + '/notifications',
      update,
      {headers: headers}
    ).toPromise();
  }

  updateUserNotification(username: string, eventId: string, update) {
    const headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});

    return this.http.put(
      this.api + '/account/' + username + '/notifications/' + eventId,
      update,
      {headers: headers}
    ).toPromise();
  }

  postEventNotifications(username: string, eventId: string, friends: string[], message: string) {
    const headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});

    return this.http.post(
      this.api + '/event/' + eventId + '/notifications',
      {username: username, friends: friends, read: false, message: message},
      {headers: headers}
    ).toPromise();
  }

}
