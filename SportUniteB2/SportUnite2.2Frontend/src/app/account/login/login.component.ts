import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from "@angular/router";
import {AccountService} from "../../shared/services/account.service";
import {LocationService} from "../../shared/services/location.service";
import {Subscription} from "rxjs/Subscription";
import {AuthService, GoogleLoginProvider, FacebookLoginProvider} from "angular5-social-login";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  signinForm: FormGroup;
  wrongLogin: boolean;
  notExist: boolean;
  otherError: boolean;
  subscription: Subscription;

  constructor(private accountService: AccountService,
              private router: Router,
              private locationService: LocationService,
              private socialAuthService: AuthService) {
  }

  ngOnInit() {
    this.accountService.logOut();
    this.signinForm = new FormGroup({
      'username': new FormControl(null, [Validators.required]),
      'password': new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    (<HTMLInputElement>document.getElementById("username")).disabled = true;
    (<HTMLInputElement>document.getElementById("password")).disabled = true;
    (<HTMLButtonElement>document.getElementById("login")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
    (<HTMLButtonElement>document.getElementById("login")).disabled = true;
    (<HTMLButtonElement>document.getElementById("googleLogin")).disabled = true;
    (<HTMLButtonElement>document.getElementById("facebookLogin")).disabled = true;
    (<HTMLInputElement>document.getElementById("registreren")).disabled = true;

    this.accountService.login(this.signinForm.value.username, this.signinForm.value.password)
      .subscribe((response) => {
          this.accountService.loggedIn = true;
          this.accountService.token = response.json().token;
          this.accountService.username = response.json().user;
          this.accountService.getAccount(response.json().user)
            .subscribe((res) => {
              this.locationService.prefderedRadius = res.json().radius;
              this.router.navigate(['events']);
            })
        },
        (error) => {
          if (error.json().authorized === false) {
            this.accountService.loggedIn = false;
            this.wrongLogin = true;
          } else {
            this.accountService.loggedIn = false;
            this.otherError = true;
          }
          (<HTMLInputElement>document.getElementById("username")).disabled = false;
          (<HTMLInputElement>document.getElementById("password")).disabled = false;
          (<HTMLButtonElement>document.getElementById("login")).innerHTML = "Log in";
          (<HTMLButtonElement>document.getElementById("login")).disabled = false;
          (<HTMLButtonElement>document.getElementById("googleLogin")).disabled = false;
          (<HTMLButtonElement>document.getElementById("facebookLogin")).disabled = false;
          (<HTMLInputElement>document.getElementById("registreren")).disabled = false;
        });
  }

  public socialSignIn(socialPlatform: string) {
    let socialPlatformProvider;

    if (socialPlatform === 'google') {
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    } else if (socialPlatform === 'facebook') {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    }

    this.socialAuthService.signIn(socialPlatformProvider).then(
      (userData) => {

        (<HTMLInputElement>document.getElementById("username")).disabled = true;
        (<HTMLInputElement>document.getElementById("password")).disabled = true;
        (<HTMLButtonElement>document.getElementById("googleLogin")).disabled = true;
        (<HTMLButtonElement>document.getElementById("facebookLogin")).disabled = true;
        (<HTMLButtonElement>document.getElementById("login")).disabled = true;
        (<HTMLInputElement>document.getElementById("registreren")).disabled = true;

        if (socialPlatform === 'google') {
          (<HTMLButtonElement>document.getElementById("googleLogin")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Logging in...";
          this.accountService.token = userData.idToken;
        } else if (socialPlatform === 'facebook') {
          (<HTMLButtonElement>document.getElementById("facebookLogin")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Logging in...";
          this.accountService.token = userData.token;
        }

        this.accountService.socialUser = userData;

        console.log(this.accountService.socialUser);
        console.log(this.accountService.token);

        this.subscription = this.accountService.socialLogin()
          .subscribe(
            (response) => {
              if (response.json().length === 0) {
                this.router.navigate(['account/social']);
              } else {
                this.accountService.loggedIn = true;
                this.accountService.username = response.json().username;
                this.router.navigate(['events']);
              }
            },
            () => {
              (<HTMLInputElement>document.getElementById("username")).disabled = false;
              (<HTMLInputElement>document.getElementById("password")).disabled = false;
              (<HTMLButtonElement>document.getElementById("googleLogin")).disabled = false;
              (<HTMLButtonElement>document.getElementById("facebookLogin")).disabled = false;
              (<HTMLButtonElement>document.getElementById("googleLogin")).innerHTML = "Inloggen met Google";
              (<HTMLButtonElement>document.getElementById("facebookLogin")).innerHTML = "Inloggen met Facebook";
              (<HTMLButtonElement>document.getElementById("login")).disabled = false;
              (<HTMLInputElement>document.getElementById("registreren")).disabled = false;
              this.subscription.unsubscribe();
            },
            () => {
              this.subscription.unsubscribe();
            });
      });
  }

  onClick() {
    this.router.navigate(['account/registration']);
  }
}
