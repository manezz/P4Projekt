import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  // standalone: true,
  // imports: [CommonModule],
  template: `
  <div class="nav">
    <a class="linkLeft"  routerLink="/main"    >Home</a>
    <a class="linkRight" routerLink="/"        >Logout</a>
    <a class="linkRight" routerLink="/profile" >Profile</a>
  </div>
  `,
  styles: [`
    .nav{
      background-color: transparent;
    }
    a:link, a:visited{
      display: inline;
      border-radius: 5px;
      padding: 2px 10px;
      color: #eee;
      text-decoration: none;
    }
    .linkLeft{
      margin: 0px 5px 0px 5px;
      float: left;
    }
    .linkRight{
      margin: 0px 5px 0px 5px;
      float: right;
    }
  `]
})
export class HeaderComponent {

}
