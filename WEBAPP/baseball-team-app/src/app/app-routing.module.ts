import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from '../app/login/login.component';
import { EventsComponent } from '../app/events/events.component';
import { PagenotfoundComponent } from '../app/pagenotfound/pagenotfound.component';
import { SignupComponent } from './signup/signup.component';
import { CreateEventComponent } from './create-event/create-event.component';
import { FeesComponent } from './fees/fees.component';
import {   RoleGuardService as RoleGuard } from './services/role-guard.service';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'events', component: EventsComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'CreateEvent', component: CreateEventComponent, 
    canActivate: [RoleGuard],
    data: {expectedRole: "Manager"} },
  {path: 'viewpaid', component: FeesComponent},
  { path: '',   redirectTo: '', pathMatch: 'full' }, // redirect to `first-component`
  { path: '**', component: PagenotfoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
