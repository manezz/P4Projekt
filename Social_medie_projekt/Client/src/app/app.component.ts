import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',

  template: `
  <div class="container">
    <div class="header" style="display: {test}">
      <app-header></app-header>
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
    padding: 3px 5px;
  }
  .footer{
    clear: both;
    position: relative;
    bottom: 0;
    padding: 3px 5px;
  }
  `]

})
export class AppComponent {
  title = 'Client';

}
