
import {Component, OnInit} from '@angular/core';
import {EventService} from '../../../shared/services/event.service';
import {Router, ActivatedRoute} from '@angular/router';
import {Event} from '../../../shared/models/event.model';
import {EventState} from '../../../shared/states/event.state';
import {AccountService} from '../../../shared/services/account.service';
import {LocationService} from '../../../shared/services/location.service';
import {NotificationService} from '../../../shared/services/notification.service';
import {UserService} from '../../../shared/services/user.service';


@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit {
  message = 'Kom je mee sporten?';
  id: number;

  event;
  joinedEvent: boolean;
  canJoin: boolean;
  address: string;
  private street: string;
  private postalCode: string;
  private city: string;


  constructor(private eventService: EventService,
              private router: Router,
              private route: ActivatedRoute,
              private eventState: EventState,

              private accountService: AccountService,
              private locationService: LocationService,
              private notificationService: NotificationService,
              private userService: UserService) {

  }

  ngOnInit() {
    this.route.params.subscribe(
      (params) => {
        this.id = +params.id;
        this.eventService.getEvent(this.id)
          .toPromise()
          .then((response) => {
            console.log(response.json());
            this.event = response.json().event;
            this.event.usernames = response.json().usernames;
            this.event.sportcomplex = response.json().sportcomplex;
            this.joinedEvent = !!this.event.usernames.find(x => x.username === this.accountService.username);
            console.log(this.event);
            this.loadEvent();
          }).catch((error) => {
          console.log('getEvent Error: ' + error);
        });
      });

  }

  loadEvent() {
    this.canJoin = this.event.usernames.length < this.event.peopleAmount;

    console.log(this.event);
    this.locationService.getAddress(this.event.sportcomplex.addressId)
      .toPromise()
      .then((res) => {
        this.address = res.json().street + ' ' + res.json().houseNumber + ' ' + res.json().city;
        this.street = res.json().street + ' ' + res.json().houseNumber;
        this.postalCode = res.json().postalCode + ' ' + res.json().city;
        this.city = res.json().city;
      })
      .catch((error) => {
        console.log(error);
      });

    this.joinedEvent = !!this.event.usernames.find(x => x.username === this.accountService.username);

  }

  joinEvent() {

    if (!this.event.usernames.find(x => x.username === this.accountService.username) && this.canJoin) {
      const updatedEvent = this.event;

      updatedEvent.usernames.push({username: this.accountService.username});

      this.eventService.joinEvent(this.id, updatedEvent)
        .toPromise()
        .then((result) => {
          console.log(result.json());
          this.joinedEvent = true;
          this.event.usernames = result.json().usernames;
          this.canJoin = this.event.usernames.length < this.event.peopleAmount;
        })
        .catch((error) => {
          console.log(error);
        });

    }
  }

  leaveEvent() {
    const updatedEvent = this.event;
    const updatedEventId = updatedEvent.usernames.findIndex(x => x.username === this.accountService.username);
    updatedEvent.usernames.splice(updatedEventId, 1);

    this.eventService.joinEvent(this.event.eventId, updatedEvent)
      .toPromise()
      .then((result) => {
        this.joinedEvent = false;
        this.event.usernames = result.json().usernames;
        this.canJoin = this.event.usernames.length < this.event.peopleAmount;
      })
      .catch((error) => {
        console.log(error);
      });
  }

  onUserClicked(username: string) {
    this.router.navigate(['users/' + username])
      .catch((error) => {
        console.log(error);
      });
  }

  onShareEvent(closemodal) {
    this.userService.getFriendsSubject()
      .subscribe((friends) => {
        const friendnames = [];
        friends.forEach(x => {
          friendnames.push(x.username);
        });
        this.notificationService.postEventNotifications(this.accountService.username, this.id.toString(), friendnames, this.message)
          .then(() => {
            closemodal.click();
          })
          .catch((error) => {
            console.log(error);
          });
      });
    this.userService.refreshFriends(this.accountService.username);
  }


}
