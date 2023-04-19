import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../image/image.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ProfilepageNavComponent } from 'src/app/pages/profilepage-nav/profilepage-nav.component';
import { User } from 'src/app/_models/user';

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
export class ProfilepageSidenavComponent {
  @Input()
  profileUser: User = {
    userName: '',
    userImage: {
      image: '',
    },
  };
}
