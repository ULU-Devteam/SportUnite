import {Component, Input, OnInit} from '@angular/core';
import {AccountService} from '../../../shared/services/account.service';
import {User} from '../../../shared/models/user.model';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.css']
})
export class ProfileDetailComponent implements OnInit {
  user: User;
  @Input() username: string;
  loaded = false;

  constructor(private accountService: AccountService) {
  }

  ngOnInit() {
    this.accountService.getAccount(this.username)
      .subscribe((account) => {
        this.user = account.json();
        this.loaded = true;
      });
  }

}
