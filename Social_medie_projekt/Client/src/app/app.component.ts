import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './_services/auth.service';


@Component({
  selector: 'app-root',
  template: `
  <div class="container">
    <div class="header">
      <app-headerLoggedIn *ngIf="loggedIn"></app-headerLoggedIn>
      <app-headerLoggedOut *ngIf="!loggedIn"></app-headerLoggedOut>
    </div>
    <div class="content">
      <router-outlet></router-outlet>
    </div>
    <div class="footer">
      <app-footer></app-footer>
    </div>
  </div>
  `,
  styles: [`
  .header{
    width: 100%;
    position: fixed;
    top: 0px;
    left: 0px;
    padding: 10px 0px 10px 0px;
    background-color: gray;
    z-index: 1;
  }
  .content{
    position: relative;
    margin-top: 40px;
    margin-bottom: 200px;
    padding: 3px 5px;
  }
  .footer{
    position: relative;
    bottom: 0;
    padding: 3px 5px;
  }
  `]
})
export class AppComponent {

  constructor(private auth: AuthService) { }

  title = 'Client'
  currentUser: any
  loggedIn: any
  
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
}


