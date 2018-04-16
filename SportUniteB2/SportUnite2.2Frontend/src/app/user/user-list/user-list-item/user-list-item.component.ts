import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {User} from '../../../shared/models/user.model';
import {UserService} from '../../../shared/services/user.service';
import {Subscription} from 'rxjs/Subscription';
import {ActivatedRoute, Router} from '@angular/router';
import {AccountService} from '../../../shared/services/account.service';

@Component({
  selector: 'app-user-list-item',
  templateUrl: './user-list-item.component.html',
  styleUrls: ['./user-list-item.component.css']
})
export class UserListItemComponent implements OnInit, OnDestroy {
  @Input() user: User;
  @Input() index: number;
  private friends: User[];
  isFriend: number;
  private subscription: Subscription;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private userService: UserService,
              private accountService: AccountService) {
  }

  ngOnInit() {
    this.isFriend = 0;

    this.friends = this.userService.getFriends();
    this.subscription = this.userService.getFriendsSubject()
      .subscribe((friends: User[]) => {
        this.friends = friends;
        this.setIsFriend();
      });

    if (this.friends !== undefined) {
      this.setIsFriend();
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  setIsFriend() {
    if (this.user.username === this.accountService.username) {
      this.isFriend = 0;
    } else if (this.friends.find(f => f.username === this.user.username)) {
      this.isFriend = 1;
    } else {
      this.isFriend = -1;
    }
  }

  onAddFriend() {
    this.userService.addFriend(this.user)
      .then((result) => {
        this.isFriend = 1;
        console.log('Added ' + result.json().name + ' as a friend');
      })
      .catch((error) => {
        console.log(error);
        });
  }

  onRemoveFriend() {
    this.userService.removeFriend(this.user)
      .then((result) => {
        this.isFriend = -1;
        console.log('Removed ' + result.json().name + ' as a friend');
      })
      .catch((error) => {
        console.log(error);
      });
  }

  onUserClicked() {
    this.router.navigate([this.user.username], {relativeTo: this.route});
  }

}
