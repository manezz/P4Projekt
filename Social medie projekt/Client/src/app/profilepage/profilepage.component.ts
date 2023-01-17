import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profilepage',
  // standalone: true,
  // imports: [CommonModule],
  template: `
    <h1>
      Profilepage works!
    </h1>
  `,
  styles: [`
  h1{
    color: white;
  }
  `]
})
export class ProfilepageComponent {

}
