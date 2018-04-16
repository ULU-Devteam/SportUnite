import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../../shared/models/user.model';
import {Router} from "@angular/router";
import {AccountService} from "../../shared/services/account.service";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  signupForm: FormGroup;
  repeatPasswordCheck: boolean;
  usernameExist: boolean;
  loggedIn = false;

  constructor(private accountService: AccountService,
              private router: Router) {
  }

  ngOnInit() {
    this.loggedIn = this.accountService.loggedIn;
    this.signupForm = new FormGroup({
        'name': new FormControl(null, [Validators.required]),
        'age': new FormControl(null, [Validators.required]),
        'email': new FormControl(null, [Validators.required, Validators.email]),
        'city': new FormControl(null, [Validators.required]),
        'usernameR': new FormControl(null, [Validators.required]),
        'passwordR': new FormControl(null, [Validators.required]),
        'repeatpassword': new FormControl(null, [Validators.required])
      },
      {
        validators: this.passwordEqual.bind(this)
      });
  }

  onSubmit() {
    (<HTMLInputElement>document.getElementById("name")).disabled = true;
    (<HTMLInputElement>document.getElementById("age")).disabled = true;
    (<HTMLInputElement>document.getElementById("email")).disabled = true;
    (<HTMLInputElement>document.getElementById("city")).disabled = true;
    (<HTMLInputElement>document.getElementById("usernameR")).disabled = true;
    (<HTMLInputElement>document.getElementById("passwordR")).disabled = true;
    (<HTMLInputElement>document.getElementById("repeatpassword")).disabled = true;
    (<HTMLButtonElement>document.getElementById("register")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
    (<HTMLButtonElement>document.getElementById("register")).disabled = true;
    const values = this.signupForm.value;
    const user = new User(values.usernameR, values.passwordR, values.name, values.age, values.email, values.city);
    this.accountService.register(user)
      .then(() => {
        this.accountService.login(values.usernameR, values.passwordR)
          .subscribe((response) => {
              this.accountService.loggedIn = true;
              this.accountService.token = response.json().token;
              this.accountService.username = response.json().user;
              this.router.navigate(['events']);
            }
          );
      })
      .catch((error) => {
        this.usernameExist = true;
        (<HTMLInputElement>document.getElementById("name")).disabled = false;
        (<HTMLInputElement>document.getElementById("age")).disabled = false;
        (<HTMLInputElement>document.getElementById("email")).disabled = false;
        (<HTMLInputElement>document.getElementById("city")).disabled = false;
        (<HTMLInputElement>document.getElementById("usernameR")).disabled = false;
        (<HTMLInputElement>document.getElementById("passwordR")).disabled = false;
        (<HTMLInputElement>document.getElementById("repeatpassword")).disabled = false;
        (<HTMLButtonElement>document.getElementById("register")).innerHTML = "Registreren";
        (<HTMLButtonElement>document.getElementById("register")).disabled = false;
      });
  }

  ownAccount() {
    return window.location.href.indexOf('account/settings') !== -1;
  }

  passwordEqual(form: FormControl) {
    if (form.value.repeatpassword === form.value.passwordR) {
      this.repeatPasswordCheck = false;
      return null;
    } else {
      this.repeatPasswordCheck = true;
    }
    return {'password': true};
  }
  onClick(){
    this.router.navigate(['account/login']);
  }
}
