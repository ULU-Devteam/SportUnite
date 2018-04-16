import {Injectable} from '@angular/core';
import {Subject} from 'rxjs/Subject';
import {User} from '../models/user.model';
import {Http, Headers} from '@angular/http';
import {AccountService} from './account.service';
import {UrlConfig} from "../config/url.config";


@Injectable()
export class UserService {
  private headers = new Headers({'Content-Type': 'application/json', 'token': this.accountService.token});
  private api: string

  private usersChanged = new Subject<User[]>();
  private friendsChanged = new Subject<User[]>();
  private accountFriendsChanged = new Subject<User[]>();
  private users: User[] = [];
  private accountFriends: User[] = undefined;
  private friends: User[] = undefined;


  constructor(private http: Http, private accountService: AccountService, private config: UrlConfig) {
    this.api = config.nodeUrl + 'account/';
    this.accountService.tokenSubject.subscribe((newToken) => {
      this.headers = new Headers({'Content-Type': 'application/json', 'token': newToken})
    });

  }

  getUsersSubject() {
    return this.usersChanged;
  }

  getFriendsSubject() {
    return this.friendsChanged;
  }

  getAccountFriendsSubject() {
    return this.accountFriendsChanged;
  }

  getFriends() {
    return this.friends;
  }

  setUsers(users: User[]): void {
    this.users = users;
    this.usersChanged.next(this.users.slice());
  }

  setFriends(friends: User[]): void {
    this.friends = friends;
    this.friendsChanged.next(this.friends.slice());
  }

  setAccountFriends(friends: User[]): void {
    this.accountFriends = friends;
  }

  getAccountFriends(): User[] {
    return this.accountFriends;
  }

  refreshUsers(): void {
    this.http.get(this.api, {headers: this.headers})
      .toPromise()
      .then((users) => {
        console.log('Retrieved ' + users.json().length + ' user(s)');
        this.setUsers(users.json());
        return users.json();
      })
      .catch((error) => {
        console.log(error);
      });
  }

  refreshFriends(username: string): void {
    this.http.get(this.api + username + '/friends', {headers: this.headers})

      .toPromise()
      .then((friends) => {
        console.log('Retrieved ' + friends.json().length + ' friend(s)');
        this.setFriends(friends.json());
        return friends.json();
      })
      .catch((error) => {
        console.log(error);
      });
  }

  refreshAccountFriends(): void {

    this.http.get(this.api + this.accountService.username + '/friends', {headers: this.headers})

      .toPromise()
      .then((friends) => {
        console.log('Retrieved ' + friends.json().length + ' friend(s)');
        this.setAccountFriends(friends.json());
        return friends.json();
      })
      .catch((error) => {
        console.log(error);
      });
  }

  getUser(user: User): User {
    for (let i = 0, charactersLength = this.users.length; i < charactersLength; i++) {
      if (this.users[i].name === user.name) {
        return this.users[i];
      }
    }
    return undefined;
  }

  addFriend(user: User) {

    return this.http.post(this.api + this.accountService.username + '/friends', user, {headers: this.headers})

      .toPromise();
  }

  removeFriend(user: User) {

    return this.http.delete(this.api+ this.accountService.username + '/friends/' + user.username, {headers: this.headers})

      .toPromise();
  }

}
