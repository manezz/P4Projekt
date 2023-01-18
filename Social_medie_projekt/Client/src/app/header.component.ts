import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  template: `
    <nav>
      <div class="topBar">
        <div class="left">
          <img src="/assets/images/socialmachine.png">
          <a routerLink="/main">Home</a>
        </div>
        <div class="right">
          <a routerLink="/profile">Profile</a>
          <a routerLink="/">Login</a>
        </div>
      </div>
    </nav>
  `,
  styles: [`
  a{
    background-color: white;
  }
  `]
})
export class HeaderComponent {

}
