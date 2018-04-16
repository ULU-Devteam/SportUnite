import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {Router} from '@angular/router';
import {NotificationService} from '../../shared/services/notification.service';
import {AccountService} from '../../shared/services/account.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit, OnDestroy {
  notifications: Notification[] = [];
  unread = 0;
  private notifySub: Subscription;

  constructor(private router: Router,
              private accountService: AccountService,
              private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.notifySub = this.notificationService.getNotifySub()
      .subscribe((result) => {
        this.notifications = result.notifications;
        this.unread = result.unread;
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

  onReadAllClick() {
    this.notificationService.updateAllUserNotifications(this.accountService.username, {read: true})
      .then((result) => {
        this.notificationService.getUserNotifications(this.accountService.username);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  ngOnDestroy() {
    this.notifySub.unsubscribe();
  }

}
