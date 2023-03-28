import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <div class="body">

    <div class="header">
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
  .body{
    margin: 0;
    min-height: 100%;
    display: grid;
    grid-template-rows: auto 1fr auto;
  }

  .header{
    position: fixed;
    width: 100%;
    background-color: gray;
    z-index: 1;
  }

  .content{
    margin-top: 75px;
  }
  .footer{
    width: 20%;
    margin: 20px 0px 20px 0px;

    /* puts the footer in the middle */
    position: relative;
    left: 50%;
    transform: translateX(-50%);
  }
  `]
})
export class AppComponent {
  title = 'Client2';
}
