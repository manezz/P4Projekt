import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-image',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: 'image.component.html',
  styleUrls: ['image.component.css'],
})
export class ImageComponent {
  @Input()
  imageValue: string = '/assets/images/placeholder.png';

  @Input()
  imageClass: string = '';

  getImageValue(): string {
    if (this.imageValue === '') {
      return '/assets/images/placeholder.png';
    } else {
      return 'data:image/png;base64, ' + this.imageValue;
    }
  }
}
