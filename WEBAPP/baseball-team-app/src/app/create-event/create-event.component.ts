import { Component, OnInit } from '@angular/core';
import { EventsService } from '../services/event.service'
import { AuthService } from '../services/auth.service'

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {

  constructor(private eventService: EventsService, private authService: AuthService) { }

  authLevel: boolean;

  ngOnInit(): void {
  }

  postEvent(form) {
    this.eventService.postEvent(form.value);
    form.reset();
    this.authService.authLevel.subscribe(data => {
      this.authLevel = data;
    });
  }

}
