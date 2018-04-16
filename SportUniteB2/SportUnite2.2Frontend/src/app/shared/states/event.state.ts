import {Injectable} from "@angular/core";
import {Subject} from "rxjs/Subject";
import {Event} from "../models/event.model"
import { Sport } from '../models/sport.model';
import { LocationService } from '../services/location.service';

@Injectable()
export class EventState {
  private events: Event[];
  private queriedEvents: Event[];
  public eventsChanged;

  constructor(private locationService: LocationService) {
    this.events = [];
    this.eventsChanged = new Subject<Event[]>();
  }

  getEvent(index: number): Event {
    return this.events[index];
  }

  setEvents(events: Event[]) {
    this.events = events;
    this.eventsChanged.next(this.events.slice());
  }

  updateEvent(index: number , event: Event) {
    this.events.splice(index, 1, event);
    this.eventsChanged.next(this.events.slice());
  }

  getEvents() {
    this.queriedEvents = this.events;
    this.eventsChanged.next(this.queriedEvents.slice());
  }

  //////////////////////////
  // Filter & sort events //
  //////////////////////////
  //  Remove Console.log  //
  //////////////////////////

  getQueriedEvents(query) {
    this.queriedEvents = this.events.filter(val => {
      return val.name.match(query);
    });
    this.eventsChanged.next(this.queriedEvents);
  }

  filterEvents(sport, complex, city) {
    console.log(sport);
    console.log(this.queriedEvents);
    if (sport) {
      let filteredSport = [];
      this.queriedEvents.forEach(event => {
        event.sports.forEach(eventSport => {
          if (eventSport.sportId === sport.sportId) {
            console.log(eventSport);
            filteredSport.push(event);
          }
        });
      });
      this.queriedEvents = filteredSport;
      this.eventsChanged.next(this.queriedEvents.slice());
      console.log(this.queriedEvents);
    }
    if (complex) {
      let filteredComplex = [];
      this.queriedEvents.forEach(event => {
        if (event.sportcomplex.name.toLowerCase() === complex.name.toLowerCase()) {
          console.log(event);
          filteredComplex.push(event);
        }
      });
      this.queriedEvents = filteredComplex;
      console.log(this.queriedEvents);
      this.eventsChanged.next(this.queriedEvents.slice());
    }
    if (city) {
      let filteredLocation = [];
      this.queriedEvents.forEach(event => {
        console.log(event.sportcomplex);
        this.locationService.getAddress(event.sportcomplex.addressId)
          .subscribe(
            (response: any) => {
              console.log(response.json());
              if (response.json().city === city) {
                console.log(event);
                filteredLocation.push(event);
                this.queriedEvents = filteredLocation;
                console.log(this.queriedEvents);
                this.eventsChanged.next(this.queriedEvents.slice());
              }
            }
          );
      });
    }
  }

  sortedByLocationAsc() {
    console.log('SORTING');
    console.log(this.queriedEvents);
    if (!this.queriedEvents) {
      this.queriedEvents = this.events;
      console.log(this.queriedEvents);
      this.sortAsc(this.queriedEvents);
    } else {
      this.sortAsc(this.queriedEvents);
    }
  }

  sortedByLocationDesc() {
    console.log('SORTING');
    console.log(this.queriedEvents);
    if (!this.queriedEvents) {
      this.queriedEvents = this.events;
      console.log(this.queriedEvents);
      this.sortDesc(this.queriedEvents);
    } else {
      this.sortDesc(this.queriedEvents);
    }
  }

  sortAsc(array: any[]) {
    array.sort((a, b) => {
      if (a.name.toLowerCase() < b.name.toLowerCase()) {
        return -1;
      }
      if (a.name.toLowerCase() > b.name.toLowerCase()) {
        return 1;
      }
      return 0;
    });
    this.queriedEvents = array;
    console.log(this.queriedEvents);
    this.eventsChanged.next(this.queriedEvents);
  }

  sortDesc(array: any[]) {
    array.sort((a, b) => {
      if (a.name.toLowerCase() < b.name.toLowerCase()) {
        return 1;
      }
      if (a.name.toLowerCase() > b.name.toLowerCase()) {
        return -1;
      }
      return 0;
    });
    this.queriedEvents = array;
    console.log(this.queriedEvents);
    this.eventsChanged.next(this.queriedEvents);
  }

  sortedByDistanceFar() {
    console.log('FURTHEST');
    console.log(this.locationService.lat, this.locationService.long);
    if (!this.queriedEvents) {
      this.queriedEvents = this.events;
      this.sortFurthest(this.queriedEvents);
    } else {
      this.sortFurthest(this.queriedEvents);
    }
  }

  sortedByDistanceNear() {
    console.log('NEAREST');
    console.log(this.locationService.lat, this.locationService.long);
    if (!this.queriedEvents) {
      this.queriedEvents = this.events;
      this.sortByNearest(this.queriedEvents);
    } else {
      this.sortByNearest(this.queriedEvents);
    }
  }

  sortFurthest(array: any) {
    let distances = [];
    let sortByFurthest = [];

    array.forEach(event => {
      let calculatedDistance = this.locationService.calculateDistance(event.lat, event.long, this.locationService.lat, this.locationService.long);
      let distance = Number((calculatedDistance).toFixed(1));
      let eventDistance = {
          event: event,
          distance: distance
        };
      distances.push(eventDistance);
    });
    console.log(distances);
    distances.sort((a, b) => {
      if (a.distance < b.distance) {
        return 1;
      }
      if (a.distance > b.distance) {
        return -1;
      }
      return 0;
    });
    console.log(distances);
    distances.forEach(eventDistance => {
      sortByFurthest.push(eventDistance.event);
    });
    console.log(sortByFurthest);
    this.queriedEvents = sortByFurthest;
    this.eventsChanged.next(this.queriedEvents.slice());
  }

  sortByNearest(array: any) {
    let distances = [];
    let sortByNearest = [];

    array.forEach(event => {
      let calculatedDistance = this.locationService.calculateDistance(event.lat, event.long, this.locationService.lat, this.locationService.long);
      let distance = Number((calculatedDistance).toFixed(1));
      let eventDistance = {
        event: event,
        distance: distance
      };
      distances.push(eventDistance);
    });
    console.log(distances);
    distances.sort((a, b) => {
      if (a.distance < b.distance) {
        return -1;
      }
      if (a.distance > b.distance) {
        return 1;
      }
      return 0;
    });
    console.log(distances);
    distances.forEach(eventDistance => {
      sortByNearest.push(eventDistance.event);
    });
    console.log(sortByNearest);
    this.queriedEvents = sortByNearest;
    this.eventsChanged.next(this.queriedEvents.slice());
  }

}
