import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../image/image.component';
import { User } from 'src/app/_models/user';
import { FollowButtonComponent } from '../follow-button/follow-button.component';

@Component({
  selector: 'app-profilepage-nav',
  standalone: true,
  imports: [CommonModule, RouterModule, ImageComponent, FollowButtonComponent],
  templateUrl: 'profilepage-nav.component.html',
  styleUrls: ['profilepage-nav.component.css'],
})
export class ProfilepageNavComponent {
  @Input()
  currentUser: any;

  imageClass: string = 'profileUserImage';

  @Input()
  profileUser: User = {
    userName: '',
    userImage: {
      image: '',
    },
  };
}
