import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {AccountService} from '../services/account.service';
import {NotificationService} from '../services/notification.service';

@Injectable()
export class AuthGuardGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router, private notificationService: NotificationService) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    console.log(this.accountService.loggedIn)
    if (this.accountService.loggedIn) {
      this.notificationService.getUserNotifications(this.accountService.username);

      return true;
    } else {
      this.router.navigate(['account/login']);
      return false;
    }
  }
}
