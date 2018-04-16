import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {User} from "../../shared/models/user.model";
import {AccountService} from "../../shared/services/account.service";
import {Router} from "@angular/router";
import {LocationService} from "../../shared/services/location.service";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  profileForm: FormGroup;
  repeatPasswordCheck: boolean;
  editUser: User;
  loggedIn = false;
  submitted = false;

  constructor(private accountService: AccountService,
              private router: Router,
              private locationService: LocationService) {
  }

  ngOnInit() {
    this.loggedIn = this.accountService.loggedIn;

    this.accountService.getAccount(this.accountService.username)
      .subscribe((account) => {
        this.editUser = account.json();
        setTimeout(() => {
          this.profileForm.setValue({
            name: this.editUser.name,
            age: this.editUser.age,
            email: this.editUser.email,
            city: this.editUser.city,
            radius: this.editUser.radius || 0
          });
        }, 1)
      });

    this.profileForm = new FormGroup({
        'name': new FormControl(null, [Validators.required]),
        'age': new FormControl(null, [Validators.required]),
        'email': new FormControl(null, [Validators.required, Validators.email]),
        'city': new FormControl(null, [Validators.required]),
        'radius': new FormControl(null, [Validators.required])
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
    (<HTMLButtonElement>document.getElementById("edit")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
    (<HTMLButtonElement>document.getElementById("edit")).disabled = true;

    (<HTMLButtonElement>document.getElementById("radius")).disabled = true;

    const values = this.profileForm.value;
    this.accountService.updateAccount(this.editUser.username, values)

      .subscribe((response) =>
        {
          this.accountService.getAccount(this.accountService.username)
            .subscribe((res) => {
              this.locationService.prefderedRadius = res.json().radius;
              this.submitted = true;
              this.router.navigate(['events']);
            })

        }
      ),
      (error) => {
        (<HTMLInputElement>document.getElementById("name")).disabled = false;
        (<HTMLInputElement>document.getElementById("age")).disabled = false;
        (<HTMLInputElement>document.getElementById("email")).disabled = false;
        (<HTMLInputElement>document.getElementById("city")).disabled = false;
        (<HTMLButtonElement>document.getElementById("edit")).innerHTML = "Wijzigingen opslaan";
        (<HTMLButtonElement>document.getElementById("edit")).disabled = false;
      }
  }


  onCancel(){
    this.router.navigate(['events']);
  }


  passwordEqual(form: FormControl) {
    if (form.value.repeatpassword === form.value.passwordR) {
      this.repeatPasswordCheck = false;
      return null;
    } else {
      this.repeatPasswordCheck = true;
    }
    return {'password': true}
  }


}
