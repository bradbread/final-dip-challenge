import { Component, OnInit } from '@angular/core';
import { EventsService } from '../services/event.service'
import { AtendeeService } from '../services/atendee.service'
import { Atendee } from '../models/atendee';
import { JwtHelperService } from '@auth0/angular-jwt';
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-fees',
  templateUrl: './fees.component.html',
  styleUrls: ['./fees.component.css']
})
export class FeesComponent implements OnInit {

  atendees: Array<any>;
  event: any;
  constructor(private eventServices: EventsService, private attendeeService: AtendeeService, private jwtHelper: JwtHelperService) { }

  ngOnInit(): void {
    this.event =  this.eventServices.passEvent;
    this.loadPayments();
  }

  postPaid(form) {
    var person = new Atendee(this.eventServices.passEvent.eventId, form.value.Username , form.value.Paid);
    console.log(person);
    this.attendeeService.postGamePaid(person);
    form.reset();
    this.loadPayments();
  }

  loadPayments() {
    this.attendeeService.getGameAtendees(this.eventServices.passEvent.eventId).subscribe({
      next: data => {
        this.atendees = data;
        console.log(this.atendees);
      },
      error: error => {
        console.log("error fetching attendees");
      }
    })
  }

}
