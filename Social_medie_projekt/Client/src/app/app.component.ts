import { Component, OnInit } from '@angular/core';
import { getMatInputUnsupportedTypeError } from '@angular/material/input';
import { Observable } from 'rxjs';
import { AuthService } from './_services/auth.service';


@Component({
  selector: 'app-root',
  template: `
  <div class="body">

    <div class="header">
      <app-headerLoggedIn *ngIf="loggedIn"></app-headerLoggedIn>
      <app-headerLoggedOut *ngIf="!loggedIn"></app-headerLoggedOut>
    </div>

    <div class="content">
      <router-outlet></router-outlet>
    </div>

    <div class="chat">
      <app-chat *ngIf="chatOpen"></app-chat>
    </div>

    <div class="footer">
      <app-footer></app-footer>
    </div>

  </div>
  `,
  styles: [`
  .body{
    margin: 0;
    min-height: 100%;
    display: grid;
    grid-template-rows: auto 1fr auto;
  }

  .header{
    width: 100%;
    padding: 10px 0px 10px 0px;
    background-color: gray;
  }

  .footer{
    width: 100%;
    margin: 20px 0px 20px 0px;

    /* puts the footer in the middle */
    position: relative;
    left: 50%;
    transform: translateX(-50%);
  }
  `]
})
export class AppComponent {

  constructor(private auth: AuthService) { }

  title = 'Client'
  currentUser: any
  loggedIn: any
  chatOpen: any
  
  // sørger for headeren altid er rigtig efter side opdatering
  ngOnInit(): void{
    this.validateHeader()
  }

  validateHeader(): void{
    this.auth.currentUser.subscribe(x => { this.currentUser = x})

    if (this.currentUser != null) {
      this.loggedIn = true
    }
    if (this.currentUser == null) {
      this.loggedIn = false
    }
  }

  openChat(): void{
    this.auth.currentUser.subscribe(x => { this.currentUser = x})

    if (this.currentUser != null) {
      

    }
  }

}


