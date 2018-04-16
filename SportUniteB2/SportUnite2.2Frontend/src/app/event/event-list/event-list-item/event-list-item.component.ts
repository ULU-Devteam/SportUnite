import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Event } from '../../../shared/models/event.model';
import {LocationService} from "../../../shared/services/location.service";

@Component({
  selector: 'app-event-list-item',
  templateUrl: './event-list-item.component.html',
  styleUrls: ['./event-list-item.component.css']
})
export class EventListItemComponent implements OnInit {
  @Input() event;
  @Input() index: Number;
  private lat: number;
  private long: number;
  private distanceToUser: number;
  inRadius: boolean = false;
  displayDistance: number;
  allowed = this.locationService.locationAllowed;
  private address: string;
  private street: string;
  private postalCode: string;
  private city: string;



  constructor(private router: Router,
              private route: ActivatedRoute, private locationService: LocationService) { }

  ngOnInit() {
    if (this.locationService.locationAllowed) {
      this.distanceToUser = this.locationService.calculateDistance(this.event.lat, this.event.long, this.locationService.lat, this.locationService.long);
      this.inRadius = this.distanceToUser < this.locationService.prefderedRadius;
      this.displayDistance = Number((this.distanceToUser).toFixed(1));
    } else {
      this.inRadius = true;
    }


    this.locationService.getAddress(this.event.sportcomplex.addressId)
      .subscribe((res) => {
        this.address = res.json().street + ' ' + res.json().houseNumber + ' ' + res.json().postalCode + ' ' + res.json().city;
        this.street = res.json().street + ' ' + res.json().houseNumber;
        this.postalCode = res.json().postalCode + ' ' + res.json().city;
        this.city =  res.json().city;
        if(!this.searchList(res.json().city)){
          this.locationService.addCity(res.json().city)
        }
      });

  }

  onEventClicked() {
    this.router.navigate([this.event.eventId], {relativeTo: this.route});
  }

  searchList(city: string) {
    for(let c of this.locationService.addressList){
      if (c === city){
        return true
      }
    }
    return false;
  }

}
