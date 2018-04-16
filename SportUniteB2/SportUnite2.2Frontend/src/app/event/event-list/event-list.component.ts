import {Component, OnInit, Input, OnDestroy} from '@angular/core';
import {Event} from '../../shared/models/event.model';
import {EventService} from '../../shared/services/event.service';
import {Subscription} from 'rxjs/Subscription';
import {Sport} from '../../shared/models/sport.model';
import {EventState} from '../../shared/states/event.state';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit, OnDestroy {
  private subscription: Subscription;
  private subjectSubscription: Subscription;

  events;
  noEvents = false;
  loaded = false;
  error = false;


  constructor(private eventService: EventService,
              private eventState: EventState) {
  }

  ngOnInit() {

    this.events = [];

    this.subscription = this.eventService.getEvents()
      .subscribe(
        (response) => {
          console.log(response.json());
          response.json().forEach((entry) => {
            let event = entry.event;

              let newEvent = new Event(
                event.eventId, event.name, event.peopleAmount, event.archived, [], [],
                entry.sportcomplex, entry.date, entry.long, entry.lat);

            event.sports.forEach((sport) => {
              let newSport = new Sport(sport.sportId, sport.type, sport.archived);
              newEvent.sports.push(newSport);
            });

            entry.usernames.forEach((user) => {
              newEvent.usernames.push({username: user.username});
            });

              this.events.push(newEvent);
              this.loaded = true;

          });

        },
        (error) => {
          this.loaded = true;
          this.error = true;
          this.subscription.unsubscribe();
        },
        () => {
          this.loaded = true;
          this.subscription.unsubscribe();
          this.eventState.setEvents(this.events);
        }
      );

    this.subjectSubscription = this.eventState.eventsChanged
      .subscribe((events) => {
        this.events = events;
      });
  }

  ngOnDestroy() {

    if (this.subjectSubscription !== undefined) {
      this.subjectSubscription.unsubscribe();
    }

  }

}
