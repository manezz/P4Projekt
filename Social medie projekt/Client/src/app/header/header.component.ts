import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  template: `
    <nav>
      <div class="topBar">
        <div class="left">
          <img src="/assets/images/socialmachine.png">
          <a href="#">Home</a>
        </div>
        <div class="right">
          <a href="#">Log In</a>
        </div>
      </div>
    </nav>`,
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

}
