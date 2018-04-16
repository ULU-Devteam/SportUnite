import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {User} from '../../shared/models/user.model';
import {UserService} from '../../shared/services/user.service';
import {AccountService} from '../../shared/services/account.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit, OnDestroy {
  private allUsers: User[];
  users: User[];
  private friends: User[];
  private subscription: Subscription;
  loaded: boolean = false;

  error: boolean =  false;
r

  constructor(private userService: UserService,
              private accountService: AccountService) {
  }

  ngOnInit() {
    this.subscription = this.userService.getUsersSubject()
      .subscribe((users: User[]) => {
        this.users = users;
        this.allUsers = users;
        this.loaded = true;
      }),
      (error) => {
        console.log(error);
        this.error = true;
      };
    this.subscription = this.userService.getFriendsSubject()
      .subscribe((friends: User[]) => {
        this.friends = friends;
      }),
      (error) => {
        console.log(error);
        this.error = true;
      };

    console.log('refreshing users');
    this.userService.refreshUsers();
    console.log('refreshing friends');
    this.userService.refreshFriends(this.accountService.username);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  filterList(filter) {
      const filteredList = [];
      for (const u of this.allUsers) {
        if (u.username.toLowerCase().indexOf(filter.toLowerCase()) !== -1) {
          filteredList.push(u);
        }
        this.users = filteredList;
      }

  }

}
