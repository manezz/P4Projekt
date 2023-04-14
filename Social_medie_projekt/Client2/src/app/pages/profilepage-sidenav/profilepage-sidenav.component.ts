import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../image/image.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ProfilepageNavComponent } from 'src/app/profilepage-nav/profilepage-nav.component';

@Component({
  selector: 'app-profilepage-sidenav',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatSidenavModule,
    ImageComponent,
    ProfilepageNavComponent,
  ],
  templateUrl: 'profilepage-sidenav.component.html',
  styleUrls: ['profilepage-sidenav.component.css'],
})
export class ProfilepageSidenavComponent {}
