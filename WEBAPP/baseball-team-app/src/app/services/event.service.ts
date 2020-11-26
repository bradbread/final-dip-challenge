import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event} from '../models/event';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  apiUrl = "https://dip-challenge-apim.azure-api.net/v1/api/Games"
  passEvent: Event;

  constructor(private _http:HttpClient) { }

  getEvents(): Observable<Event[]> {
    return this._http.get<Event[]>(this.apiUrl);
  }

  getUpcomingEvents(): Observable<Event[]> {
    return this._http.get<Event[]>(this.apiUrl + "/upcoming");
  }

  getPastEvents(): Observable<Event[]> {
    return this._http.get<Event[]>(this.apiUrl + "/past");
  }

  postEvent(event: Event){
    console.log(event);
    return new Promise ((resolve, reject) => {
      this._http.post(this.apiUrl, event).subscribe(
        () => {
          console.log("event created");
          resolve();
        },
        err => {
          console.log("error creating event");
          reject(err);
        });
    })
  }

  deleteEvent(id) {
    return new Promise ((resolve, reject) => {
      this._http.delete(this.apiUrl + "/" + id).subscribe(
        () => {
          console.log("event deleted");
          resolve();
        },
        err => {
          console.log("error creating event");
          reject(err);
        });
    })
  }
}
