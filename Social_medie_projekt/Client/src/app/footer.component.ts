import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  template: `
    <p>
      footer works!
    </p>
  `,
  styles: [
  ]
})
export class FooterComponent {

}
