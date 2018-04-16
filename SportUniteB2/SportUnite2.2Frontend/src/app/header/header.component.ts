import {AfterViewInit, Component, OnInit, OnDestroy} from '@angular/core';
import {AccountService} from '../shared/services/account.service';
import {Router} from '@angular/router';
import {NotificationService} from '../shared/services/notification.service';
import {Subscription} from 'rxjs/Subscription';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy, AfterViewInit {
  loggedIn = false;
  notifications: Notification[] = [];
  unread = 0;
  private loginSub: Subscription;
  private notifySub: Subscription;
  notificationButton: any;

  constructor(private router: Router,
              private accountService: AccountService,
              private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.loggedIn = this.accountService.loggedIn;
    this.loginSub = this.accountService.loginEvent
      .subscribe((result) => {
        this.loggedIn = result;
      });

    this.notifySub = this.notificationService.getNotifySub()
      .subscribe((result) => {
        this.notifications = result.notifications;
        this.unread = result.unread;
      });

  }

  ngAfterViewInit() {
    this.accountService.loginEvent
      .subscribe((result) => {
        this.loggedIn = result;
      });
  }

  onNotificationClick(notification) {
    this.router.navigate(['events/' + notification.eventId])
      .then(() => {
        if (!notification.read) {
          this.notificationService.updateUserNotification(this.accountService.username, notification.eventId, {read: true})
            .then(() => {
              console.log('notification ' + notification.eventId + ' updated');
              this.notificationService.getUserNotifications(this.accountService.username);
            })
            .catch((error) => {
              console.log(error);
            });
        }
      });
  }

  onLogout() {
    this.accountService.logOut();
    this.router.navigate(['account/login']);

  }

  ngOnDestroy() {
    this.loginSub.unsubscribe();
    this.notifySub.unsubscribe();
  }

  onReadAllClick() {
    this.notificationService.updateAllUserNotifications(this.accountService.username, {read: true})
      .then((result) => {
        this.notificationService.getUserNotifications(this.accountService.username);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  onClickNotifications() {
    this.notificationButton = window.document.getElementById('notification');
    if (this.notificationButton.innerHTML === '<div>Notificaties</div>') {
      this.router.navigate(['account/notifications']);
    } else {
      // this.router.navigate(['account/notifications']);
    }
    // this.router.navigate(['account/notifications']);

  }
}
