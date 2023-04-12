import { Component, OnInit, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-image',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: 'image.component.html',
})
export class ImageComponent {
  @Input()
  imageValue: any;

  constructor() {}
}
