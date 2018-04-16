import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AccountService} from "../../shared/services/account.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs/Subscription";
import {User} from "../../shared/models/user.model";

@Component({
  selector: 'app-googleregistration',
  templateUrl: './socialregistration.component.html',
  styleUrls: ['./socialregistration.component.css']
})
export class SocialRegistrationComponent implements OnInit {
  private googleRegistration: boolean = false;
  private facebookRegistration: boolean = false;
  private signupForm: FormGroup;
  private subscription: Subscription;
  private usernameExist: boolean = false;
  private socialUser;

  constructor(private accountService: AccountService,
              private router: Router) {
  }

  ngOnInit() {
    this.socialUser = this.accountService.socialUser;

    if (this.socialUser.provider === 'facebook') {
      this.facebookRegistration = true;
    } else if (this.socialUser.provider === 'google') {
      this.googleRegistration = true;
    }

    this.signupForm = new FormGroup({
      'name': new FormControl(null, [Validators.required]),
      'age': new FormControl(null, [Validators.required]),
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'city': new FormControl(null, [Validators.required]),
      'usernameR': new FormControl(null, [Validators.required])
    });

    this.signupForm.setValue({
      name: this.socialUser.name,
      age: 0,
      email: this.socialUser.email,
      city: '',
      usernameR: ''
    });
  }

  onSubmit() {
    (<HTMLInputElement>document.getElementById("name")).disabled = true;
    (<HTMLInputElement>document.getElementById("age")).disabled = true;
    (<HTMLInputElement>document.getElementById("email")).disabled = true;
    (<HTMLInputElement>document.getElementById("city")).disabled = true;
    (<HTMLInputElement>document.getElementById("usernameR")).disabled = true;
    (<HTMLButtonElement>document.getElementById("register")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
    (<HTMLButtonElement>document.getElementById("register")).disabled = true;

    const values = this.signupForm.value;
    const user = new User(values.usernameR, values.passwordR, values.name, values.age, values.email, values.city);
    this.subscription = this.accountService.socialRegister(user)
      .subscribe(
        (response) => {
          this.accountService.loggedIn = true;
          this.accountService.username = response.json().username;
          this.router.navigate(['events']);
        },
        () => {
          this.usernameExist = true;
          this.subscription.unsubscribe();
          (<HTMLInputElement>document.getElementById("name")).disabled = false;
          (<HTMLInputElement>document.getElementById("age")).disabled = false;
          (<HTMLInputElement>document.getElementById("email")).disabled = false;
          (<HTMLInputElement>document.getElementById("city")).disabled = false;
          (<HTMLInputElement>document.getElementById("usernameR")).disabled = false;
          (<HTMLButtonElement>document.getElementById("register")).innerHTML = "Registreren";
          (<HTMLButtonElement>document.getElementById("register")).disabled = false;
        },
        () => {
          this.subscription.unsubscribe();
        });
  }

  onLoginClick() {
    this.router.navigate(['events']);
  }

}
