import {EventEmitter, Injectable} from "@angular/core";
import {Http} from "@angular/http";
import {UrlConfig} from "../config/url.config";

@Injectable()
export class LocationService {
  addresslist(arg0: any): any {
    throw new Error("Method not implemented.");
  }

  get locationAllowed(): boolean {
    return this._locationAllowed;
  }

  set locationAllowed(value: boolean) {
    this._locationAllowed = value;
  }
  get prefderedRadius(): number {
    return this._prefderedRadius;
  }

  set prefderedRadius(value: number) {
    this._prefderedRadius = value;
  }
  get lat(): number {
    return this._lat;
  }

  set lat(value: number) {
    this._lat = value;
  }
  get long(): number {
    return this._long;
  }

  set long(value: number) {
    this._long = value;
  }

  constructor(private http: Http, private config: UrlConfig){}

  private _long: number;
  private _lat: number;
  private _prefderedRadius: number = 15;
  private _locationAllowed: boolean = false;
  addressList = [];
  addresslistEvent= new EventEmitter<any>();


  getLocation() {
        navigator.geolocation.getCurrentPosition((position) => {
          this._lat = position.coords.latitude;
          this._long = position.coords.longitude
          this.locationAllowed = true;
         }, (error) => {
          this.locationAllowed = false
        });
  }

  addCity(city: string) {
    this.addressList.push(city);
    this.addresslistEvent.emit(this.addressList);
    console.log(this.addressList)
  }


    geoCode(address){
      return this.http.get('https://maps.googleapis.com/maps/api/geocode/json?address=' + address + ' &key=AIzaSyDy4X1dyHXjG-qPtiJ6j9uOD2ns3-a5iNM')

    }

    getAddress(id) {
      return this.http.get('https://sportuniteapi.azurewebsites.net/api/address/'  + id);
    }




   degreesToRadians(degrees) {
    return degrees * Math.PI / 180;
  }

   calculateDistance(lat1, lon1, lat2, lon2) {
      this.getLocation()
    var earthRadiusKm = 6371;

    var dLat = this.degreesToRadians(lat2-lat1);
    var dLon = this.degreesToRadians(lon2-lon1);

    lat1 = this.degreesToRadians(lat1);
    lat2 = this.degreesToRadians(lat2);

    var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
      Math.sin(dLon/2) * Math.sin(dLon/2) * Math.cos(lat1) * Math.cos(lat2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    return earthRadiusKm * c;
  }
  }
