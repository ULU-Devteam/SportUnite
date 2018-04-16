import {Component, OnInit} from '@angular/core';
import {LocationService} from "../shared/services/location.service";
import {EventService} from "../shared/services/event.service";
import { Sport } from '../shared/models/sport.model';
import { EventState } from '../shared/states/event.state';


@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {

  events: Event[];
  sportComplexes;
  sports: Sport[];
  sport: Sport;
  allowed = this.locationService.locationAllowed;
  undefined;
  private addresses;

  constructor(private eventService: EventService,
              private eventState: EventState,
              private locationService: LocationService) {
  }

  ngOnInit() {
    this.eventState.getEvents();
    this.getComplexes();
    this.getSports();
    this.locationService.getLocation();
    this.addresses = this.locationService.addressList
    this.locationService.addresslistEvent
      .subscribe((list) => {
        this.addresses = list;
      });
  }

  getComplexes() {
    this.eventService.getComplex()
      .subscribe((sportcomplexes) => {
          this.sportComplexes = sportcomplexes.json()._embedded.sportComplexes;
        },
        (error) => {
        });
  }

  getSports() {
    this.eventService.getSports()
      .subscribe((sports) => {
        this.sports = sports.json();
      },
      (error) => {
      });
  }

  onShowAllEvents() {
    this.eventState.getEvents();
  }

  onSearch(sport, complex, city, query) {
    console.log(sport, complex, city, query);
    this.eventState.getQueriedEvents(query);
    this.eventState.filterEvents(sport, complex, city);
  }

  sortByNameAsc() {
    console.log('ASC');
    this.eventState.sortedByLocationAsc();
  }

  sortByNameDesc() {
    console.log('DESC');
    this.eventState.sortedByLocationDesc();
  }

  sortByLocationFar() {
    console.log('DISTANCE furthest');
    this.eventState.sortedByDistanceFar();
  }

  sortByLocationNear() {
    console.log('DISTANCE nearest');
    this.eventState.sortedByDistanceNear();
  }

}
