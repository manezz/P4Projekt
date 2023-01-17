import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-indexpage',
  // standalone: false,
  // imports: [CommonModule],
  template: `
    <h1>
      Indexpage works!
    </h1>
  `,
  styles: [`
  h1{
    color: white;
  }
  `]
  
})
export class IndexpageComponent {

}
