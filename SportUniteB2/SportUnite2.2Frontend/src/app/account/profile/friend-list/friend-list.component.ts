import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {User} from '../../../shared/models/user.model';
import {UserService} from '../../../shared/services/user.service';
import {Subscription} from 'rxjs/Subscription';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit, OnDestroy {
  friends: User[];
  subscription: Subscription;
  @Input() username: string;
  loaded = false;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.subscription = this.userService.getFriendsSubject()
      .subscribe((friends: User[]) => {
        this.friends = friends;
        this.loaded = true;
      });
    this.userService.refreshAccountFriends();
    this.userService.refreshFriends(this.username);

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
