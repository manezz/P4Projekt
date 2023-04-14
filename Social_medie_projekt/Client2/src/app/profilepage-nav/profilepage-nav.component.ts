import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../pages/image/image.component';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-profilepage-nav',
  standalone: true,
  imports: [CommonModule, RouterModule, ImageComponent],
  templateUrl: 'profilepage-nav.component.html',
  styleUrls: ['profilepage-nav.component.css'],
})
export class ProfilepageNavComponent {
  currentUser: any = {};

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }
}
