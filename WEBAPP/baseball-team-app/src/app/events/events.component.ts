import { Component, OnInit } from '@angular/core';
import { EventsService} from '../services/event.service'

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: Array<any>;

  constructor(private eventService: EventsService) { }

  ngOnInit(): void {
  }

  getUpcomingEvents() {
    this.eventService.getUpcomingEvents().subscribe({
      next: data => {
        this.events = data;
        console.log(data);
      },
      error: error => {
        console.log("error fetching events");
      }
    })
  }

  getPastEvents() {
    this.eventService.getPastEvents().subscribe({
      next: data => {
        this.events = data;
        console.log(this.events);
      },
      error: error => {
        console.log("error fetching events");
      }
    })
  }

  deleteEvent(e) {
    console.log(e);
    this.eventService.deleteEvent(e.eventId);
  }

  passEvent(e){
    this.eventService.passEvent = e;
  }

}
