import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { AuthService } from '../_services/auth.service';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-header',
  templateUrl: 'header.component.html',
  styleUrls: ['header.component.css'],
})
export class HeaderComponent {
  constructor(
    private auth: AuthService,
    private router: Router // private route: ActivatedRoute, // private AppComponent: AppComponent
  ) {
    this.auth.currentUser.subscribe((x) => (this.currentUser = x));
  }

  currentUser: any;
  //user: User[] = [];

  logOut() {
    this.auth.logout();
    this.auth.currentUser.subscribe((x) => (this.currentUser = x));

    //ændrer headeren
    // this.AppComponent.validateHeader();
  }
}
