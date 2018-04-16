import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {AccountComponent} from './account/account.component';
import {EventComponent} from './event/event.component';
import {EventDetailComponent} from './event/event-list/event-detail/event-detail.component';
import {EventFormComponent} from './event/event-form/event-form.component';
import {ErrorPageComponent} from './error-page/error-page.component';
import {UserComponent} from './user/user.component';
import {UserDetailComponent} from './user/user-list/user-detail/user-detail.component';
import {AuthGuardGuard} from './shared/guards/auth-guard.guard';
import {RegistrationComponent} from './account/registration/registration.component';
import {ProfileComponent} from './account/profile/profile.component';
import {SettingsComponent} from './account/settings/settings.component';
import {LoginComponent} from './account/login/login.component';

import {NotificationsComponent} from "./account/notifications/notifications.component";
import {SocialRegistrationComponent} from "./account/socialregistration/socialregistration.component";


const appRoutes: Routes = [
  {path: '', redirectTo: '/account/login', pathMatch: 'full'},
  {
    path: 'account', component: AccountComponent, children: [
      {path: 'login', component: LoginComponent},
      {path: 'registration', component: RegistrationComponent},
      {path: 'profile', canActivate: [AuthGuardGuard], component: ProfileComponent},
      {path: 'notifications', canActivate: [AuthGuardGuard], component: NotificationsComponent},
      {path: 'settings', canActivate: [AuthGuardGuard], component: SettingsComponent},
      {path: 'social', component: SocialRegistrationComponent}

    ]
  },

  {path: 'events/new', component: EventFormComponent, canActivate: [AuthGuardGuard], pathMatch: 'full'},
  {path: 'events', component: EventComponent, canActivate: [AuthGuardGuard], pathMatch: 'full'},
  {path: 'events/:id', component: EventDetailComponent, canActivate: [AuthGuardGuard], pathMatch: 'full'},
  {path: 'users', component: UserComponent, canActivate: [AuthGuardGuard], pathMatch: 'full'},

  {path: 'users/:id', component: UserDetailComponent, canActivate: [AuthGuardGuard], pathMatch: 'full'},
  {path: 'not-found', component: ErrorPageComponent, data: {message: 'Page not found!'}},
  {path: '**', redirectTo: '/not-found'}
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
