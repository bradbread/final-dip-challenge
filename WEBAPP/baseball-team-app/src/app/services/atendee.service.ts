import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Atendee} from '../models/atendee'

@Injectable({
  providedIn: 'root'
})
export class AtendeeService {
  apiUrl = "https://dip-challenge-apim.azure-api.net/v1/api/Attendees"

  constructor(private _http:HttpClient) { }

  getGameAtendees(id): Observable<Atendee[]> {
    return this._http.get<Atendee[]>(this.apiUrl + "/" + id);
  }

  postGamePaid(atendee) {
    console.log(atendee);
    //return this._http.post(this.apiUrl, atendee);
    return new Promise ((resolve, reject) => {
      this._http.post(this.apiUrl, atendee).subscribe(
        () => {
          console.log("payment recorded");
          resolve();
        },
        err => {
          console.log("error recording payment");
          reject(err);
        });
    })
  }
}
