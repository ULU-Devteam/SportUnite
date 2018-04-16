import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { AccountComponent } from './account/account.component';
import { LoginComponent } from './account/login/login.component';
import { RegistrationComponent } from './account/registration/registration.component';
import {AppRoutingModule} from "./app-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { EventComponent } from './event/event.component';
import { EventListComponent } from './event/event-list/event-list.component';
import { EventListItemComponent } from './event/event-list/event-list-item/event-list-item.component';
import { EventDetailComponent } from './event/event-list/event-detail/event-detail.component';
import { EventFormComponent } from './event/event-form/event-form.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import {AccountService} from "./shared/services/account.service";
import {HttpModule} from "@angular/http";
import {EventService} from './shared/services/event.service';
import { UserComponent } from './user/user.component';
import { FriendListComponent } from './account/profile/friend-list/friend-list.component';
import { FriendListItemComponent } from './account/profile/friend-list/friend-list-item/friend-list-item.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserListItemComponent } from './user/user-list/user-list-item/user-list-item.component';
import { UserDetailComponent } from './user/user-list/user-detail/user-detail.component';
import {AuthGuardGuard} from "./shared/guards/auth-guard.guard";
import {EventState} from "./shared/states/event.state";
import { ProfileComponent } from './account/profile/profile.component';
import { ProfileDetailComponent } from './account/profile/profile-detail/profile-detail.component';
import { SettingsComponent } from './account/settings/settings.component';
import {UserService} from './shared/services/user.service';
import {SocialRegistrationComponent} from "./account/socialregistration/socialregistration.component";
import {SocialLoginModule, AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider} from "angular5-social-login";
import { NotificationsComponent } from './account/notifications/notifications.component';
import { NotificationDirective } from './shared/directives/notification.directive';
import {UrlConfig} from "./shared/config/url.config";
import {LocationService} from "./shared/services/location.service";
import {NotificationService} from './shared/services/notification.service';


export function getAuthServiceConfigs() {
  let config = new AuthServiceConfig(
    [
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider("328216757674961")
      },
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider("658702919219-1fpbt81i71q88mkhv9phslmi213fah6s.apps.googleusercontent.com")
      }
    ]);
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    AccountComponent,
    LoginComponent,
    RegistrationComponent,
    EventComponent,
    EventListComponent,
    EventListItemComponent,
    EventDetailComponent,
    EventFormComponent,
    ErrorPageComponent,
    UserComponent,
    FriendListComponent,
    FriendListItemComponent,
    UserListComponent,
    UserListItemComponent,
    UserDetailComponent,
    ProfileComponent,
    ProfileDetailComponent,
    SettingsComponent,
    SocialRegistrationComponent,
    NotificationsComponent,
    NotificationDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpModule,
    FormsModule,
    SocialLoginModule
  ],

  providers: [AccountService, EventService, AuthGuardGuard, EventState, UserService, {provide: AuthServiceConfig, useFactory: getAuthServiceConfigs}, LocationService, UrlConfig, NotificationService],

  bootstrap: [AppComponent]
})
export class AppModule {
}


