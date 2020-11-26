import { Component, OnInit } from '@angular/core';
import { MemberService } from '../services/member.service'

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  constructor(private memberService: MemberService) { }

  ngOnInit(): void {
  }

  Signup(form) {
    this.memberService.signup(form.value);
    console.log(form.value);
    form.reset();
  }

}