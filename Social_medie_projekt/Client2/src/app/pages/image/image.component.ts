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
  imageValue: any;

  @Input()
  imageClass: any;

  constructor() {}
}
