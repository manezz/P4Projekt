import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  template: `
    <a routerLink="/">Login</a>
    <a routerLink="/main">Home</a>
    <a routerLink="/profile">Profile</a>
  `,
  styles: [`
  a{
    background-color: white;
  }
  `]
})
export class HeaderComponent {

}
