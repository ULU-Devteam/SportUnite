import { Component, OnInit } from '@angular/core';
import {AccountService} from '../../shared/services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  username: string;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.username = this.accountService.username;
  }

}
