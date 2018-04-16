import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {User} from '../../../../shared/models/user.model';
import {UserService} from '../../../../shared/services/user.service';
import {Subscription} from 'rxjs/Subscription';
import {AccountService} from '../../../../shared/services/account.service';

@Component({
  selector: 'app-friend-list-item',
  templateUrl: './friend-list-item.component.html',
  styleUrls: ['./friend-list-item.component.css']
})
export class FriendListItemComponent implements OnInit, OnDestroy {
  @Input() friend: User;
  @Input() index: number;
  private friends: User[];
  private accountFriends: User[];
  isFriend: number;
  private subscription: Subscription;

  constructor(private userService: UserService,
              private accountService: AccountService) {
  }

  ngOnInit() {
    this.isFriend = 0;

    this.friends = this.userService.getFriends();
    this.accountFriends = this.userService.getAccountFriends();
    this.subscription = this.userService.getFriendsSubject()
      .subscribe((friends: User[]) => {
        this.friends = friends;
        this.setIsFriend();
      });
    this.subscription = this.userService.getAccountFriendsSubject()
      .subscribe((friends: User[]) => {
        this.accountFriends = friends;
        this.setIsFriend();
      });

    this.setIsFriend();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  setIsFriend() {
    if (this.accountFriends !== undefined) {
      if (this.friend.username === this.accountService.username) {
        this.isFriend = 0;
      } else if (this.accountFriends.find(f => f.username === this.friend.username)) {
        this.isFriend = 1;
      } else {
        this.isFriend = -1;
      }
    }
  }

  onAddFriend() {
    this.userService.addFriend(this.friend)
      .then((result) => {
        this.isFriend = 1;
        console.log('Added ' + result.json().name + ' as a friend');
      })
      .catch((error) => {
        console.log(error);
      });
  }

  onRemoveFriend(user: User) {
    this.userService.removeFriend(this.friend)
      .then((result) => {
        this.isFriend = -1;
        console.log('Removed ' + result.json().name + ' as a friend');
      })
      .catch((error) => {
        console.log(error);
      });
  }

}
