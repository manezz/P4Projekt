import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  template: `
  <p> THE social media</p>
  <p> Copyright</p>
  <p> Bjarke | Leonard | Magnus</p>
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
