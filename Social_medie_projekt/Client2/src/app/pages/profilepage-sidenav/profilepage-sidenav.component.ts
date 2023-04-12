import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../image/image.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-profilepage-sidenav',
  standalone: true,
  imports: [CommonModule, RouterModule, MatSidenavModule, ImageComponent],
  templateUrl: 'profilepage-sidenav.component.html',
  styleUrls: ['profilepage-sidenav.component.css'],
})
export class ProfilepageSidenavComponent {
  currentUser: any = {};

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }
}
