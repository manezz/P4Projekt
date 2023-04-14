import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../image/image.component';
import { ProfilepageNavComponent } from '../profilepage-nav/profilepage-nav.component';

@Component({
  selector: 'app-profilepage-centernav',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ImageComponent,
    ProfilepageNavComponent,
  ],
  templateUrl: 'profilepage-centernav.component.html',
  styleUrls: ['profilepage-centernav.component.css'],
})
export class ProfilepageCenternavComponent {}
