import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import {EventService} from "../../shared/services/event.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.css']
})
export class EventFormComponent implements OnInit {
  minValue : boolean;
  eventForm: FormGroup;
  sportComplexes;
  sports;
  selectedSport;
  submitted: boolean = false;


  constructor(private eventService: EventService, private router: Router) {}

  ngOnInit() {
    this.eventForm = new FormGroup({
      'name': new FormControl(null, [Validators.required]),
      'persons': new FormControl(null, [Validators.required]),
      'date': new FormControl(null, [Validators.required]),
      'sport': new FormControl(null, [Validators.required]),
      'sportcomplexes': new FormControl(null, [Validators.required]),
    },
      {
        validators: this.minimalValue.bind(this)
      });


    this.getComplexes();
    this.getSports();
  }

  onSubmit() {
    (<HTMLInputElement>document.getElementById("name")).disabled = true;
    (<HTMLInputElement>document.getElementById("persons")).disabled = true;
    (<HTMLInputElement>document.getElementById("date")).disabled = true;
    (<HTMLInputElement>document.getElementById("sportcomplexes")).disabled = true;
    (<HTMLInputElement>document.getElementById("sport")).disabled = true;
    (<HTMLButtonElement>document.getElementById("newEvent")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
    (<HTMLButtonElement>document.getElementById("newEvent")).disabled = true;
    this.submitted = true;
    const event = {
      "name": this.eventForm.value.name,
      "peopleAmount": this.eventForm.value.persons,
      "archived": false,
      "sportIds": [
        this.eventForm.value.sport.sportId
      ]
    }
    this.eventService.createEvent(event)
      .subscribe((res) => {

        const mongoevent = {
          "eventId" : res.json().eventId,
          "date" : this.eventForm.value.date,
          "sportcomplex": this.eventForm.value.sportcomplexes,
        };


        this.eventService.createEventMongo(mongoevent)
          .then((res2) => {
            this.router.navigate(['/events']);
          }),
          (error) => {
            (<HTMLInputElement>document.getElementById("name")).disabled = false;
            (<HTMLInputElement>document.getElementById("persons")).disabled = false;
            (<HTMLInputElement>document.getElementById("date")).disabled = false;
            (<HTMLInputElement>document.getElementById("sportcomplexes")).disabled = false;
            (<HTMLInputElement>document.getElementById("sport")).disabled = false;
            (<HTMLButtonElement>document.getElementById("newEvent")).innerHTML = "Evenement aanmaken";
            (<HTMLButtonElement>document.getElementById("newEvent")).disabled = false;
          }


      }),
      (error) => {
        (<HTMLInputElement>document.getElementById("name")).disabled = false;
        (<HTMLInputElement>document.getElementById("persons")).disabled = false;
        (<HTMLInputElement>document.getElementById("date")).disabled = false;
        (<HTMLInputElement>document.getElementById("sportcomplexes")).disabled = false;
        (<HTMLInputElement>document.getElementById("sport")).disabled = false;
        (<HTMLButtonElement>document.getElementById("newEvent")).innerHTML = "Evenement aanmaken";
        (<HTMLButtonElement>document.getElementById("newEvent")).disabled = false;
    }

  }

  getComplexes() {
    this.eventService.getComplex()
      .subscribe((sportcomplexes) => {
        this.sportComplexes = sportcomplexes.json()._embedded.sportComplexes;
        (<HTMLButtonElement>document.getElementById("sportcomplexes")).disabled = false;
      }),
      (error)=> {
        (<HTMLButtonElement>document.getElementById("sportcomplexes")).disabled = true;
        (<HTMLButtonElement>document.getElementById("sportcomplexes")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
      }
  }


  getSports(){
    this.eventService.getSports()
      .subscribe((sports) => {
        (<HTMLButtonElement>document.getElementById("sport")).disabled = false;
        this.sports = sports.json();
      }),
      (error)=> {
        (<HTMLButtonElement>document.getElementById("sport")).disabled = true;
        (<HTMLButtonElement>document.getElementById("sport")).innerHTML = "<span class=\"glyphicon glyphicon-refresh glyphicon-refresh-animate\"></span> Loading...";
      }
  }

  minimalValue(form: FormControl){
    if(form.value.persons > 1){
      this.minValue = false;
      return null;
    } else{
      this.minValue = true;
    }
    return {'peopleAmount': true};
  }

}
