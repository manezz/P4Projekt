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
  // currentUser: any = {};
  @Input()
  imageValue: any;

  // constructor(private authService: AuthService) {}

  // ngOnInit(): void {
  //   this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  // }
}
