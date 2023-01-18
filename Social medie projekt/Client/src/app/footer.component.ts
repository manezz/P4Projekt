import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  // standalone: false,
  // imports: [CommonModule],
  template: `
  <div class="footer">
    <p> THE social media</p>
    <p> Copyright:</p>
    <p> Bjarke | Leonard | Wiktor</p>
  </div>
  `,
  styles: [`
  p{
    font-size: 12px;
    text-align: center;
    margin: 0 0 5px 0;
  }
  `]
})
export class FooterComponent {

}
